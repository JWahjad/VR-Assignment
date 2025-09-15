using UnityEngine;

public class GolfballResetTrigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform golfball, player;

    [Header("Reset Settings")]
    [SerializeField] private float resetDistance = 20f;

    private Vector3 resetPosition = new Vector3(1.983227f, 1.153145f, 14.004799f);
    private Quaternion resetRotation = Quaternion.identity;
    private Vector3 resetScale = new Vector3(0.15f, 0.15f, 0.15f);

    private void Update()
    {
        if (golfball != null && player != null)
        {
            float distance = Vector3.Distance(golfball.position, player.position);
            if (distance > resetDistance)
            {
                ResetGolfball();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (golfball != null && other.transform == golfball)
        {
            ResetGolfball();
        }
    }

    private void ResetGolfball()
    {
        golfball.position = resetPosition;
        golfball.rotation = resetRotation;
        golfball.localScale = resetScale;

        Rigidbody rb = golfball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}