using UnityEngine;

public class RightDoor : MonoBehaviour
{
    public Vector3 openPositionOffset = new Vector3(14.07f, 5.39f, 59.9f);
    public float slideSpeed = 2f;
    public bool open = false;

    private Vector3 closedPosition;
    private Vector3 openPosition;

    void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + openPositionOffset;
    }

    void Update()
    {
        Vector3 target = open ? openPosition : closedPosition;
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * slideSpeed);
    }

    public void ToggleDoor()
    {
        open = !open;
    }
}

