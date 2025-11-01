using UnityEngine;

public class RotateForever : MonoBehaviour
{
    public float baseSpeed = -150f;
    public float speedVariation = 1.0f;

    void Update()
    {
        float zRotation = transform.eulerAngles.z;

        if (zRotation > 180f) zRotation -= 360f;
        float rotationMultiplier = 1f + Mathf.Sin(zRotation * Mathf.Deg2Rad) * speedVariation;

        transform.Rotate(Vector3.forward * baseSpeed * rotationMultiplier * Time.deltaTime);
    }
}

