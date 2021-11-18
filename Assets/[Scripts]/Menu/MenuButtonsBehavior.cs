using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtonsBehavior : MonoBehaviour
{
    public Button startButton, instructionButton, quitButton;   
    public void StartButtonOnClick()
    {
        SceneManager.LoadScene(2);
    }

    public void InstructionsButtonOnClick()
    {
        SceneManager.LoadScene(1);
    }
    public void MainMenuButtonOnClick()
    {
        SceneManager.LoadScene(0);
    }

    //temporary
    public void GameOverButtonOnClick()
    {
        SceneManager.LoadScene(3);
    }


    public void QuitButtonOnClick()
    {
        Application.Quit();
    }
}
