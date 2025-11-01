using UnityEngine;
using UnityEngine.SceneManagement;

public class TrafficLightZone : MonoBehaviour
{
    public enum LightState { Green, SlowDown, Red }
    public LightState currentState = LightState.Green;

    [Header("Timing")]
    public float minGreenTime = 4f;
    public float maxGreenTime = 8f;
    public float minRedTime = 3f;
    public float maxRedTime = 7f;
    public float slowDownDuration = 3.5f;

    [Header("Light References")]
    public Light directionalLight;
    public Color greenColor = Color.green;
    public Color yellowColor = Color.yellow;
    public Color redColor = Color.red;

    [Header("Player Detection")]
    public Rigidbody playerRb;
    public GameObject loseTextObject;
    public float movementThreshold = 5.0f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] greenLightClips;
    public AudioClip[] yellowLightClips;

    private float stateTimer = 0f;
    private float stateDuration = 0f;

    private void Start()
    {
        if (loseTextObject != null)
        {
            loseTextObject.SetActive(false);
        }

        SetState(LightState.Green);
    }

    private void Update()
    {
        if (currentState == LightState.Red && playerRb != null)
        {
            if (playerRb.linearVelocity.magnitude > movementThreshold)
            {
                if (loseTextObject != null)
                {
                    SceneManager.LoadScene("YouDiedScreen");
                }
            }
        }

        stateTimer += Time.deltaTime;

        while (stateTimer >= stateDuration)
        {
            if (currentState == LightState.Green)
            {
                SetState(LightState.SlowDown);
            }
            else if (currentState == LightState.SlowDown)
            {
                SetState(LightState.Red);
            }
            else if (currentState == LightState.Red)
            {
                SetState(LightState.Green);
            }
        }
    }

    private void SetState(LightState newState)
    {
        currentState = newState;
        stateTimer = 0f;

        if (newState == LightState.Green)
        {
            stateDuration = Random.Range(minGreenTime, maxGreenTime);
            SetLightColor(greenColor);
            PlayRandomClip(greenLightClips);
        }
        else if (newState == LightState.SlowDown)
        {
            stateDuration = slowDownDuration;
            SetLightColor(yellowColor);
            PlayRandomClip(yellowLightClips);
        }
        else if (newState == LightState.Red)
        {
            stateDuration = Random.Range(minRedTime, maxRedTime);
            SetLightColor(redColor);
        }
    }

    private void SetLightColor(Color c)
    {
        if (directionalLight != null)
        {
            directionalLight.color = c;
        }
    }

    private void PlayRandomClip(AudioClip[] clips)
    {
        if (audioSource == null || clips == null || clips.Length == 0)
            return;

        int index = Random.Range(0, clips.Length);
        audioSource.clip = clips[index];
        audioSource.Play();
    }
}
