using UnityEngine;
using System.Collections;

public class NoteEvent : MonoBehaviour
{
    [Header("Pasos del pasillo")]
    [SerializeField] private AudioSource footstepsSource;
    [SerializeField] private AudioClip footstepSound;
    [SerializeField] private float silenceDuration = 2f;
    [SerializeField] private float stepInterval = 0.65f;

    [Header("Silla")]
    [SerializeField] private Transform chair;
    [SerializeField] private Transform chairTarget;
    [SerializeField] private float chairMoveDuration = 0.1f;

    private bool hasPlayed;

    public void PlayOnce()
    {
        if (hasPlayed) return;

        hasPlayed = true;
        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {

        StartCoroutine(MoveChair());
      
        yield return new WaitForSeconds(silenceDuration);

        PlayFootstep();

        yield return new WaitForSeconds(stepInterval);
        PlayFootstep();

        yield return new WaitForSeconds(stepInterval);
        PlayFootstep();
    }

    private void PlayFootstep()
    {
        if (footstepsSource != null && footstepSound != null)
            footstepsSource.PlayOneShot(footstepSound);
    }

    private IEnumerator MoveChair()
    {
        if (chair == null || chairTarget == null)
            yield break;

        Vector3 startPosition = chair.position;
        Quaternion startRotation = chair.rotation;

        Vector3 targetPosition = chairTarget.position;
        Quaternion targetRotation = chairTarget.rotation;

        float elapsed = 0f;

        while (elapsed < chairMoveDuration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / chairMoveDuration);

            chair.position = Vector3.Lerp(startPosition, targetPosition, t);
            chair.rotation = Quaternion.Slerp(startRotation, targetRotation, t);

            yield return null;
        }

        chair.position = targetPosition;
        chair.rotation = targetRotation;
    }
}