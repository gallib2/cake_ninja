using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnNextLevel;

    public static List<int> goals = new List<int>();
    public static int currentGoal;
    public static int score = 0;
    public static bool isGameOver = false;

    static public GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        StartGameSettings();
    }

    private void OnDisable()
    {
        score = 0;
        isGameOver = false;
        goals.Clear();
    }

    private void StartGameSettings()
    {
        Debug.Log("start game settings");
        goals.Add(2);
        currentGoal = goals.Last();
        score = 0;
        OnNextLevel?.Invoke();
    }

    public static void NextLevel()
    {
        Debug.Log("--------------in next level ");

        int nextGoal;
        //bool isNumberExistInGoals = false;
        nextGoal = UnityEngine.Random.Range(4, 6);
        bool isGoalOdd = nextGoal % 2 != 0;
        if (isGoalOdd)
        {
            nextGoal += 1;
        }

        goals.Add(nextGoal); // TODO G random, even and not exist in the list

        currentGoal = goals.Last();
        score = goals.Count - 1;

        OnNextLevel?.Invoke();
    }

    public static void GameOver()
    {
        isGameOver = true;
        //gameOverScreenPrefub = _gameOverScreenPrefub;
    }

    public void PlayAgain()
    {
        isGameOver = false;
        goals.Clear();

        Debug.Log("goals " + goals.Count);

        StartGameSettings();


        //if (gameOverScreenPrefub != null)
        //{
        //    Destroy(gameOverScreenPrefub.gameObject);
        //}
    }
}
