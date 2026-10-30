using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool HasSpareKey { get; private set; }

    [SerializeField] private MessageController messageController;

    public void AddSpareKey()
    {
        HasSpareKey = true;
        messageController.ShowMessage("Has recogido la llave");
        Debug.Log("Has recogido la llave de repuesto.");
    }

    public bool UseSpareKey()
    {
        if (!HasSpareKey)
            return false;

        HasSpareKey = false;
        return true;
    }
}
