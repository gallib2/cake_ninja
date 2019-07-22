using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int initialScore = 10;
    public static int score = 10;
    public int regularScoreToAdd = 10;

    public Text scoreText;
    public GameObject[] floatingTextPrefubs;
    public GameObject[] negativeFeedbackPrefubs;
    GameObject floatingText;


    private void OnEnable()
    {
        GameManager.OnNextLevel += NextLevel;
        GameManager.OnGameOver += GameOver;
        SlicesManager.OnScoreChange += ScoreChanged;
        SlicesManager.OnBadSlice += BadSlice;
    }

    private void OnDisable()
    {
        GameManager.OnNextLevel -= NextLevel;
        GameManager.OnGameOver -= GameOver;
        SlicesManager.OnScoreChange -= ScoreChanged;
        SlicesManager.OnBadSlice -= BadSlice;
    }

    private void Start()
    {
        score = 0;
    }

    private void Update()
    {
        // if the feedback animation is over destroy
        if(floatingText && Mathf.Approximately(floatingText.transform.localScale.x, 0))
        {
            Destroy(floatingText);
        }
    }

    private void ScoreChanged(int scoreToAdd, ScoreLevel scoreLevel)
    {
        int newScore = score + scoreToAdd;
        int index = Random.Range(0, floatingTextPrefubs.Length);

        SetScore(newScore);
        if(floatingTextPrefubs[index] && scoreLevel != ScoreLevel.Regular)
        {
            ShowFloatingText(scoreLevel, floatingTextPrefubs[index]);
        }

    }

    private void BadSlice(bool isTooManySlices)
    {
        int tooManySlicesIndex = 3;

        int index = isTooManySlices ? tooManySlicesIndex : Random.Range(0, negativeFeedbackPrefubs.Length - 1);

        if (negativeFeedbackPrefubs[index])
        {
            ShowFloatingText(ScoreLevel.Regular, negativeFeedbackPrefubs[index]);
        }
    }

    private void ShowFloatingText(ScoreLevel scoreLevel, GameObject floatingTextPrefub)
    {
        floatingText = Instantiate(floatingTextPrefub);
        //TextMesh floatingTextMesh = floatingText.GetComponent<TextMesh>();
        //floatingTextMesh.text = scoreLevel.ToString();
        //floatingTextMesh.color
    }

    private void NextLevel()
    {
        int newScore = score + regularScoreToAdd;

        //Destroy(floatingText);

        SetScore(newScore);
    }

    private void GameOver()
    {
        Highscores.AddNewHighScore("poly", score);
        //SetScore(initialScore);
    }

    private void SetScore(int scoreToSet)
    {
        score = scoreToSet;
        scoreText.text = score.ToString();
    }
}
