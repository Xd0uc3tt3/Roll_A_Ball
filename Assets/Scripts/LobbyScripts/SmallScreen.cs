using UnityEngine;

public class DoorShowWhenOpen : MonoBehaviour
{
    public LeftDoor door;

    public GameObject objectToShow;

    void Update()
    {
        if (door == null || objectToShow == null)
            return;

        objectToShow.SetActive(door.open);
    }
}
