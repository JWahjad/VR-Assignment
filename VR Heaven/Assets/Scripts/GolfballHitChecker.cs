using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GolfballHitChecker : MonoBehaviour
{
    [SerializeField] private string golfballTag = "Golfball";
    [SerializeField] private Material skyboxDay;   // day skybox
    [SerializeField] private Material skyboxNight; // night skybox
    [SerializeField] private float transitionDelay = 1f;
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 0.5f;

    private bool isTransitioning = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(golfballTag) && !isTransitioning)
        {
            StartSkyboxTransition();
        }
    }

    // testing
    public void StartSkyboxTransition()
    {
        if (!isTransitioning)
        {
            StartCoroutine(TransitionSkybox());
        }
    }

    private IEnumerator TransitionSkybox()
    {
        isTransitioning = true;

        // Fade to black
        yield return StartCoroutine(Fade(0f, 1f, fadeDuration));

        // Change skybox
        RenderSettings.skybox = skyboxNight;
        yield return new WaitForSeconds(transitionDelay);

        // Fade back to normal
        yield return StartCoroutine(Fade(1f, 0f, fadeDuration));

        isTransitioning = false;
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        Color color = fadeImage.color;
        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }
        fadeImage.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}
