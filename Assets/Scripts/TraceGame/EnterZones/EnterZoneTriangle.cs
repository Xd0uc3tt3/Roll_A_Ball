using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterZoneTriangle : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "TraceMinigameTriangle";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
