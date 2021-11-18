using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("On Screen Controls")]
    public GameObject onScreenControls;


    [Header("Button Controls")]
    public static bool jumpButtonDown;


    // Start is called before the first frame update
    void Start()
    {
        CheckPlataforms();
    }

    private void CheckPlataforms()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
            case RuntimePlatform.IPhonePlayer:
            case RuntimePlatform.WindowsEditor:
                onScreenControls.SetActive(true);
                break;
            default:
                onScreenControls.SetActive(false);
                break;
        }
    }
    public void OnJumpDown()
    {
        jumpButtonDown = true;  
    }

    public void OnJumpUp()
    {
        jumpButtonDown = false;
    }
}
