using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using playerNameSpace;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI finalScore;    
    public TextMeshProUGUI virusCounterText; // UI Text element for virus counter
    public string levelSceneName = "SampleScene"; // game scene
    public string menuSceneName = "Menu"; // main menu scene
    public string nextSceneName = "NextScene"; // next scene after defeating all viruses

    private int totalVirusCount = 0; // Total number of virus enemies in the scene
    private int killedVirusCount = 0; // Number of virus enemies killed

    void Start()
    {
        // Display final score on the score screen
        if (finalScore != null)
        {
            finalScore.text = "Score: " + Player.scoreVal;
        }

        // Count all virus enemies in the scene
        totalVirusCount = FindObjectsOfType<Enemy>().Length;

        // Update the virus counter UI at the start
        UpdateVirusCounterUI();
    }

    public void restartGame()
    {
        Player.scoreVal = 0;
        Player.playerAlive = 1;
        SceneManager.LoadScene(levelSceneName);
    }

    public void mainMenuGame()
    {
        Player.scoreVal = 0;
        Player.playerAlive = 1;
        SceneManager.LoadScene(menuSceneName);
    }

    // Call this method when a virus is killed
    public void OnVirusKilled()
    {
        killedVirusCount++;
        UpdateVirusCounterUI();

        // Check if all viruses have been killed
        if (killedVirusCount == totalVirusCount)
        {
            OnAllVirusesKilled();
        }
    }

    // Update the UI text for virus counter
    private void UpdateVirusCounterUI()
    {
        if (virusCounterText != null)
        {
            virusCounterText.text = $"{killedVirusCount} / {totalVirusCount} Virus Cells";
        }
    }

    // Handle the event when all viruses are killed
    private void OnAllVirusesKilled()
    {
        // Transition to the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
