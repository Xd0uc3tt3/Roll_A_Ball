using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerJumpRope : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private bool isGrounded = true;

    public float speed = 10f;
    public float jumpForce = 7f;
    public Transform cameraTransform;
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    public float airDriftForce = 5f;
    private float nextDriftTime = 0f;
    private Vector3 randomAirDirection = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.linearDamping = 0f;
        rb.angularDamping = 0.05f;
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump(InputValue value)
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && isGrounded)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded)
        {
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;
            camForward.y = 0f;
            camRight.y = 0f;

            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDirection = camForward * movementY + camRight * movementX;
            rb.AddForce(moveDirection * speed, ForceMode.Force);

            if (moveDirection.sqrMagnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 10f * Time.deltaTime));
            }
        }
        else
        {
            if (Time.time >= nextDriftTime)
            {
                Vector3 camRight = cameraTransform.right;
                randomAirDirection = camRight * Random.Range(-1f, 1f);
                nextDriftTime = Time.time + Random.Range(0.3f, 0.7f);
            }

            rb.AddForce(randomAirDirection * airDriftForce, ForceMode.Force);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("KillZoneGround"))
        {
            SceneManager.LoadScene("YouDiedScreen");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rope"))
        {
            Vector3 pushDirection = (transform.position - collision.contacts[0].point).normalized;

            float pushForce = -12f;
            rb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
    }


    public void ResetCharacter()
    {
        transform.position = new Vector3(0.0f, 0.5f, 0.0f);
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}

