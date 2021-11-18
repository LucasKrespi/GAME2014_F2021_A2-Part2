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
        SceneManager.LoadScene(1);
    }

    public void InstructionsButtonOnClick()
    {
        Debug.Log("instructions");
    }

    public void QuitButtonOnClick()
    {
        Application.Quit();
    }
}
