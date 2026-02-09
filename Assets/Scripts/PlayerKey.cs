using UnityEngine;

public class PlayerKey : MonoBehaviour
{
    public bool hasKey = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            hasKey = true;
            Destroy(other.gameObject);
            Debug.Log("Key collected!");
        }
    }
}
