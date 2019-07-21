using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayHighscores : MonoBehaviour
{
    public TextMesh[] highscoreText;
    Highscores HighscoreManager;

    void Start()
    {
        for (int i = 0; i < highscoreText.Length; i++)
        {
            highscoreText[i].text = i + 1 + ". Fetching...";
        }

        HighscoreManager = GetComponent<Highscores>();

        //StartCoroutine(RefreshHighscores());
    }

    public void OnHighscoresDownloaded(Highscore[] highscoreList)
    {
        for (int i = 0; i < highscoreText.Length; i++)
        {
            highscoreText[i].text = i + 1 + ". ";

            if (highscoreList.Length > i)
            {
                highscoreText[i].text += highscoreList[i].username + " - " + highscoreList[i].score;
            }
        }
    }

    IEnumerator RefreshHighscores()
    {
        while (true)
        {
            HighscoreManager.DownloadHighscores();

            yield return new WaitForSeconds(60);
        }
    }
}
