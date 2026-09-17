using System.Collections;
using UnityEngine;
using Platformer.Mechanics;

public class Collectible : MonoBehaviour
{
    private Animator animator;
    private bool collected = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null && !collected)
        {
            collected = true;
            animator.SetTrigger("Collected");
            StartCoroutine(DisableAfterDelay());
        }
    }

    private IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(0.25f);
        gameObject.SetActive(false);
    }
}