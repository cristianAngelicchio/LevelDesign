using Platformer.Gameplay;
using Platformer.Mechanics;
using UnityEngine;
using static Platformer.Core.Simulation;

public class EscapeBoat : MonoBehaviour
{
    public RisingWater water;

    void FixedUpdate()
    {

        if (water != null && water.transform.position.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x,water.transform.position.y, transform.position.z);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (water.isRising)
        {
            var p = collider.gameObject.GetComponent<PlayerController>();
            p.controlEnabled = false;
            water.risingSpeed = 0.3f;
        }
    }
}
