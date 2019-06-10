using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static List<int> goals = new List<int>();
    public static int currentGoal;
    public static int score = 0;
    public static bool isGameOver = false;

    static GameObject gameOverScreenPrefub;

    static public GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        StartGameSettings();
    }

    private void StartGameSettings()
    {
        goals.Add(2);
        currentGoal = goals.Last();
        score = 0;
        gameOverScreenPrefub = null;
    }

    public static void NextLevel()
    {
        int nextGoal;
        //bool isNumberExistInGoals = false;
        nextGoal = Random.Range(4, 6);
        bool isGoalOdd = nextGoal % 2 != 0;
        if (isGoalOdd)
        {
            nextGoal += 1;
        }

        goals.Add(nextGoal); // TODO G random, even and not exist in the list

        currentGoal = goals.Last();
        score = goals.Count - 1;
    }

    public static void GameOver()
    {
        isGameOver = true;

        Debug.Log("gameOverScreenPrefub GameOver: ", gameOverScreenPrefub);
        //gameOverScreenPrefub = _gameOverScreenPrefub;
    }

    public void PlayAgain()
    {
        isGameOver = false;
        goals.Clear();

        StartGameSettings();

        Debug.Log("gameOverScreenPrefub PlayAgain: ", gameOverScreenPrefub);

        //if (gameOverScreenPrefub != null)
        //{
        //    Destroy(gameOverScreenPrefub.gameObject);
        //}
    }
}
