using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int initialScore = 10;
    public int score = 10;
    public int regularScoreToAdd = 10;

    public Text scoreText;
    public GameObject floatingTextPrefub;


    private void OnEnable()
    {
        GameManager.OnNextLevel += NextLevel;
        GameManager.OnGameOver += GameOver;
        SlicesManager.OnScoreChange += ScoreChanged;
    }

    private void OnDisable()
    {
        GameManager.OnNextLevel -= NextLevel;
        GameManager.OnGameOver -= GameOver;
        SlicesManager.OnScoreChange -= ScoreChanged;
    }

    private void ScoreChanged(int scoreToAdd, ScoreLevel scoreLevel)
    {
        int newScore = score + scoreToAdd;

        SetScore(newScore);
        if(floatingTextPrefub && scoreLevel != ScoreLevel.Regular)
        {
            ShowFloatingText(scoreLevel);
        }

    }

    private void ShowFloatingText(ScoreLevel scoreLevel)
    {
        GameObject floatingText = Instantiate(floatingTextPrefub);
        TextMesh floatingTextMesh = floatingText.GetComponent<TextMesh>();
        floatingTextMesh.text = scoreLevel.ToString();
        //floatingTextMesh.color
    }

    private void NextLevel()
    {
        int newScore = score + regularScoreToAdd;

        SetScore(newScore);
    }

    private void GameOver()
    {
        Highscores.AddNewHighScore("poly", score);
        SetScore(initialScore);
    }

    private void SetScore(int scoreToSet)
    {
        score = scoreToSet;
        scoreText.text = score.ToString();
    }
}
