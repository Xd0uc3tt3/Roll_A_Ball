using UnityEngine;
using System.Collections;

public class RotateRope : MonoBehaviour
{
    public float baseSpeed = -100f;
    public float speedVariation = 0.5f;
    public AudioSource swingSound;
    public float soundDelay = 0.2f;

    private float lastZ;
    private bool hasPlayedSound = false;
    private bool isDelaying = false;

    void Start()
    {
        lastZ = transform.eulerAngles.z;
    }

    void Update()
    {
        float zRotation = transform.eulerAngles.z;
        if (zRotation > 180f) zRotation -= 360f;

        float rotationMultiplier = 1f + Mathf.Sin(zRotation * Mathf.Deg2Rad) * speedVariation;
        transform.Rotate(Vector3.forward * baseSpeed * rotationMultiplier * Time.deltaTime);

        float currentZ = transform.eulerAngles.z;
        if (currentZ > 180f) currentZ -= 360f;

        bool isSwingingDown = currentZ < lastZ;

        if (isSwingingDown && !hasPlayedSound && !isDelaying)
        {
            StartCoroutine(PlayDelayedSound());
            hasPlayedSound = true;
        }

        if (!isSwingingDown)
            hasPlayedSound = false;

        lastZ = currentZ;
    }

    private IEnumerator PlayDelayedSound()
    {
        isDelaying = true;
        yield return new WaitForSeconds(soundDelay);

        if (swingSound != null)
            swingSound.Play();

        isDelaying = false;
    }
}


