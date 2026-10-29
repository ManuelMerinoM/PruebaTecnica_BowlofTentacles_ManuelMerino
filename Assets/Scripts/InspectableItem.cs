
using UnityEngine;

public class InspectableItem : MonoBehaviour, IInteractable
{
    [Header("Item")]
    [SerializeField] private string itemName = "Objeto";

    private Transform originalParent;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Vector3 originalLocalPosition;
    private Quaternion originalLocalRotation;
    private Vector3 originalLocalScale;

    private Transform inspectionPoint;
    private Collider[] itemColliders;
    private bool[] colliderStates;
    private Rigidbody itemRigidbody;
    private bool wasKinematic;
    private bool hadGravity;
    private bool isBeingInspected;

    public string InteractionPrompt => $"[E] Inspeccionar {itemName}";

    [Header("Note")]
    [SerializeField] private bool isNote;
    [SerializeField] private string noteContent;

    public bool IsNote => isNote;
    public string NoteContent => noteContent;

    public void Interact()
    {
        if (InteractionModeController.Instance != null)
        {
            InteractionModeController.Instance.BeginInspection(this);
        }
    }

    public void StartInspection(Transform point)
    {
        if (isBeingInspected || point == null)
            return;

        originalParent = transform.parent;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalLocalPosition = transform.localPosition;
        originalLocalRotation = transform.localRotation;
        originalLocalScale = transform.localScale;

        inspectionPoint = point;

        itemColliders = GetComponentsInChildren<Collider>(true);
        colliderStates = new bool[itemColliders.Length];

        for (int i = 0; i < itemColliders.Length; i++)
        {
            colliderStates[i] = itemColliders[i].enabled;
            itemColliders[i].enabled = false;
        }

        itemRigidbody = GetComponent<Rigidbody>();

        if (itemRigidbody != null)
        {
            wasKinematic = itemRigidbody.isKinematic;
            hadGravity = itemRigidbody.useGravity;

            itemRigidbody.isKinematic = true;
            itemRigidbody.useGravity = false;
        }

        transform.SetParent(inspectionPoint, false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        isBeingInspected = true;
    }

    public void RotateInspection(Vector2 mouseDelta)
    {
        if (!isBeingInspected)
            return;

        transform.Rotate(
            Vector3.up,
            -mouseDelta.x * 0.2f,
            Space.World
        );

        transform.Rotate(
            Vector3.right,
            mouseDelta.y * 0.2f,
            Space.Self
        );
    }

    public void EndInspection()
    {
        if (!isBeingInspected)
            return;

        transform.SetParent(originalParent, true);
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        transform.localScale = originalLocalScale;

        // Restaurar también la posición y rotación locales
        // para conservar la jerarquía original.
        transform.localPosition = originalLocalPosition;
        transform.localRotation = originalLocalRotation;

        if (itemColliders != null)
        {
            for (int i = 0; i < itemColliders.Length; i++)
            {
                if (itemColliders[i] != null)
                    itemColliders[i].enabled = colliderStates[i];
            }
        }

        if (itemRigidbody != null)
        {
            itemRigidbody.isKinematic = wasKinematic;
            itemRigidbody.useGravity = hadGravity;
        }

        inspectionPoint = null;
        isBeingInspected = false;
    }
}
