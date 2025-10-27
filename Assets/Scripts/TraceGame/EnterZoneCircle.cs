using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterZoneCircle : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "TraceMinigameCircle";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}

