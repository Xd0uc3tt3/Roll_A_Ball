using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public AudioSource introAudio;
    public GameObject invisibleWall;
    public MonoBehaviour redLightGreenLightScript;
    public MonoBehaviour rlglTimerScript;

    void Start()
    {
        if (redLightGreenLightScript != null)
            redLightGreenLightScript.enabled = false;

        if (rlglTimerScript != null)
            rlglTimerScript.enabled = false;

        if (invisibleWall != null)
            invisibleWall.SetActive(true);

        if (introAudio != null && introAudio.clip != null)
        {
            introAudio.Play();
            Invoke(nameof(EndIntro), introAudio.clip.length);
        }
        else
        {
            EndIntro();
        }
    }

    void EndIntro()
    {
        if (redLightGreenLightScript != null)
            redLightGreenLightScript.enabled = true;

        if (rlglTimerScript != null)
            rlglTimerScript.enabled = true;

        if (invisibleWall != null)
            invisibleWall.SetActive(false);
    }
}
