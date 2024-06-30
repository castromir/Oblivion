using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FelicidadeInvestida : MonoBehaviour
{
    public float investidaVelocidade = 6f;
    public float investidaTempo      = 2f;

   /* float chargeSpeedDropMultiplier = 1f;
    chargeSpeed -= chargeSpeed* chargeSpeedDropMultiplier * Time.deltaTime;

            float hitDistance = 3f;
    RaycastHit2D raycastHit2D = Physics2D.Raycast(GetPosition(), chargeDir, hitDistance, 1 << GameAssets.i.playerLayer);
            if (raycastHit2D.collider != null) {
                Player player = raycastHit2D.collider.GetComponent<Player>();
                if (player != null) {
                    player.Damage(enemyMain.Enemy, .6f);
                    player.Knockback(chargeDir, 5f);
                    chargeSpeed = 60f;
                    chargeDir *= -1f;
                }
            }

            float chargeSpeedMinimum = 70f;
if (chargeSpeed < chargeSpeedMinimum)
{
    state = State.Normal;
    enemyMain.EnemyPathfindingMovement.Enable();
    chargeDelay = 1.5f;
    SetStateNormal();
    HideAim();
}
break;*/
}
