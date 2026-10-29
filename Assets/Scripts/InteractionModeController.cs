
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractionModeController : MonoBehaviour
{
    public static InteractionModeController Instance { get; private set; }

    [Header("References")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerInteractor playerInteractor;
    [SerializeField] private Transform inspectionPoint;


    [Header("Note UI")]
    [SerializeField] private GameObject notePanel;
    [SerializeField] private TMP_Text noteContentText;

    [Header("Exit Prompt")]
    [SerializeField] private GameObject exitPromptPanel;
    [SerializeField] private TMP_Text exitPromptText;

    private InspectableItem activeItem;
    private bool movementWasEnabled;
    private bool cursorWasVisible;
    private CursorLockMode previousCursorLock;

    public bool IsBusy { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;


        if (notePanel != null)
            notePanel.SetActive(false);

        if (exitPromptPanel != null)
            exitPromptPanel.SetActive(false);
    }

    private void Update()
    {
        if (!IsBusy)
            return;

        if (Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            CloseCurrentMode();
            return;
        }

        if (activeItem != null && Mouse.current != null)
        {
            activeItem.RotateInspection(
                Mouse.current.delta.ReadValue()
            );
        }
    }

    public void BeginInspection(InspectableItem item)
    {
        if (IsBusy || item == null || inspectionPoint == null)
            return;

        BeginMode();

        activeItem = item;
        activeItem.StartInspection(inspectionPoint);


        if (item.IsNote)
            ShowNote(item);
        else if (notePanel != null)
            notePanel.SetActive(false);
    }

    private void ShowNote(InspectableItem item)
    {
        if (notePanel == null)
            return;

        notePanel.SetActive(true);


        if (noteContentText != null)
            noteContentText.text = item.NoteContent;

    }

    private void BeginMode()
    {
        IsBusy = true;

        movementWasEnabled = playerMovement != null &&
                             playerMovement.enabled;

        cursorWasVisible = Cursor.visible;
        previousCursorLock = Cursor.lockState;

        if (playerInteractor != null)
            playerInteractor.ClearTarget();

        if (playerMovement != null)
            playerMovement.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (exitPromptPanel != null)
            exitPromptPanel.SetActive(true);

        if (exitPromptText != null)
            exitPromptText.text = "[ESC] Salir";
    }

    public void CloseCurrentMode()
    {
        if (!IsBusy)
            return;

        if (activeItem != null)
        {
            activeItem.EndInspection();
            activeItem = null;
        }


        if (notePanel != null)
            notePanel.SetActive(false);

        IsBusy = false;

        if (playerMovement != null)
            playerMovement.enabled = movementWasEnabled;

        Cursor.lockState = previousCursorLock;
        Cursor.visible = cursorWasVisible;

        if (exitPromptPanel != null)
            exitPromptPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}
