using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtonsBehavior : MonoBehaviour
{
    private void Start()
    {
        SoundManager.soundManagerInstace.PlaySound("Background");
    }
    public Button startButton, instructionButton, quitButton;   
    public void StartButtonOnClick()
    {
        SoundManager.soundManagerInstace.PlaySound("Select");
        SceneManager.LoadScene(2);
    }

    public void InstructionsButtonOnClick()
    {
        SoundManager.soundManagerInstace.PlaySound("Select");
        SceneManager.LoadScene(1);
    }
    public void MainMenuButtonOnClick()
    {
        SoundManager.soundManagerInstace.PlaySound("Select");
        SceneManager.LoadScene(0);
    }

    //temporary
    public void GameOverButtonOnClick()
    {
        SoundManager.soundManagerInstace.PlaySound("Select");
        SceneManager.LoadScene(3);
    }


    public void QuitButtonOnClick()
    {
        SoundManager.soundManagerInstace.PlaySound("Select");
        Application.Quit();
    }
}
