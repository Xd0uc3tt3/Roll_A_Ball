using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterZoneStar : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "TraceMinigameStar";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
