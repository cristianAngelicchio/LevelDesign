using Platformer.Mechanics;
using System.Collections;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float runSpeed = 0.15f;
    public float runDuration = 1f;
    public Transform[] nextPosition;

    public float jumpDuration = 0.5f;
    public float jumpHeight = 0.5f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private PlayerController player;
    private bool isRunning = false;
    private int triggerInt = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = FindFirstObjectByType<PlayerController>();
    }

    private void FixedUpdate()
    {
        if (player == null)
            return;

        float distanceX = Mathf.Abs(player.transform.position.x - transform.position.x);
        float distanceY = Mathf.Abs(player.transform.position.y - transform.position.y);

        // First detection: NPC at nextPosition[0]
        if (triggerInt == 2)
        {
            if (distanceX <= 1.5f && distanceY >= 1.9f)
            {
                triggerInt = 3;

                if (nextPosition != null && nextPosition.Length > 1)
                {
                    spriteRenderer.flipX = false;
                    transform.position = nextPosition[1].position;
                }
            }
        }

        // Second detection: NPC at nextPosition[1]
        else if (triggerInt == 3)
        {
            if (distanceX <= 0.25f && distanceY <= 1.25f)
            {
                triggerInt = 4;
                StartCoroutine(Escape());
            }
        }
    }

    private IEnumerator Escape()
    {
        // Jump toward the first wall
        yield return StartCoroutine(JumpToPosition(nextPosition[2].position));

        // Face the other direction
        spriteRenderer.flipX = !spriteRenderer.flipX;

        // Jump toward the second wall
        yield return StartCoroutine(JumpToPosition(nextPosition[3].position));

        // Disappear
        gameObject.SetActive(false);
    }

    private IEnumerator JumpToPosition(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;

        float timer = 0f;

        while (timer < jumpDuration)
        {
            float progress = timer / jumpDuration;

            Vector3 position = Vector3.Lerp(startPosition, targetPosition, progress);

            // Arc upward during the jump
            position.y += Mathf.Sin(progress * Mathf.PI) * jumpHeight;

            transform.position = position;

            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }

    public void StartRunning()
    {
        triggerInt = 1;
        StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        yield return new WaitForSeconds(2f);

        isRunning = true;
        animator.SetBool("IsRunning", true);

        // Face right
        spriteRenderer.flipX = false;

        float timer = 0f;

        while (timer < runDuration)
        {
            transform.position += Vector3.right * runSpeed * Time.deltaTime;

            timer += Time.deltaTime;
            yield return null;
        }

        // Stop running
        isRunning = false;
        animator.SetBool("IsRunning", false);

        // Teleport to next position
        if (nextPosition != null)
        {
            spriteRenderer.flipX = true;
            transform.position = nextPosition[0].position;
            triggerInt = 2;
        }
    }
}