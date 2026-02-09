using UnityEngine;

public class TimedTile : MonoBehaviour
{
    public int tileNumber; // Unique ID for this tile (1-5)
    public Color activeColor = Color.green; // Color when activated
    public Color defaultColor = Color.gray; // Default color
    public PuzzleManager puzzleManager; // Reference to PuzzleManager

    private Renderer tileRenderer;
    private bool isActivated = false;

    void Start()
    {
        tileRenderer = GetComponent<Renderer>();
        tileRenderer.material.color = defaultColor;
    }

    // Call this when the player steps on the tile
    public void StepOnTile()
    {
        if (!isActivated)
        {
            isActivated = true;
            tileRenderer.material.color = activeColor;
            puzzleManager.CheckTimedTile(tileNumber);
        }
    }

    // Reset tile color and state
    public void ResetTile()
    {
        isActivated = false;
        if (tileRenderer != null)
            tileRenderer.material.color = defaultColor;
    }
}
