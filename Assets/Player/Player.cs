using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace playerNameSpace
{
    public class Player : MonoBehaviour
    {
        public static int scoreVal = 0;
        public static int playerAlive = 1;
        public TextMeshProUGUI score;
        public Button restart;
        public string levelSceneName = "PostScreen";
        private bool sceneLoaded = false;

        void Start() 
        {

        }

        void Update() 
        {
            if (score != null)
            {
                score.text = "Score: " + scoreVal;
            }
            if (playerAlive == 0 && !sceneLoaded)
            {
                sceneLoaded = true;
                StartCoroutine(PostScreenSceneCoroutine());
            }
        }

        private IEnumerator PostScreenSceneCoroutine()
        {
            yield return new WaitForSeconds(4); // Wait for 4 seconds
            SceneManager.LoadScene(levelSceneName);
        }
    }
}
