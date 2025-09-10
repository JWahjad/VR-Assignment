using UnityEngine;

public class GolfballHitChecker : MonoBehaviour
{
    [SerializeField] private string golfballTag = "Golfball";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(golfballTag))
        {
            Debug.Log("Golfball hit the target!");
        }
    }
}
