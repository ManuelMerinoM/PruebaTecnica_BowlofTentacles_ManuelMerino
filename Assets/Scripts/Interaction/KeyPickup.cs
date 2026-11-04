using UnityEngine;

public class KeyPickup : MonoBehaviour, IInteractable
{
    [Header("Inventory")]
    [SerializeField] private PlayerInventory playerInventory;


    public string InteractionPrompt => "[E] Recoger llave";

    public void Interact()
    {
        if (playerInventory == null)
        {
            Debug.LogWarning("No se ha asignado PlayerInventory a la llave.");
            return;
        }

        playerInventory.AddSpareKey();


        gameObject.SetActive(false);
    }
}
