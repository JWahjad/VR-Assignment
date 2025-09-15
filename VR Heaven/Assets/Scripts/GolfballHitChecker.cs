using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GolfballHitChecker : MonoBehaviour
{
    [SerializeField] private string golfballTag = "Golfball";
    [SerializeField] private Material skyboxDay, skyboxNight;
    [SerializeField] private float transitionDelay = 1f, fadeDuration = 0.5f;
    [SerializeField] private Image fadeImage;
    [SerializeField] private Light directionalLight;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameObject fireworksEffect, godRays;

    private bool isTransitioning = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(golfballTag) && !isTransitioning)
        {
            Destroy(other.gameObject);
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
        Debug.Log("Transition started");
        isTransitioning = true;

        yield return StartCoroutine(Fade(0f, 1f, fadeDuration));

        if (directionalLight != null)
            directionalLight.color = Color.black;

        if (godRays != null)
            godRays.SetActive(false);

        RenderSettings.skybox = skyboxNight;

        if (audioManager != null)
        {
            Debug.Log(audioManager);
            audioManager.PlayFireworks();

            if (fireworksEffect != null)
                fireworksEffect.SetActive(true);
        }

        Debug.Log("Waiting");
        yield return new WaitForSeconds(transitionDelay);
        Debug.Log("Waiting finished");

        yield return StartCoroutine(Fade(1f, 0f, fadeDuration));
        if (fadeImage != null)
            fadeImage.gameObject.SetActive(false);

        isTransitioning = false;
        Debug.Log("Transition finished");
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        Debug.Log("Fading");
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
        Debug.Log("Fading finished");
    }
}