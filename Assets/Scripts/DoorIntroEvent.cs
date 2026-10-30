
using UnityEngine;

public class DoorIntroEvent : MonoBehaviour
{
    [Header("Doors")]
    [SerializeField] private DoorInteractable entryDoor;
    [SerializeField] private DoorInteractable exitDoor;

    [Header("Message")]
    [SerializeField] private MessageController messageController;

    [TextArea]
    [SerializeField]
    private string introMessage =
        "He venido a recoger la caja de mamá. Después entregaré las llaves.";

    private void Start()
    {
        // Cerrar la puerta de entrada.
        if (entryDoor != null)
            entryDoor.CloseDoor();

        // Bloquear la salida.
        if (exitDoor != null)
            exitDoor.LockDoor();

        // Mostrar el mensaje inicial.
        if (messageController != null)
            messageController.ShowMessage(introMessage);
    }
}
