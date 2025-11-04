using UnityEngine;
using UnityEngine.SceneManagement;

public class ComfirmLeave : MonoBehaviour
{
    public void RejoinGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
