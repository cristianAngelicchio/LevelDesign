using UnityEngine;

public class EscapeTrigger : MonoBehaviour
{
    public GameObject[] objectsToActivate;
    public GameObject[] objectsToDeActivate;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            gameObject.SetActive(false);
            foreach (GameObject obj in objectsToDeActivate)
            {
                obj.SetActive(false);
            }

            foreach (GameObject obj in objectsToActivate)
            {
                obj.SetActive(true);
            }

            Destroy(collision.gameObject);
        }
    }
}
