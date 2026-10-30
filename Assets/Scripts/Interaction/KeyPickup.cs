using UnityEngine;

public class KeyPickup : MonoBehaviour, IInteractable
{
    [Header("Inventory")]
    [SerializeField] private PlayerInventory playerInventory;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pickupSound;

    public string InteractionPrompt => "[E] Recoger llave";

    public void Interact()
    {
        if (playerInventory == null)
        {
            Debug.LogWarning("No se ha asignado PlayerInventory a la llave.");
            return;
        }

        playerInventory.AddSpareKey();

        if (audioSource != null && pickupSound != null)
            audioSource.PlayOneShot(pickupSound);

        gameObject.SetActive(false);
    }
}
