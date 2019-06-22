using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HeaderSetting : MonoBehaviour
{
    //public GameObject prefab;
    public GameObject[] prefabs;
    public Text scoreText;
    public Text goalText;

    int goal;
    int score;

    private void OnEnable()
    {
        GameManager.OnNextLevel += NextLevel;
    }

    private void OnDisable()
    {
        GameManager.OnNextLevel -= NextLevel;
    }

    // Start is called before the first frame update
    void Start()
    {
        goal = GameManager.currentGoal;

        ChangeText();
    }

    // Update is called once per frame
    void Update()
    {
        //goal = GameManager.currentGoal;
        //score = GameManager.score;
        //ChangeText();
    }

    private void NextLevel()
    {
        Debug.Log("GameManager.score" + GameManager.score);

        goalText.text = GameManager.currentGoal.ToString();
        scoreText.text = (GameManager.score + 1).ToString();

        //goal = GameManager.currentGoal;
        //score = GameManager.score;
        //ChangeText();
    }

    void ChangeText()
    {
        //for (int i = 0; i < prefabs.Length; i++)
        //{
        //    foreach (var item in prefabs[i].GetComponentsInChildren<Text>())
        //    {
        //        switch (item.name)
        //        {
        //            case "Score":
        //                item.text = "Score: " + score; // TODO G add score
        //                break;
        //            case "Goal":
        //                item.text = "Goal: " + goal;
        //                break;
        //            case "Timer":
        //                item.text = "Timer: " + 10; // TODO G add timer
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}
    }
}
