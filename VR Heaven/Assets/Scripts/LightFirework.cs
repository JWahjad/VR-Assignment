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
    [SerializeField] private float fuzeLightDelay = 1.0f;  // Delay for fuze to light before launching

    private bool isLit = false;
    private bool isLaunched = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    void Update()
    {
        // Trigger firework lighting and launching
        if (!isLit && (IsTriggerPressed(XRNode.LeftHand) || IsTriggerPressed(XRNode.RightHand)))
        {
            isLit = true;
            if (fireworkParticles != null)
                fireworkParticles.Play();
            StartCoroutine(LightFuzeAndLaunch());
        }
    }

    private IEnumerator LightFuzeAndLaunch()
    {
        // Light the fuze first
        yield return new WaitForSeconds(fuzeLightDelay);  // Delay before launching

        // Now launch the firework
        if (!isLaunched)
        {
            LaunchObject();
            isLaunched = true;
            yield return new WaitForSeconds(1f); // wait a bit before explosion
        }

        // Wait for the explosion delay after launch
        yield return new WaitForSeconds(explosionDelay);

        // Explode the firework
        if (explosionParticles != null)
            explosionParticles.Play();

        fireworkParticles.Stop();
        fireworkModel.SetActive(false);
    }

    private void LaunchObject()
    {
        if (grabInteractable.isSelected)
        {
            // Use the new method signature with IXRSelectInteractable
            grabInteractable.interactionManager.CancelInteractableSelection(grabInteractable as UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable);
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
}
