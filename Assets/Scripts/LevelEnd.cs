using UnityEngine;
using Platformer.Mechanics;

public class LevelEnd : MonoBehaviour
{
    public BlackScreen blackScreen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
            blackScreen.StartLevelEndTransition();
        }
    }
}