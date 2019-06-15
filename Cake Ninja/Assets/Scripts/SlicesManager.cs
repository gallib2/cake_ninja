﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlicesManager : MonoBehaviour
{
    public int goal;
    private int minmumSize;

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
        sliceableObjects = GameObject.FindGameObjectWithTag("SliceableObjects");
        goal = GameManager.currentGoal;

        Debug.Log("Goal is: " + goal);
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

    void GameOver()
    {
        DestroyAllLeftPieces();
        Instantiate(gameOverScreenPrefub);
        GameManager.GameOver();
        GameObject cake = GetRandomCake();
        Instantiate(cake, sliceableObjects.transform, true);
    }

    // Update is called once per frame
    void Update()
    {
        CheckSlices();
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
