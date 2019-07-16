using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnNextLevel;

    public delegate void GoalChange(int goal);

    public static event GoalChange OnGoalChange;

    //TimerHelper timer;
    //float timerRequired = 1f;

    public List<int> slicesSizeList;
    public static List<int> goals = new List<int>();
    public static int currentGoal;
    public static int score = 0;
    public static bool isGameOver = false;


    static public GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //timer = TimerHelper.Create();

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
        goals.Add(2);
        currentGoal = goals.Last();
        score = 0;
        //OnNextLevel?.Invoke();
        OnGoalChange?.Invoke(currentGoal);
    }

    // TODO event
    public static void NextLevel()
    {
        int nextGoal = GetNextGoal();

        goals.Add(nextGoal);

        currentGoal = goals.Last();
        score = goals.Count - 1;

        //OnNextLevel?.Invoke();
        OnGoalChange?.Invoke(currentGoal);
    }

    public static void GameOver()
    {
        isGameOver = true;
        OnNextLevel?.Invoke();
    }

    public void PlayAgain()
    {
        isGameOver = false;
        goals.Clear();

        StartGameSettings();


        //if (gameOverScreenPrefub != null)
        //{
        //    Destroy(gameOverScreenPrefub.gameObject);
        //}
    }

    // goal need to be even
    private static int GetNextGoal()
    {
        int goal = UnityEngine.Random.Range(4, 6);
        bool isGoalOdd = goal % 2 != 0;
        if (isGoalOdd)
        {
            goal += 1;
        }

        return goal;
    }
}
