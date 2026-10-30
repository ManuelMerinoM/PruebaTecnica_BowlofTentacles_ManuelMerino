
using System.Collections;
using TMPro;
using UnityEngine;

public class MessageController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private TMP_Text messageText;

    [Header("Duration")]
    [SerializeField] private float messageDuration = 4f;

    private Coroutine messageCoroutine;

    private void Awake()
    {
        if (messagePanel != null)
            messagePanel.SetActive(false);
    }

    public void ShowMessage(string message)
    {
        if (messageCoroutine != null)
            StopCoroutine(messageCoroutine);

        messageCoroutine = StartCoroutine(
            ShowMessageRoutine(message)
        );
    }

    private IEnumerator ShowMessageRoutine(string message)
    {
        if (messagePanel != null)
            messagePanel.SetActive(true);

        if (messageText != null)
            messageText.text = message;

        yield return new WaitForSeconds(messageDuration);

        if (messagePanel != null)
            messagePanel.SetActive(false);

        messageCoroutine = null;
    }
}
