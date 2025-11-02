using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public AudioSource introAudio;
    public GameObject invisibleWall;
    public MonoBehaviour redLightGreenLightScript;
    public RLGLTimer rlglTimer;

    void Start()
    {
        if (rlglTimer != null)
            rlglTimer.isRunning = false;
        Debug.Log("Timer paused: " + rlglTimer.isRunning);

        if (redLightGreenLightScript != null)
            redLightGreenLightScript.enabled = false;

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
        if (rlglTimer != null)
            rlglTimer.isRunning = true;

        if (redLightGreenLightScript != null)
            redLightGreenLightScript.enabled = true;

        if (invisibleWall != null)
            invisibleWall.SetActive(false);
    }
}