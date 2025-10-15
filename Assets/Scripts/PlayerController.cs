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

    private HashSet<GameObject> touchedLines = new HashSet<GameObject>();

    public GameObject winTextObject;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        lineCount = 0;
        SetCountText();
        SetLineCountText();
        winTextObject.SetActive(false);
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
    }
}

