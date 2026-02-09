using UnityEngine;

public class Door : MonoBehaviour
{
    public PlayerKey player;
    public float openAngle = 90f;
    private bool isOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && player.hasKey && !isOpen)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        isOpen = true;
        transform.Rotate(0, openAngle, 0);
        Debug.Log("Door unlocked!");
    }
}
