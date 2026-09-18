using System.Collections;
using UnityEngine;
using Cinemachine;
using Platformer.Mechanics;

public class CameraConfinerTrigger : MonoBehaviour
{
    public CinemachineConfiner confiner;
    public Collider2D newConfiner;

    public float transitionDamping = 2.5f;
    public float normalDamping = 0.25f;
    public float dampingDuration = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
            confiner.m_BoundingShape2D = newConfiner;

            StartCoroutine(TemporaryDamping());
        }
    }

    private IEnumerator TemporaryDamping()
    {
        confiner.m_Damping = transitionDamping;

        float timer = 0f;

        while (timer < dampingDuration)
        {
            float t = timer / dampingDuration;

            confiner.m_Damping = Mathf.Lerp(
                transitionDamping,
                0f,
                t
            );

            timer += Time.deltaTime;
            yield return null;
        }

        confiner.m_Damping = 0f;
    }
}