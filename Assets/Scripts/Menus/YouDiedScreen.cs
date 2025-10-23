using UnityEngine;
using UnityEngine.SceneManagement;

public class YouLooseScreen : MonoBehaviour
{
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
