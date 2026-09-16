using Platformer.Gameplay;
using Platformer.Mechanics;
using UnityEngine;
using static Platformer.Core.Simulation;

public class Spikes : MonoBehaviour
{
    public bool SpikeIsSlimed;
    public Spikes[] neighbouringSpikes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnCollisionEnter2D(Collision2D collision)
    {
        var p = collision.gameObject.GetComponent<PlayerController>();
        if (p != null && !SpikeIsSlimed)
        {
            var ev = Schedule<PlayerDeath>(0);
        }
        else if(SpikeIsSlimed)
        {
            GetComponent<Animator>().SetTrigger("Bounce");
            p.Bounce(3.65f);
        }


        //If instead, it collides with the Slime Box..
        if (!SpikeIsSlimed && collision.gameObject.CompareTag("Pushable"))
        {
            // Slime this spike
            SlimeSpike();

            // Slime direct neighbours only
            foreach (Spikes neighbour in neighbouringSpikes)
            {
                if (neighbour != null)
                {
                    neighbour.SlimeSpike();
                }
            }

            Destroy(collision.gameObject);
        }
    }

    public void SlimeSpike()
    {
        if (SpikeIsSlimed)
            return;

        SpikeIsSlimed = true;

        Animator animator = GetComponent<Animator>();

        if (animator != null)
        {
            animator.SetBool("IsSlimed", true);
        }
    }

    public void ResetSpike()
    {
        SpikeIsSlimed = false;

        Animator animator = GetComponent<Animator>();

        if (animator != null)
        {
            animator.SetBool("IsSlimed", false);
        }
    }
}
