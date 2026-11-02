
using UnityEngine;

public class CodeBoxLock : MonoBehaviour
{
    [Header("Images")]
    [Tooltip("Nueve sprites, uno para cada botón. El índice será su ID.")]
    [SerializeField] private Sprite[] symbolSprites = new Sprite[3];

    [Header("Combination")]
    [Tooltip("IDs de los tres símbolos en el orden correcto.")]
    [SerializeField] private int[] correctSymbols = { 2, 0, 1 };

    [Header("Opening")]
    [SerializeField] private Animator boxAnimator;
    [SerializeField] private string openTrigger = "Open";
    [SerializeField] private GameObject objectInside;

    [Header("Error Sound")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip failureSound;

    [Header("Mensaje de error")]
    [SerializeField] private MessageController messageController;
    [TextArea]
    [SerializeField]
    private string message =
        "Asi no era la rutina de mama";

    [Header("Collider de interacción")]
    [SerializeField] private Collider interactionCollider;
    public Collider InteractionCollider => interactionCollider;

    public bool IsOpen { get; private set; }

    public int CodeLength => correctSymbols.Length;

    public Sprite GetSymbolSprite(int id)
    {
        if (id < 0 || id >= symbolSprites.Length)
            return null;

        return symbolSprites[id];
    }

    public bool TryOpen(int[] enteredSymbols)
    {
        if (IsOpen)
            return true;

        bool correct = enteredSymbols.Length == correctSymbols.Length;

        if (correct)
        {
            for (int i = 0; i < correctSymbols.Length; i++)
            {
                if (enteredSymbols[i] != correctSymbols[i])
                {
                    correct = false;
                    break;
                }
            }
        }

        if (!correct)
        {
            if (messageController != null)
                messageController.ShowMessage(message);

            if (audioSource != null && failureSound != null)
                audioSource.PlayOneShot(failureSound);

            return false;
        }

        IsOpen = true;

        if (interactionCollider != null)
            interactionCollider.enabled = false;

        if (boxAnimator != null)
            boxAnimator.SetTrigger(openTrigger);

        

        return true;
    }
}
