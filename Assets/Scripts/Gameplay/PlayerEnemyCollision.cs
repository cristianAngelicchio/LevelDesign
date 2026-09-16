using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{

    /// <summary>
    /// Fired when a Player collides with an Enemy.
    /// </summary>
    /// <typeparam name="EnemyCollision"></typeparam>
    public class PlayerEnemyCollision : Simulation.Event<PlayerEnemyCollision>
    {
        public EnemyController enemy;
        public PlayerController player;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var willHurtEnemy = player.Bounds.center.y >= enemy.Bounds.max.y;

            if (willHurtEnemy)
            {
                var enemyHealth = enemy.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.Decrement();
                    if (!enemyHealth.IsAlive)
                    {
                        Schedule<EnemyDeath>().enemy = enemy;
                        player.Bounce(2);
                    }
                    else
                    {
                        player.Bounce(7);
                    }
                }
                else
                {
                    //CA: If it's a saw, it kills the player instantly.
                    if(enemy.IsSaw && !enemy.SawIsSlimed)
                    {
                        Schedule<PlayerDeath>();
                    }
                    else
                    {
                        if (!enemy.IsSaw)
                        {
                            Schedule<EnemyDeath>().enemy = enemy;
                            enemy.control.GetComponent<Animator>().SetBool("death", true);
                        }
                        else
                        {
                            enemy.GetComponent<Animator>().SetTrigger("Bounce");
                        }

                        if (!enemy.isBoss)
                            player.Bounce(4.5f);
                        else
                            player.Bounce(2f);
                    }
                }
            }
            else
            {
                if (!enemy.SawIsSlimed)
                    Schedule<PlayerDeath>();
            }
        }
    }
}