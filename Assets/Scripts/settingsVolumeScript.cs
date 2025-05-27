using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        // Load the saved volume level or default to 0.8 if no value exists
        volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.8f);
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        // Save the volume level in PlayerPrefs
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
}
