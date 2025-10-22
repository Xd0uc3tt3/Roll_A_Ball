using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float rotationSpeed = 1200f;
    public float wallBuffer = 0.2f;
    public LayerMask collisionLayers;

    private Vector3 offset;
    private float currentRotationX;
    private float currentRotationY;
    private float desiredDistance;

    void Start()
    {
        offset = transform.position - player.transform.position;
        desiredDistance = offset.magnitude;
    }

    void LateUpdate()
    {
        float horizontal = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float vertical = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        currentRotationY += horizontal;
        currentRotationX += vertical;
        currentRotationX = Mathf.Clamp(currentRotationX, -40f, 60f);

        Quaternion rotation = Quaternion.Euler(currentRotationX, currentRotationY, 0);
        Vector3 desiredCameraPos = player.transform.position + rotation * offset.normalized * desiredDistance;

        Vector3 playerHead = player.transform.position + Vector3.up * 1.5f;
        Vector3 direction = (desiredCameraPos - playerHead).normalized;
        float distance = desiredDistance;

        if (Physics.Raycast(playerHead, direction, out RaycastHit hit, desiredDistance, collisionLayers))
        {
            distance = hit.distance - wallBuffer;
        }

        transform.position = playerHead + direction * Mathf.Max(distance, 0.5f);
        transform.LookAt(playerHead);
    }
}




