using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using Platformer.Mechanics;

public class SecretAreaTrigger : MonoBehaviour
{
    public GameObject hiddenAreas;
    public float fadeDuration = 0.5f;

    private Coroutine fadeCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
            StartFade(0f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
            StartFade(1f);
        }
    }

    private void StartFade(float targetAlpha)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeAreas(targetAlpha));
    }

    private IEnumerator FadeAreas(float targetAlpha)
    {
        Tilemap[] tilemaps = hiddenAreas.GetComponentsInChildren<Tilemap>();

        float startAlpha = tilemaps.Length > 0 ? tilemaps[0].color.a : 1f;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeDuration);

            foreach (Tilemap tilemap in tilemaps)
            {
                Color color = tilemap.color;
                color.a = alpha;
                tilemap.color = color;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        foreach (Tilemap tilemap in tilemaps)
        {
            Color color = tilemap.color;
            color.a = targetAlpha;
            tilemap.color = color;
        }

        fadeCoroutine = null;
    }    
}