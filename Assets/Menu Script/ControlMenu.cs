using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Import the SceneManager namespace

public class ControlMenu : MonoBehaviour
{
    // This function can be called when the button is pressed
    public void OnClickBack()
    {
        SceneManager.LoadScene("Menu");  // Replace "Menu" with the name of your scene
    }
}
