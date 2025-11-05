using UnityEngine;
using UnityEngine.SceneManagement;

public class GamesManager : MonoBehaviour
{
    public static int currentGameIndex = 0;

    private string[] gameScenes = { "PhotoRoom", "RedLightGreenLight", "TraceMinigameLobby", "JumpRope" };

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadNextGame();
        }
    }

    public void LoadNextGame()
    {
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


