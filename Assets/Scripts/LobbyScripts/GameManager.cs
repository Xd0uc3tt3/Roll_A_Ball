using UnityEngine;
using UnityEngine.SceneManagement;

public class GamesManager : MonoBehaviour
{
    public static int currentGameIndex = 0;

    private string[] gameScenes = { "RedLightGreenLight", "TraceMinigameCircle" };

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadNextGame();
        }
    }

    public void LoadNextGame()
    {
        Debug.Log("Active GamesManager from scene: " + gameObject.scene.name);
        Debug.Log("Current game index: " + currentGameIndex);
        Debug.Log("Next scene name: " + gameScenes[currentGameIndex]);

        if (currentGameIndex < gameScenes.Length)
        {
            string nextScene = gameScenes[currentGameIndex];
            SceneManager.LoadScene(nextScene);

        }
        else
        {
            Debug.Log("All games completed!");
        }
    }

    public static void AdvanceToNextGame()
    {
        currentGameIndex++;
    }
}


