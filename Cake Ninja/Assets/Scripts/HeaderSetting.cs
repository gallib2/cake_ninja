using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HeaderSetting : MonoBehaviour
{
    //public GameObject prefab;
    public GameObject[] prefabs;
    int goal;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        goal = GameManager.currentGoal;

        ChangeText();
    }

    // Update is called once per frame
    void Update()
    {
        if (goal != GameManager.currentGoal)
        {
            goal = GameManager.currentGoal;
            ChangeText();
        }

        if (score != GameManager.score)
        {
            score = GameManager.score;
            ChangeText();
        }
    }

    void ChangeText()
    {
        Debug.Log("prefabs.Length " + prefabs.Length);


        for (int i = 0; i < prefabs.Length; i++)
        {
            foreach (var item in prefabs[i].GetComponentsInChildren<Text>())
            {
                switch (item.name)
                {
                    case "Score":
                        item.text = "Score: " + score; // TODO G add score
                        break;
                    case "Goal":
                        item.text = "Goal: " + goal;
                        break;
                    case "Timer":
                        item.text = "Timer: " + 10; // TODO G add timer
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
