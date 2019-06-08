using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HeaderSetting : MonoBehaviour
{
    public GameObject prefab;
    int goal;

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
    }

    void ChangeText()
    {
        foreach (var item in prefab.GetComponentsInChildren<Text>())
        {
            switch (item.name)
            {
                case "Score":
                    item.text = "Score: " + 0; // TODO G add score
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
