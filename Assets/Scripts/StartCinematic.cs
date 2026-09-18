using System.Collections;
using UnityEngine;
using Platformer.Mechanics;

public class IntroSequence : MonoBehaviour
{
    public Transform cameraConfiner;
    public NPCController npc;
    public PlayerController player;

    public float startY = 7.5f;
    public float targetY = 5.8f;
    public float cameraMoveDuration = 3f;

    private void Start()
    {
        StartCoroutine(PlayIntro());
    }

    private IEnumerator PlayIntro()
    {
        // Make sure the player can't move during the intro.
        player.controlEnabled = false;

        // Move camera confiner down.
        Vector3 startPosition = cameraConfiner.position;
        Vector3 targetPosition = startPosition;
        targetPosition.y = targetY;

        float timer = 0f;

        while (timer < cameraMoveDuration)
        {
            float t = timer / cameraMoveDuration;

            cameraConfiner.position = Vector3.Lerp(
                startPosition,
                targetPosition,
                t
            );

            timer += Time.deltaTime;
            yield return null;
        }

        cameraConfiner.position = targetPosition;

        // Camera is now looking at the player.
        npc.StartRunning();

        // Wait for the NPC's intro movement to finish.
        yield return new WaitForSeconds(1.5f + npc.runDuration);

        // Wake player up.
        player.GetComponent<Animator>().SetBool("Spawn", true);
        yield return new WaitForSeconds(0.75f);
        player.controlEnabled = true;
        //gameObject.SetActive(false);
    }
}