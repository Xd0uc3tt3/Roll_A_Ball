using UnityEngine;

public class RotateForever : MonoBehaviour
{
    public float rotationSpeed = -100f;

    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
