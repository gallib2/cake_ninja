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
        GameManager.OnGoalChange += GoalChange;
    }

    private void OnDisable()
    {
        GameManager.OnNextLevel -= NextLevel;
        GameManager.OnGoalChange -= GoalChange;
    }

    // Start is called before the first frame update
    void Start()
    {
        //goal = GameManager.currentGoal;
    }

    private void GoalChange(int newGoal)
    {
        goalText.text = newGoal.ToString();
    }

    private void NextLevel()
    {
        Debug.Log("GameManager.score" + GameManager.score);

        goalText.text = GameManager.currentGoal.ToString();
        //scoreText.text = (GameManager.score + 1).ToString();
    }
}
