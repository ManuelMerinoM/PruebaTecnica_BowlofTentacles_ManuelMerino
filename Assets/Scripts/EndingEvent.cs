using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class EndingEvent : MonoBehaviour
{
    [Header("Jugador")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerInteractor playerInteractor;

    [Header("Puerta")]
    [SerializeField] private float waitBeforeVoice = 2f;
    [SerializeField] private float waitAfterVoice = 1.5f;

    [Header("Subtítulos")]
    [SerializeField] private MessageController messageController;

    [Header("Voz")]
    [SerializeField] private AudioSource voiceSource;
    [SerializeField] private AudioClip finalVoice;

    [Header("Fundido")]
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 2f;

    [Header("Pantalla final")]
    [SerializeField] private GameObject endingPanel;

    private bool endingStarted;

    private void Start()
    {

        if (endingPanel != null)
            endingPanel.SetActive(false);

        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = 0f;
            fadeImage.color = color;
            fadeImage.raycastTarget = false;
        }
    }

    public void BeginEnding(DoorInteractable door)
    {
        if (endingStarted) return;

        endingStarted = true;

        if (playerInteractor != null)
            playerInteractor.enabled = false;

        if (playerMovement != null)
            playerMovement.enabled = false;

        StartCoroutine(EndingSequence(door));
    }

    private IEnumerator EndingSequence(DoorInteractable door)
    {
        
        door.OpenDoor();

        yield return new WaitForSeconds(1f);

        voiceSource.PlayOneShot(door.closeSound);

        yield return new WaitForSeconds(waitBeforeVoice);

        if (messageController != null)
            messageController.ShowMessage("No te vayas todavía");

        if (voiceSource != null && finalVoice != null)
            voiceSource.PlayOneShot(finalVoice);

        float voiceDuration = finalVoice != null
            ? finalVoice.length
            : 2f;

        yield return new WaitForSeconds(voiceDuration);

        yield return StartCoroutine(FadeToBlack());

        yield return new WaitForSeconds(waitAfterVoice);

        

        //door.CloseDoor();


        if (endingPanel != null)
            endingPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private IEnumerator FadeToBlack()
    {
        if (fadeImage == null)
            yield break;

        fadeImage.raycastTarget = true;

        float elapsed = 0f;
        Color color = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / fadeDuration);
            fadeImage.color = color;

            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}