using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Retrieve the saved volume setting and apply it
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.8f);
        audioSource.volume = savedVolume;

        // Start playing the music if it’s not already playing
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
