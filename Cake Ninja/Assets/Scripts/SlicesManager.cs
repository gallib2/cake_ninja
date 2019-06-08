using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlicesManager : MonoBehaviour
{
    public int goal = 4;
    private int minmumSize;

    public double originalSize = 0;
    public double currentSize = 0;

    public int sliced = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int slicesCount = Slicer2D.GetList().Count;

        if (slicesCount == goal)
        {
            bool isAllSlicesEqual = IsAllSlicesAreAlmostEqual();

            Debug.Log("isAllSlicesEqual: " + isAllSlicesEqual);

            if(isAllSlicesEqual)
            {
                Debug.Log("all slices the same, move to next level ");
                // next level 
            }
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
