using Platformer.Gameplay;
using Platformer.Mechanics;
using UnityEngine;
using static Platformer.Core.Simulation;

public class Spikes : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnCollisionEnter2D(Collision2D collision)
    {
        var p = collision.gameObject.GetComponent<PlayerController>();
        if (p != null)
        {
            var ev = Schedule<PlayerDeath>(0);
        }
    }
}
