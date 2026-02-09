using UnityEngine;

public class BatteryPlace : MonoBehaviour
{
    public Transform correctPedestal;   // assign the matching pedestal
    private bool isPlaced = false;

    void OnTriggerEnter(Collider other)
    {
        if(other.transform == correctPedestal && !isPlaced)
        {
            isPlaced = true;
            Debug.Log(name + " placed correctly!");
            // Snap battery to pedestal
            transform.position = correctPedestal.position + Vector3.up * 0.3f;
        }
    }
}
