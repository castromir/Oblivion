using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DisparoArcanoRaycast
{
   public static void Disparar(Vector3 posicaoInicial, Vector3 posicaoFinal, int dano)
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(posicaoInicial, posicaoFinal);

        if (raycastHit2D.collider != null)
        { 
            Alvo alvo = raycastHit2D.collider.GetComponent<Alvo>();
            if (alvo != null)
            {
                //Disparo acertou um alvo!
                alvo.gameObject.GetComponent<BossVida>().ReceberDano(dano);
                UtilsClass.ShakeCamera(0.1f, .1f);
            }
        }
    }
}
