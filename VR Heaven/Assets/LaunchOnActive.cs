using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class LaunchOnActivate : MonoBehaviour
{
    public float launchForce = 30f;
    private Rigidbody rb;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    public void LaunchObject()
    {
        if (grabInteractable.isSelected)
        {
            // Use the new method signature with IXRSelectInteractable
            grabInteractable.interactionManager.CancelInteractableSelection(grabInteractable as UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable);
        }

        Vector3 launchDirection = (Vector3.up * 2f + Vector3.forward).normalized;
        rb.AddForce(launchDirection * launchForce, ForceMode.Impulse);
    }
}
