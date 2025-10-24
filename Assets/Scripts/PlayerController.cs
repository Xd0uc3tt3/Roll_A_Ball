using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float speed = 0f;

    [Header("References")]
    public Transform cameraTransform;

    public TextMeshProUGUI countText;
    private int count;

    public TextMeshProUGUI lineCountText;
    private int lineCount;

    public TextMeshProUGUI attemptText;
    private int attempts = 3;

    private HashSet<GameObject> touchedLines = new HashSet<GameObject>();

    public GameObject winTextObject;
    public GameObject loseTextObject;

    public bool RLGLCompleted = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        lineCount = 0;
        attempts = 3;
        SetCountText();
        SetLineCountText();
        SetAttemptCountText();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name != "TraceMinigameCircle")
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
            Vector3 moveDirection = new Vector3(movementX, 0f, movementY);
            rb.AddForce(moveDirection * speed, ForceMode.Force);
        }
    }

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            count++;
            SetCountText();
        }

        if (other.gameObject.CompareTag("Line"))
        {
            if (!touchedLines.Contains(other.gameObject))
            {
                lineCount++;
                touchedLines.Add(other.gameObject);
                SetLineCountText();
            }
        }

        if (other.gameObject.CompareTag("traceHitbox"))
        {
            attempts--;
            ResetCharacter();
            SetAttemptCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 1)
        {
            winTextObject.SetActive(true);
            GamesManager.AdvanceToNextGame();
            SceneManager.LoadScene("Lobby");

        }
    }

    void SetLineCountText()
    {
        int percentage = lineCount * 5;
        lineCountText.text = "Progress: " + percentage.ToString() + " %";
        if (lineCount >= 20)
        {
            GamesManager.AdvanceToNextGame();
            SceneManager.LoadScene("Lobby");
        }
    }

    void SetAttemptCountText()
    {
        attemptText.text = "Attempts Remaining: " + attempts.ToString();
        if (attempts <= 0)
        {
            SceneManager.LoadScene("YouDiedScreen");
        }
    }

    void ResetCharacter()
    {
        transform.position = new Vector3(1.24f, 0.7f, -7.922f);
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}


