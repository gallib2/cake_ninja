using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score = 10;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        GameManager.OnNextLevel += NextLevel;
        //GameManager.GameOver += GameOver;
        SlicesManager.OnScoreChange += ScoreChanged;
    }

    private void OnDisable()
    {
        GameManager.OnNextLevel -= NextLevel;
        SlicesManager.OnScoreChange -= ScoreChanged;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ScoreChanged(int newScore)
    {
        score += newScore;
        scoreText.text = score.ToString();
    }

    private void NextLevel()
    {
        score += 10;
        scoreText.text = (score).ToString();
    }
}
