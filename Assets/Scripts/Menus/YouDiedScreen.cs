using UnityEngine;
using UnityEngine.SceneManagement;

public class YouDiedScreen : MonoBehaviour
{
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
