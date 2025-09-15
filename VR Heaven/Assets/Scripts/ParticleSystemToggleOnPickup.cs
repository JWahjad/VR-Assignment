using UnityEngine;

public class ParticleSystemToggleOnPickup : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystemToToggle;

    private void Awake()
    {
        if (particleSystemToToggle == null)
            particleSystemToToggle = GetComponentInChildren<ParticleSystem>();
    }

    // Call this when the object is picked up
    public void OnPickUp()
    {
        if (particleSystemToToggle != null)
            particleSystemToToggle.gameObject.SetActive(false);
    }

    // Call this when the object is released
    public void OnRelease()
    {
        if (particleSystemToToggle != null)
            particleSystemToToggle.gameObject.SetActive(true);
    }
}