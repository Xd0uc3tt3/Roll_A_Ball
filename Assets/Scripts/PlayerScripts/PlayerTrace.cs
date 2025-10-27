using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerTrace : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float speed = 0f;

    public TextMeshProUGUI countText;
    private int count;

    public TextMeshProUGUI lineCountText;
    private int lineCount;

    public TextMeshProUGUI attemptText;
    private int attempts = 3;

    private HashSet<GameObject> touchedLines = new HashSet<GameObject>();

    public GameObject winTextObject;
    public GameObject loseTextObject;

    private bool justReset = false;
    private bool canLoseAttempt = true;
    private float resetTimer = 0f;



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
            Vector3 moveDirection = new Vector3(movementX, 0f, movementY);
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

        if (!canLoseAttempt)
        {
            resetTimer -= Time.deltaTime;
            if (resetTimer <= 0f)
            {
                canLoseAttempt = true;
            }
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
            if (canLoseAttempt)
            {
                canLoseAttempt = false;
                attempts--;
                ResetCharacter();
                SetAttemptCountText();

                resetTimer = 0.3f;
            }
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
        string currentScene = SceneManager.GetActiveScene().name;
        int percentage;
        int maxLines;

        if (currentScene == "TraceMinigameUmbrella")
        {
            maxLines = 53;
        }
        else
        {
            maxLines = 20;
        }

        percentage = Mathf.RoundToInt(((float)lineCount / maxLines) * 100f);

        percentage = Mathf.Clamp(percentage, 0, 100);
        lineCountText.text = "Progress: " + percentage.ToString() + " %";

        if (lineCount >= maxLines)
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
        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "TraceMinigameCircle":
                transform.position = new Vector3(1.24f, 0.7f, -7.922f);
                break;

            case "TraceMinigameTriangle":
                transform.position = new Vector3(1.24f, 0.7f, -5.75f);
                break;

            case "TraceMinigameStar":
                transform.position = new Vector3(1.24f, 0.7f, -5.55f);
                break;

            case "TraceMinigameUmbrella":
                transform.position = new Vector3(0.85f, 0.7f, -4.52f);
                break;
        }

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        justReset = false;

    }

}
