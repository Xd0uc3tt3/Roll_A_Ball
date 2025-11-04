using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void ComfirmLeaveGame()
    {
        SceneManager.LoadScene("ComfirmLeave");
    }
}

