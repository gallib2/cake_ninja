using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SlicesManager : MonoBehaviour
{
    public delegate void ScoreChange(int score);

    public static event ScoreChange OnScoreChange;

    List<int> slicesSizeList;
    // public AudioSource m_MyAudioSource;
    public Slider sliderTimer;
    public int goal;
    private int minmumSize;
    public ParticleSystem particlesEndLevel;

    public Text timerText;

    TimerHelper timer;
    float timerRequired = 1f;
    bool toStopTimer = false;

    private double originalSize = 0;
    private int sliced = 0;

    //public GameObject cake;
    public GameObject[] cakes;
    public GameObject gameOverScreenPrefub;

    GameObject sliceableObjects;

    public int slicesCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        // m_MyAudioSource = GetComponent<AudioSource>();
        // m_MyAudioSource.Play();
        sliceableObjects = GameObject.FindGameObjectWithTag("SliceableObjects");
        goal = GameManager.currentGoal;

        timer = TimerHelper.Create();

        Debug.Log("Goal is: " + goal);
    }

    // Update is called once per frame
    void Update()
    {
        int timerOpp = 8 - (int)timer.Get();
        sliderTimer.value = timerOpp; //(int)timer.Get();

        timerText.text = "Timer: " + timerOpp;

        if (!toStopTimer && timerOpp < 0)
        {
            Debug.Log("times up! ");
            GameOver();
            toStopTimer = true;
        }
        else
        {
            CheckSlices();
        }
    }

    void CheckSlices()
    {
        slicesCount = sliceableObjects.GetComponentsInChildren<Transform>().Length - 1;

        goal = GameManager.currentGoal;

        if (slicesCount == goal)
        {
            bool isAllSlicesEqual = IsAllSlicesAreAlmostEqual();

            if (isAllSlicesEqual)
            {

                CalculateNewScore();

                DestroyAllLeftPieces();
                GameObject cake = GetRandomCake();
                Instantiate(cake, sliceableObjects.transform, true); // create new cake

                GameManager.NextLevel();
                NextLevel();
            }
            else if (!isAllSlicesEqual && !GameManager.isGameOver)
            {
                GameOver();
            }
        }
        else if (slicesCount > goal)
        {
            GameOver();
        }
    }

    private void CalculateNewScore()
    {
        int sliceSizeSupposedToBe = (int)originalSize / goal;

        const int awsomePercentage = 95;
        const int greatePercentage = 90;
        const int nicePercentage = 85;

        bool isAwsome;

        isAwsome = slicesSizeList.Any(currSize => {
        int percentage = (currSize * 100) / sliceSizeSupposedToBe;

            return percentage >= awsomePercentage;
        });

        OnScoreChange?.Invoke(30);
        //return 30;
    }

    void NextLevel() 
    {
        particlesEndLevel.Play();
        toStopTimer = false;
        timer = TimerHelper.Create();
    }


    void GameOver()
    {
       // m_MyAudioSource.Stop();
        DestroyAllLeftPieces();
        Instantiate(gameOverScreenPrefub);
        GameManager.GameOver();
        GameObject cake = GetRandomCake();
        Instantiate(cake, sliceableObjects.transform, true);
    }

    void DestroyAllLeftPieces()
    {
        foreach (Transform item in sliceableObjects.transform)
        {
            Destroy(item.gameObject);
        }
    }

    bool IsAllSlicesAreAlmostEqual()
    {
        bool isAlmostEqual = false;
        slicesSizeList = GetSlicesSizesList();
        int sliceSizeSupposedToBe = (int)originalSize / goal;

        minmumSize = sliceSizeSupposedToBe / 2;

        isAlmostEqual = !slicesSizeList.Any(currSize => currSize < minmumSize);

        return isAlmostEqual;
    }

    //private List<int> GetSlicesPercentageList(int sliceSizeSupposedToBe)
    //{
    //    List<int> slicesPercentageList = new List<int>();

    //    foreach (var currentSliceSize in slicesSizeList)
    //    {
    //        int percentage = (currentSliceSize * 100) / sliceSizeSupposedToBe;
    //        slicesPercentageList.Add(percentage);
    //    }

    //    return slicesPercentageList;

    //    //slicesSizeList.Any(currSize => {
    //    //int percentage = (currSize * 100) / sliceSizeSupposedToBe;
            
    //    //    return currSize < minmumSize;
    //    //});

    //}

    List<int> GetSlicesSizesList()
    {
        slicesSizeList = new List<int>();

        foreach (Slicer2D slicer in Slicer2D.GetList())
        {
            Polygon2D poly = slicer.GetPolygon().ToWorldSpace(slicer.transform);

            originalSize = slicer.GetComponent<DemoSlicer2DInspectorTracker>().originalSize;
            int currentSizeInt = (int)poly.GetArea();
            Debug.Log("current size : " + currentSizeInt);
            slicesSizeList.Add(currentSizeInt);

            sliced = slicer.sliceCounter;
        }

        return slicesSizeList;
    }

    GameObject GetRandomCake()
    {
        int maxIndex = cakes.Length;
        int index = UnityEngine.Random.Range(0, maxIndex);

        return cakes[index];
    }
}
