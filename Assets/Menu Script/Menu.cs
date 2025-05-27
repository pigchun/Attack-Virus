using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
	public GameObject DoctorImage;  // Assign the DoctorImage UI element here in the Inspector
    public float displayDuration = 10f;  // Time to display the image
    public string levelSceneName = "SampleScene";  // Scene name
	public string controlsSceneName = "Controls"; // controls scene name
    public string settingsSceneName = "SettingsScene"; // settings scene name
	public Camera mainCamera;  // Assign your main camera here in the Inspector
    public float targetOrthographicSize = 0.4f;  // Desired orthographic size to zoom to
    public Vector3 targetCameraPosition = new Vector3(-1.5f, 0f, -10f);  // Final camera position
	
	private float initialOrthographicSize;  // To store the initial camera size
    private Vector3 initialCameraPosition;  // To store the initial camera position
	
	void Start()
    {
        // Store the initial orthographic size and position of the camera
        initialOrthographicSize = mainCamera.orthographicSize;
        initialCameraPosition = mainCamera.transform.position;
    }
	
    public void OnPlayButton(){
		// Start the coroutine to handle image display, camera zoom and position change, and scene transition
        StartCoroutine(ShowImageZoomMoveAndLoadScene());
	}
	
	
	IEnumerator ShowImageZoomMoveAndLoadScene()
    {
        // Show the DoctorImage
        DoctorImage.SetActive(true);

        // Start the zoom and position interpolation
        float elapsedTime = 0f;

        while (elapsedTime < displayDuration)
        {
            // Lerp the camera's orthographic size from initial size to the target size
            mainCamera.orthographicSize = Mathf.Lerp(initialOrthographicSize, targetOrthographicSize, elapsedTime / displayDuration);

            // Lerp the camera's position from initial position to the target position
            mainCamera.transform.position = Vector3.Lerp(initialCameraPosition, targetCameraPosition, elapsedTime / displayDuration);

            // Wait for the next frame
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the camera reaches the exact target size and position at the end
        mainCamera.orthographicSize = targetOrthographicSize;
        mainCamera.transform.position = targetCameraPosition;

        // Load the next scene after zoom and move are complete
        SceneManager.LoadScene(levelSceneName);
    }
	
	
	
	public void OnQuitButton ()
	{
		Application.Quit();
	}
	
	public void OnControlsButton(){
		SceneManager.LoadScene(controlsSceneName);
	}

    public void OnSettingsButton(){
        SceneManager.LoadScene(settingsSceneName);
    }
}
