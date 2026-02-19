using System.Collections;
using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 0.2f;

    private Coroutine currentRoutine;

    public void Show()
    {
        if (currentRoutine != null) StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(Fade(1f, true));
    }

    public void Hide()
    {
        if (currentRoutine != null) StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(Fade(0f, false));
    }

    IEnumerator Fade(float targetAlpha, bool interactable)
    {
        float startAlpha = canvasGroup.alpha;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime; // IMPORTANTE: funciona en pausa
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
        canvasGroup.interactable = interactable;
        canvasGroup.blocksRaycasts = interactable;
    }
}
