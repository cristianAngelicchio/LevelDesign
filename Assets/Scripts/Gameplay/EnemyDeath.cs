using Platformer.Core;
using Platformer.Mechanics;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the health component on an enemy has a hitpoint value of  0.
    /// </summary>
    /// <typeparam name="EnemyDeath"></typeparam>
    public class EnemyDeath : Simulation.Event<EnemyDeath>
    {
        public EnemyController enemy;

        public override void Execute()
        {
            if (!enemy.isBoss)
            {
                //CA: ADD ANYTHING YOU WANT TO HAPPEN WHEN THE ENEMY DIES
                enemy.control.enabled = false;
                enemy.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                enemy.GetComponent<BoxCollider2D>().enabled = false;

                //CA:: I Spawn a slimebox on death.
                enemy.spawnSlimeCube();

                //CA:: No longer disables the collider, instead it changes rigidbody and collisionboxes.
                //enemy._collider.enabled = false;
                enemy.GetComponent<Rigidbody2D>().linearVelocityY = 0;
                if (enemy._audio && enemy.ouch)
                    enemy._audio.PlayOneShot(enemy.ouch);
            }
            else
                enemy.ActivateBoss();
        }
    }
}