using UnityEngine;

public class SummonLargeShrine : MonoBehaviour
{
    [SerializeField] private string smallShrineTag = "ShrineObject";
    [SerializeField] private GameObject largeShrine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(smallShrineTag))
        {
            Destroy(other.gameObject);

            largeShrine.SetActive(true);
        }
    }
}
