using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float speed = 0;

    public TextMeshProUGUI countText;
    private int count;

    public TextMeshProUGUI lineCountText;
    private int lineCount;

    public TextMeshProUGUI attemptText;
    private int attempts = 3;

    private HashSet<GameObject> touchedLines = new HashSet<GameObject>();

    public GameObject winTextObject;
    public GameObject loseTextObject;

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

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }

    void SetLineCountText()
    {
        lineCountText.text = "Line Count: " + lineCount.ToString();
        if (lineCount >= 20)
        {
            winTextObject.SetActive(true);
        }
    }

    void SetAttemptCountText()
    {
        attemptText.text = "Attempts Remaining: " + attempts.ToString();
        if (attempts <= 0)
        {
            loseTextObject.SetActive(true);
        }
    }

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            count++;
            other.gameObject.SetActive(false);
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

    void ResetCharacter()
    {
        transform.position = new Vector3(0f, 0.7f, -8f);

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}


