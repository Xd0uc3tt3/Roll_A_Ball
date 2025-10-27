using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitJumpRope : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GamesManager.AdvanceToNextGame();
            SceneManager.LoadScene("Lobby");
        }
    }
}

