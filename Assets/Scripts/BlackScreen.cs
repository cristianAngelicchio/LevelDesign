using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Platformer.Core;

public class BlackScreen : MonoBehaviour
{
    Image image;

    public float fadeToBlackTime = 0.2f;
    public float waitWhileBlack = 0.2f;
    public float fadeFromBlackTime = 0.2f;

    void Awake()
    {
        image = GetComponent<Image>();

        Color color = image.color;
        color.a = 0f;
        image.color = color;
    }

    public void StartDeathTransition()
    {
        StartCoroutine(DeathTransition());
    }

    IEnumerator DeathTransition()
    {
        yield return StartCoroutine(Fade(1f, fadeToBlackTime));

        yield return new WaitForSeconds(waitWhileBlack);

        Simulation.Schedule<Platformer.Gameplay.PlayerSpawn>();

        yield return StartCoroutine(Fade(0f, fadeFromBlackTime));
    }

    IEnumerator Fade(float targetAlpha, float duration)
    {
        Color color = image.color;
        float startAlpha = color.a;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;

            color.a = Mathf.Lerp(
                startAlpha,
                targetAlpha,
                time / duration
            );

            image.color = color;

            yield return null;
        }

        color.a = targetAlpha;
        image.color = color;
    }
}