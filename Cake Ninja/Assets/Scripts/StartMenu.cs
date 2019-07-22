using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    const int gamoverScreenIndex = 2;

    public AudioClip buttonClicked;
    public AudioSource audioSource;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartGame();
        }
    }


    public void StartGame()
    {
        audioSource.PlayOneShot(buttonClicked);
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        if (SceneManager.GetActiveScene().buildIndex == gamoverScreenIndex)
        {
            nextScene = 1;
        }
        SceneManager.LoadScene(nextScene);
    }
}
