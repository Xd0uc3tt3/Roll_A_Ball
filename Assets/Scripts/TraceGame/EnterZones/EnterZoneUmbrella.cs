using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterZoneUmbrella : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "TraceMinigameUmbrella";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
