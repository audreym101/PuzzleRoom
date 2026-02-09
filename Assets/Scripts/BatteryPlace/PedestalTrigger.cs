using UnityEngine;

public class PedestalTrigger : MonoBehaviour
{
    public string correctBatteryTag;
    public PuzzleManager puzzleManager;
    private bool completed = false;

    void OnTriggerEnter(Collider other)
    {
        if (completed) return;

        if (other.CompareTag(correctBatteryTag))
        {
            completed = true;
            puzzleManager.CompletePuzzle();
            Debug.Log("Correct battery placed!");
        }
    }
}
