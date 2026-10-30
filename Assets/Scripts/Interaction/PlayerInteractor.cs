
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private InteractionModeController modeController;
    [SerializeField] private TMP_Text promptText;
    [SerializeField] private GameObject promptPanel;

    [Header("Settings")]
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private LayerMask interactionLayers = ~0;

    private PlayerInputControl inputActions;
    private Camera playerCamera;
    private IInteractable currentInteractable;

    private void Awake()
    {
        inputActions = new PlayerInputControl();

        
        playerCamera = Camera.main;

        if (playerCamera == null)
        {
            Debug.LogError(
                "PlayerInteractor: no se encuentra una cámara con la etiqueta MainCamera.",
                this
            );
        }

        if (promptText == null)
            Debug.LogError("Falta asignar Prompt Text.", this);

        if (promptPanel == null)
            Debug.LogError("Falta asignar Prompt Panel.", this);

        HidePrompt();
    }

    private void OnEnable()
    {
        if (inputActions == null)
            return;

        inputActions.Player.Enable();
        inputActions.Player.Interact.performed += OnInteract;
    }

    private void OnDisable()
    {
        if (inputActions == null)
            return;

        inputActions.Player.Interact.performed -= OnInteract;
        inputActions.Player.Disable();
    }

    private void OnDestroy()
    {
        inputActions?.Dispose();
    }

    private void Update()
    {
        if (modeController != null && modeController.IsBusy)
            return;

        DetectInteractable();
    }

    private void DetectInteractable()
    {
        IInteractable detected = null;

        if (playerCamera == null)
            return;

        Ray ray = playerCamera.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0f)
        );

        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, interactionLayers, QueryTriggerInteraction.Ignore))
        {
            detected = hit.collider.GetComponentInParent<IInteractable>();
        }

        if (detected == currentInteractable)
            return;

        currentInteractable = detected;

        if (currentInteractable != null)
            ShowPrompt(currentInteractable.InteractionPrompt);
        else
            HidePrompt();
    }


    private void OnInteract(InputAction.CallbackContext context)
    {
        if (InteractionModeController.Instance.IsBusy &&
            !InteractionModeController.Instance.CanInteractWithContents)
            return;

        if (currentInteractable != null)
            currentInteractable.Interact();
    }

    private void ShowPrompt(string message)
    {
        if (promptText != null)
            promptText.text = message;

        if (promptPanel != null)
            promptPanel.SetActive(true);
    }

    private void HidePrompt()
    {
        if (promptPanel != null)
            promptPanel.SetActive(false);
    }

    public void ClearTarget()
    {
        currentInteractable = null;
        HidePrompt();
    }
}
