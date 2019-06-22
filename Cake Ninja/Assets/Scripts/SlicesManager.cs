using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SlicesManager : MonoBehaviour
{
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

    private void OnEnable()
    {
        GameManager.OnNextLevel += NextLevel;
    }

    private void OnDisable()
    {
        GameManager.OnNextLevel -= NextLevel;

    }

    void CheckSlices()
    {
        slicesCount = sliceableObjects.GetComponentsInChildren<Transform>().Length - 1;

        goal = GameManager.currentGoal;
        // Debug.Log("slices: " + slicesCount + " goal: " + goal);

        if (slicesCount == goal) // TODO g to check only if the user was slice...
        {
            bool isAllSlicesEqual = IsAllSlicesAreAlmostEqual();

            Debug.Log("isAllSlicesEqual: " + isAllSlicesEqual);

            if (isAllSlicesEqual)
            {
                Debug.Log("all slices the same, move to next level ");

                DestroyAllLeftPieces();
                GameObject cake = GetRandomCake();
                Instantiate(cake, sliceableObjects.transform, true); // create new cake

                GameManager.NextLevel();
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

    void DestroyAllLeftPieces(/*GameObject sliceableObjects*/)
    {
        foreach (Transform item in sliceableObjects.transform)
        {
            Destroy(item.gameObject);
        }
    }

    bool IsAllSlicesAreAlmostEqual()
    {
        bool isAlmostEqual = false;
        List<int> slicesSizeList = GetSlicesSizesList();
        int sliceSizeSupposedToBe = (int)originalSize / goal;

        minmumSize = sliceSizeSupposedToBe / 2;

        isAlmostEqual = !slicesSizeList.Any(currSize => currSize < minmumSize);

        return isAlmostEqual;
    }

    List<int> GetSlicesSizesList()
    {
        List<int> slicesSizeList = new List<int>();

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
        int index = Random.Range(0, maxIndex);

        return cakes[index];
    }
}
