using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private bool isYesButtonClicked = false;
    private bool isLocalButtonClicked = false;

    public void OnYesButtonClick()
    {
        isYesButtonClicked = true;
        Restart();
    }
    public void OnNoButtonClick()
    {
        isYesButtonClicked = false;
        Restart();
    }

    public void OnLocalButtonClick()
    {
        isLocalButtonClicked = true;
        SceneManager.LoadScene(2);
    }

    public void Restart()
    {
        // If the yes button is clicked, restart the game by loading the game scene
        if (isYesButtonClicked)
        {
            SceneManager.LoadScene(1); // Load the game scene
        }
        else
        {
            SceneManager.LoadScene(0); // Load the main menu scene
        }
    }

}
