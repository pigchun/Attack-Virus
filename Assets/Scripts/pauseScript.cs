using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;           // Assign your PauseMenu panel here
    public AudioSource musicSource;        // Assign your AudioSource for music here
    public Slider volumeSlider;            // Assign the volume slider here

    private bool isPaused = false;

    void Start()
    {
        pauseMenu.SetActive(false); // Hide the menu at the start

        // Load the saved volume from PlayerPrefs or set it to 0.5 if it doesn't exist
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.8f);
        volumeSlider.value = savedVolume; // Set the slider to match the saved volume or 0.5
        musicSource.volume = savedVolume; // Set the music source volume to match

        // Add listener to the slider to call SetVolume when the value changes
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        pauseMenu.SetActive(true); // Show the pause menu
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        pauseMenu.SetActive(false); // Hide the pause menu
    }

    public void SetVolume(float volume)
    {
        musicSource.volume = volume;                  // Set the AudioSource volume
        PlayerPrefs.SetFloat("MusicVolume", volume);  // Save the volume to PlayerPrefs
        PlayerPrefs.Save();                           // Ensure the value is saved immediately
    }
}
