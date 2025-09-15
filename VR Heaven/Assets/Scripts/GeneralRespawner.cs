using UnityEngine;

public class GeneralRespawner : MonoBehaviour
{
    public enum RespawnType
    {
        Golfball,
        Gate,
        Firework
    }

    [Header("Respawn Settings")]
    public RespawnType respawnType;

    public Transform player;
    public float resetDistance = 20f;

    private Vector3 resetPosition;
    private Quaternion resetRotation;
    private Vector3 resetScale;

    private void Awake()
    {
        // Set respawn transform based on type
        switch (respawnType)
        {
            case RespawnType.Golfball:
                resetPosition = new Vector3(1.983227f, 2.153145f, 10.104799f);
                resetRotation = Quaternion.identity;
                resetScale = new Vector3(0.15f, 0.15f, 0.15f);
                break;
            case RespawnType.Gate:
                resetPosition = new Vector3(13.45f, 0.610001f, -2.769997f);
                resetRotation = new Quaternion(0f, 0.890437f, 0f, 0.45510665f);
                resetScale = new Vector3(0.05f, 0.05f, 0.05f);
                break;
            case RespawnType.Firework:
                resetPosition = new Vector3(-7.638912f, 1.360001f, -1.598419f);
                resetRotation = Quaternion.identity;
                resetScale = Vector3.one;
                break;
        }
    }

    private void Update()
    {
        if (respawnType == RespawnType.Golfball)
        {
            if (transform.position.y < 0.5f)
            {
                Respawn();
            }
        }
        else if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance > resetDistance)
            {
                Respawn();
            }
        }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     Respawn();
    // }

    public void Respawn()
    {
        transform.position = resetPosition;
        transform.rotation = resetRotation;
        transform.localScale = resetScale;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}