using UnityEngine;
using UnityEngine.XR;
using System.Collections;

public class LightFirework : MonoBehaviour
{
    [SerializeField] private ParticleSystem fireworkParticles, explosionParticles;
    [SerializeField] private float explosionDelay = 5.0f;
    [SerializeField] private GameObject fireworkModel;

    private bool isLit = false;

    void Update()
    {
        if (!isLit && (IsTriggerPressed(XRNode.LeftHand) || IsTriggerPressed(XRNode.RightHand)))
        {
            isLit = true;
            if (fireworkParticles != null)
                fireworkParticles.Play();
            StartCoroutine(ExplodeAfterDelay(explosionDelay));
        }
    }

    private IEnumerator ExplodeAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (explosionParticles != null)
            explosionParticles.Play();

        fireworkParticles.Stop();
        fireworkModel.SetActive(false);
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