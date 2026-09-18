using Cinemachine;
using Platformer.Mechanics;
using System.Collections;
using UnityEngine;

public class EscapeTrigger : MonoBehaviour
{
    public GameObject[] objectsToActivate;
    public GameObject[] objectsToDeActivate;

    public PlayerController player;

    public Animator object1Animator;
    public Animator object2Animator;

    public GameObject water;

    public GameObject cameraPivot;

    public CinemachineConfiner confiner;
    public PolygonCollider2D finalConfiner;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(PlayCinematic());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            Destroy(collision.gameObject);

            confiner.m_BoundingShape2D = finalConfiner;
            confiner.m_Damping = 0;

            StartCoroutine(PlayCinematic());
            //gameObject.SetActive(false);
        }
    }
    private Vector3 originalPosition;

    IEnumerator PlayCinematic()
    {
        foreach (GameObject obj in objectsToDeActivate)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(true);
        }

        // 1. Disable player
        player.controlEnabled = false;

        yield return new WaitForSeconds(1f);

        // 3. Start screen shake
        originalPosition = cameraPivot.transform.localPosition;
        StartCoroutine(Shake(2f, 0.5f));

        yield return new WaitForSeconds(2.5f);

        // 4. Speed up the two animations
        object1Animator.SetTrigger("SetActive");
        object2Animator.SetTrigger("SetActive");

        // 5. Wait for the cinematic
        yield return new WaitForSeconds(1.5f);

        // 6. Start the water
        water.GetComponent<RisingWater>().StartRising();

        // 7. Give control back
        player.controlEnabled = true;
    }


    public IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            Vector3 offset = Random.insideUnitSphere * magnitude;
            offset.z = 0f;

            cameraPivot.transform.localPosition = originalPosition + offset;

            elapsed += Time.deltaTime;
            yield return null;
        }

        cameraPivot.transform.localPosition = originalPosition;
    }
}
