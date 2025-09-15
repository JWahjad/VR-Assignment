using UnityEngine;

public class SummonLargeShrine : MonoBehaviour
{
    [SerializeField] private string smallShrineTag = "ShrineObject";
    [SerializeField] private GameObject largeShrine;
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private CameraShake cameraShake;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(smallShrineTag))
        {
            Destroy(other.gameObject);

            largeShrine.SetActive(true);
            StartCoroutine(MoveShrineUp(largeShrine.transform));
        }
    }

    private System.Collections.IEnumerator MoveShrineUp(Transform shrineTransform)
    {
        // Start screenshake
        if (cameraShake != null)
            StartCoroutine(cameraShake.Shake(5.5f, 0.1f));

        Vector3 startPos = shrineTransform.position;
        Vector3 targetPos = new Vector3(startPos.x, 0.5f, startPos.z);

        while (Mathf.Abs(shrineTransform.position.y - 0.5f) > 0.01f)
        {
            shrineTransform.position = Vector3.MoveTowards(
                shrineTransform.position,
                targetPos,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        shrineTransform.position = targetPos;
    }
}