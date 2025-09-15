using UnityEngine;
using UnityEngine.XR;
using System.Collections;


public class LightFirework : MonoBehaviour
{
    [Header("Launch Settings")]
    public float launchForce = 30f;
    private Rigidbody rb;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    [Header("Firework Effects")]
    [SerializeField] private ParticleSystem fireworkParticles, explosionParticles;
    [SerializeField] private float explosionDelay = 5.0f;
    [SerializeField] private GameObject fireworkModel;
    [SerializeField] private float fuzeLightDelay = 1.0f;
    
    [Header("Audio Settings")]
    [SerializeField] private AudioSource fuseAudio;
    [SerializeField] private AudioSource launchAudio;
    [SerializeField] private AudioSource explosionAudio;
    [SerializeField] private AudioSource fadeAudio;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private bool isLit = false;
    private bool isLaunched = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        if (grabInteractable.isSelected && !isLit && (IsTriggerPressed(XRNode.LeftHand) || IsTriggerPressed(XRNode.RightHand)))
        {
            isLit = true;
            if (fireworkParticles != null)
                fireworkParticles.Play();
            StartCoroutine(LightFuzeAndLaunch());
        }
    }

    private IEnumerator LightFuzeAndLaunch()
    {
        if (fuseAudio != null) fuseAudio.Play();
        yield return new WaitForSeconds(fuzeLightDelay);


        if (!isLaunched)
        {
            LaunchObject();
            isLaunched = true;
            if (launchAudio != null) launchAudio.Play();
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(explosionDelay);

        if (explosionParticles != null)
            explosionParticles.Play();

        if (explosionAudio != null)
            explosionAudio.Play();

        yield return new WaitForSeconds(0.3f);
        if (fadeAudio != null)
            fadeAudio.Play();

        fireworkParticles.Stop();
        fireworkModel.SetActive(false);

        // Destroy rocket after explosion
        yield return new WaitForSeconds(2.5f);
        // Destroy(gameObject);

        // Respawn new rocket
        ResetFirework();
        // Debug.Log("Respawning firework...");
        // if (fireworkPrefab != null)
        // {
        //     Debug.Log("Instantiating new firework at original position.: " + fireworkPrefab);
        //     Instantiate(fireworkPrefab, originalPosition, originalRotation);
        // }
    }

    private void LaunchObject()
    {
        if (grabInteractable.isSelected)
        {
            grabInteractable.interactionManager.CancelInteractableSelection(
                grabInteractable as UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable
            );
        }

        Vector3 launchDirection = (Vector3.up * 2f + Vector3.forward).normalized;
        rb.AddForce(launchDirection * launchForce, ForceMode.Impulse);
    }

    private bool IsTriggerPressed(XRNode node)
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(node);
        if (device.isValid)
        {
            bool pressed = false;
            if (device.TryGetFeatureValue(CommonUsages.triggerButton, out pressed) && pressed)
                return true;
        }
        return false;
    }

    private void ResetFirework()
    {
       // Reset internal states
        isLit = false;
        isLaunched = false;

        // Reset model and particle systems
        fireworkModel.SetActive(true);
        if (fireworkParticles != null) fireworkParticles.Stop();
        if (explosionParticles != null) explosionParticles.Stop();

        // Reset physics
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        // Stop any playing audio
        if (fuseAudio != null) fuseAudio.Stop();
        if (launchAudio != null) launchAudio.Stop();
        if (explosionAudio != null) explosionAudio.Stop();
        if (fadeAudio != null) fadeAudio.Stop();
    }
}
