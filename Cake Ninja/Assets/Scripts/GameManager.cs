using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static List<int> goals = new List<int>();
    public static int currentGoal;

    static public GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        goals.Add(2);

        currentGoal = goals.Last();
    }

    // Update is called once per frame
    void Update()
    {
        
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


        //do
        //{
        //    nextGoal = Random.Range(4, 6);
        //    bool isGoalOdd = nextGoal % 2 != 0;
        //    if (isGoalOdd)
        //    {
        //        nextGoal += 1;
        //    }

        //    isNumberExistInGoals = goals.Any(number => number == nextGoal);

        //} while (isNumberExistInGoals);

        goals.Add(nextGoal); // TODO G random, even and not exist in the list

        currentGoal = goals.Last();
    }
}
