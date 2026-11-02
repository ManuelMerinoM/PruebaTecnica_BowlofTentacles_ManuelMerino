
using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    [Header("Door")]
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private string openTrigger = "Open";
    [SerializeField] private string closeTrigger = "Close";

    [Header("Lock")]
    [SerializeField] private bool isLocked = true;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip openSound;
    [SerializeField] public AudioClip closeSound;
    [SerializeField] private AudioClip lockedSound;

    [Header("Messages")]
    [SerializeField] private MessageController messageController;
    [TextArea]
    [SerializeField]
    private string lockedMessage =
        "La llave de repuesto está en su caja.";

    [Header("Key")]
    [SerializeField] private PlayerInventory playerInventory;

    [Header("Final del juego")]
    [SerializeField] private EndingEvent endingController;

    public string InteractionPrompt =>
        isLocked ? "[E] Puerta bloqueada" : "[E] Abrir puerta";


    public void Interact()
    {
        if (isLocked)
        {
            if (playerInventory == null ||
                !playerInventory.HasSpareKey)
            {
                if (audioSource != null && lockedSound != null)
                    audioSource.PlayOneShot(lockedSound);

                if (messageController != null)
                    messageController.ShowMessage(lockedMessage);

                return;
            }           
            isLocked = false;

            playerInventory.UseSpareKey();
        }

        if (endingController != null)
        {
            endingController.BeginEnding(this);
        }
        else
        {
            OpenDoor();
        }
    }


    public void OpenDoor()
    {
        if (isLocked)
            return;

        if (doorAnimator != null)
            doorAnimator.SetTrigger(openTrigger);

        if (audioSource != null && openSound != null)
            audioSource.PlayOneShot(openSound);
    }

    public void CloseDoor()
    {
        Debug.Log("Cerrando puerta");
        if (doorAnimator != null)
            doorAnimator.SetTrigger(closeTrigger);


        if (audioSource != null && closeSound != null)
            audioSource.PlayOneShot(closeSound);
    }

    public void UnlockDoor()
    {
        isLocked = false;
    }

    public void LockDoor()
    {
        isLocked = true;
    }
}
