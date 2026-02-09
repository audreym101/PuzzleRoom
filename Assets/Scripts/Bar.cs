using UnityEngine;

public class Bar : MonoBehaviour
{
    public PlayerKey player;
    public float openAngle = 90f;
    private bool isOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && player.hasKey && !isOpen)
        {
            OpenBar();
        }
    }

    void OpenBar()
    {
        isOpen = true;
        transform.Rotate(0, openAngle, 0);
        Debug.Log("Bar unlocked!");
    }
}
