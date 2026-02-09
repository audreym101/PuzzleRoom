using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 3f;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("Mouse clicked!");
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                Debug.Log("Hit: " + hit.collider.name);

                // Check for SwitchInteractable
                SwitchInteractable switchInteractable = hit.collider.GetComponent<SwitchInteractable>();
                if (switchInteractable != null)
                {
                    Debug.Log("SwitchInteractable found!");
                    switchInteractable.Interact();
                    return;
                }

                // Check for ButtonPuzzle (Puzzle 1)
                ButtonPuzzle buttonPuzzle = hit.collider.GetComponent<ButtonPuzzle>();
                if (buttonPuzzle != null)
                {
                    Debug.Log("ButtonPuzzle found!");
                    buttonPuzzle.PressButton();
                    return;
                }

                // Check for SoundPad (Puzzle 2/4)
                SoundPad soundPad = hit.collider.GetComponent<SoundPad>();
                if (soundPad == null)
                    soundPad = hit.collider.GetComponentInParent<SoundPad>();
                if (soundPad != null)
                {
                    Debug.Log("SoundPad found!");
                    soundPad.Interact();
                    return;
                }

                // ✅ Check for TimedTile (Puzzle 5)
                TimedTile timedTile = hit.collider.GetComponent<TimedTile>();
                if (timedTile != null)
                {
                    Debug.Log("TimedTile found!");
                    timedTile.StepOnTile();
                    return;
                }

                Debug.Log("No interactable component found");
            }
            else
            {
                Debug.Log("Raycast missed");
            }
        }
    }
}
