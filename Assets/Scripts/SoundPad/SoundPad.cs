using UnityEngine;

public class SoundPad : MonoBehaviour
{
    public AudioSource audioSource;
    public int padNumber;
    public PuzzleManager puzzleManager;

    public void Interact()
    {
        Debug.Log("SoundPad " + padNumber + " interacted!");
        
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource not assigned!");
        }
        
        if (puzzleManager != null)
        {
            puzzleManager.CheckSoundSequence(padNumber);
        }
        else
        {
            Debug.LogError("PuzzleManager not assigned to SoundPad " + padNumber + "!");
        }
    }
}
