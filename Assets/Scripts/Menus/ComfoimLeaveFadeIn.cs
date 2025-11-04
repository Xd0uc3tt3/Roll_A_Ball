using UnityEngine;

public class ComfoimLeaveFadeIn : MonoBehaviour
{
    public float delay = 2f;
    public float fadeDuration = 1f;

    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        StartCoroutine(FadeInAfterDelay());
    }

    private System.Collections.IEnumerator FadeInAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }
}

