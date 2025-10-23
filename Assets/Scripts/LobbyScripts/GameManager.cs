using UnityEngine;
using UnityEngine.SceneManagement;

public class GamesManager : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("RedLightGreenLight");
        }
    }
}

