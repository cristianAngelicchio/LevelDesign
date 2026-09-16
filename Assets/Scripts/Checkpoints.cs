using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    public Transform spawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spawnPoint.position = transform.position;
        }
    }
}
