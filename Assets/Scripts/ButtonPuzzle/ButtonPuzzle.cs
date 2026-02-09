using UnityEngine;

public class ButtonPuzzle : MonoBehaviour
{
    public int buttonNumber; // 1, 2, 3
    public PuzzleManager puzzleManager;
    public Material defaultMat;
    public Material pressedMat;
    [HideInInspector] public bool pressed = false;

    // Called by player interaction
    public void PressButton()
    {
        if (pressed) return;

        pressed = true;
        GetComponent<Renderer>().material = pressedMat;
        puzzleManager.CheckButtonSequence(buttonNumber);
        Debug.Log("Button " + buttonNumber + " pressed");
    }

    // Reset function
    public void ResetButton()
    {
        pressed = false;
        GetComponent<Renderer>().material = defaultMat;
    }
}
