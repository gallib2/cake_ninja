using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlicesManager : MonoBehaviour
{
    public int goal;
    private int minmumSize;

    private double originalSize = 0;
    private int sliced = 0;

    public GameObject cake;
    public GameObject gameOverScreenPrefub;

    //private bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        goal = GameManager.currentGoal;

        Debug.Log("Goal is: " + goal);
    }

    // Update is called once per frame
    void Update()
    {
        int slicesCount = Slicer2D.GetList().Count;

        goal = GameManager.currentGoal;

        if (slicesCount == goal) // TODO g to check only if the user was slice...
        {
            bool isAllSlicesEqual = IsAllSlicesAreAlmostEqual();

            Debug.Log("isAllSlicesEqual: " + isAllSlicesEqual);

            if(isAllSlicesEqual)
            {
                Debug.Log("all slices the same, move to next level ");

                GameObject sliceableObjects = GameObject.FindGameObjectWithTag("SliceableObjects");

                DestroyAllLeftPieces(sliceableObjects);
                Instantiate(cake, sliceableObjects.transform, true); // create new cake

                GameManager.NextLevel();
            }
            else if (!isAllSlicesEqual && !GameManager.isGameOver)
            {
                GameObject sliceableObjects = GameObject.FindGameObjectWithTag("SliceableObjects");

                //isGameOver = true;
                DestroyAllLeftPieces(sliceableObjects);
                GameObject gameOverScreenPrefubCopy = Instantiate(gameOverScreenPrefub);
                GameManager.GameOver(gameOverScreenPrefubCopy);
            }
        }
    }

    void DestroyAllLeftPieces(GameObject sliceableObjects)
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
            slicesSizeList.Add(currentSizeInt);

            sliced = slicer.sliceCounter;
        }

        return slicesSizeList;
    }
}
