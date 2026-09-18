using Platformer.Gameplay;
using Platformer.Mechanics;
using UnityEngine;
using static Platformer.Core.Simulation;

public class EscapeBoat : MonoBehaviour
{
    public RisingWater water;
    private bool isActive = false;
    private float farDistance = 1.5f;

    void FixedUpdate()
    {

        if (water != null && water.transform.position.y > transform.position.y)
        {
            transform.position = new Vector3(
                transform.position.x,
                water.transform.position.y,
                transform.position.z
            );
        }

        if (water != null && water.isRising && isActive)
        {
            PlayerController player = FindFirstObjectByType<PlayerController>();

            if (player != null)
            {
                float distance = player.transform.position.y - water.transform.position.y;

                if (distance > farDistance)
                    water.risingSpeed = 5;
                else
                    water.risingSpeed = 0.3f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (water.isRising)
        {
            var p = collider.gameObject.GetComponent<PlayerController>();
            p.controlEnabled = false;
            water.risingSpeed = 0.3f;
            isActive = true;
        }
    }
}
