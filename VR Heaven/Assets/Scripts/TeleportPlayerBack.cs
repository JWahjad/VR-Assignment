using UnityEngine;

public class TeleportPlayerBack : MonoBehaviour
{
    [SerializeField] string targetTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            other.transform.position = new Vector3(0, 2, 0);
        }
    }
}
