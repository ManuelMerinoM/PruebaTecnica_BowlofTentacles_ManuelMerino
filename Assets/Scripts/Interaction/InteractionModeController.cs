
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

    [Header("Code Box UI")]
    [SerializeField] private GameObject keypadPanel;

    [SerializeField] private UnityEngine.UI.Image[] symbolButtonImages = new UnityEngine.UI.Image[3];

    [SerializeField] private UnityEngine.UI.Image[] codeSlots;

    [Header("Code Button Sound")]
    [SerializeField] private AudioSource buttonAudioSource;
    [SerializeField] private AudioClip buttonClickSound;

    public bool CanInteractWithContents =>
    IsBusy && activeCodeBox != null && activeCodeBox.IsOpen;
    private bool isUsingKeypad;

    private CodeBoxLock activeCodeBox;
    private readonly System.Collections.Generic.List<int> enteredSymbols =
        new System.Collections.Generic.List<int>();

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

        if (activeItem != null && !isUsingKeypad)
        {
            activeItem.RotateInspection(Mouse.current.delta.ReadValue());
        }
    }

    public void BeginInspection(InspectableItem item)
    {
        if (IsBusy || item == null || inspectionPoint == null)
            return;

        BeginMode();

        activeItem = item;
        activeItem.StartInspection(inspectionPoint);

        SetupCodeBox(item);

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
        bool playLivingRoomEvent = activeItem != null && activeItem.TriggersLivingRoomEvent;

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

        if (keypadPanel != null)
            keypadPanel.SetActive(false);

        if (playLivingRoomEvent)
        {
            FindFirstObjectByType<NoteEvent>().PlayOnce();
        }


        activeCodeBox = null;
        isUsingKeypad = false;
        enteredSymbols.Clear();
        RefreshCodeDisplay();
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }


    private void SetupCodeBox(InspectableItem item)
    {
        activeCodeBox = item.GetComponent<CodeBoxLock>();
        enteredSymbols.Clear();

        bool showKeypad = activeCodeBox != null && !activeCodeBox.IsOpen;

        if (keypadPanel != null)
            keypadPanel.SetActive(showKeypad);

        // Bloquear rotación si aparece el teclado.
        isUsingKeypad = showKeypad;

        if (activeCodeBox != null)
        {
            for (int i = 0; i < symbolButtonImages.Length; i++)
            {
                if (symbolButtonImages[i] != null)
                {
                    symbolButtonImages[i].sprite =
                        activeCodeBox.GetSymbolSprite(i);
                }
            }
        }

        RefreshCodeDisplay();
    }

    public void PressSymbol(int symbolId)
    {
        if (!IsBusy || activeCodeBox == null || activeCodeBox.IsOpen)
            return;

        if (enteredSymbols.Count >= activeCodeBox.CodeLength)
            return;

        PlayButtonClick();

        enteredSymbols.Add(symbolId);
        RefreshCodeDisplay();

        if (enteredSymbols.Count == activeCodeBox.CodeLength)
        {
            bool opened = activeCodeBox.TryOpen(enteredSymbols.ToArray());

            if (opened)
            {
                if (keypadPanel != null)
                    keypadPanel.SetActive(false);
            }
            else
            {
                enteredSymbols.Clear();
                RefreshCodeDisplay();
            }
        }
    }

    public void BackspaceSymbol()
    {
        if (!IsBusy || activeCodeBox == null)
            return;

        if (enteredSymbols.Count > 0)
        {
            PlayButtonClick();

            enteredSymbols.RemoveAt(enteredSymbols.Count - 1);
            RefreshCodeDisplay();
        }
    }

    private void PlayButtonClick()
    {
        if (buttonAudioSource != null && buttonClickSound != null)
        {
            buttonAudioSource.PlayOneShot(buttonClickSound);
        }
    }


    private void RefreshCodeDisplay()
    {
        if (codeSlots == null)
            return;

        for (int i = 0; i < codeSlots.Length; i++)
        {
            if (codeSlots[i] == null)
                continue;

            if (i < enteredSymbols.Count && activeCodeBox != null)
            {
                codeSlots[i].sprite =
                    activeCodeBox.GetSymbolSprite(enteredSymbols[i]);

                codeSlots[i].enabled = true;
            }
            else
            {
                codeSlots[i].sprite = null;
                codeSlots[i].enabled = false;
            }
        }
    }

}
