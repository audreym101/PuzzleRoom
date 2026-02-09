using UnityEngine;

public class SwitchInteractable : MonoBehaviour
{
    public bool isCorrectSwitch = false;
    public Light indicatorLight;
    public PuzzleManager puzzleManager;
    private bool hasCompleted = false;

    public void Interact()
    {
        if (hasCompleted) return;

        Debug.Log("Switch interacted! isCorrect: " + isCorrectSwitch);
        if (isCorrectSwitch)
        {
            hasCompleted = true;
            if (indicatorLight != null)
                indicatorLight.color = Color.green;

            if (puzzleManager != null)
            {
                puzzleManager.CompletePuzzle();
            }
            else
            {
                Debug.LogError("PuzzleManager not assigned!");
            }
        }
        else
        {
            if (indicatorLight != null)
                indicatorLight.color = Color.red;

            Debug.Log("Wrong switch!");
        }
    }
}
