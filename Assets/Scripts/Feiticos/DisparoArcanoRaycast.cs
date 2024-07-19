using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DisparoArcanoRaycast
{
    public static void Disparar(Vector3 posicaoInicial, Vector3 direcao, float distanciaMaxima, int dano)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(posicaoInicial, direcao, distanciaMaxima);

        foreach (RaycastHit2D hit in hits)
        {
            Alvo alvo = hit.collider.GetComponent<Alvo>();
            if (alvo != null)
            {
                // Disparo acertou um alvo!
                alvo.gameObject.GetComponent<BossVida>().ReceberDano(dano);
                UtilsClass.ShakeCamera(0.1f, .035f);
                break; // Para de verificar após acertar o alvo
            }
        }
    }
}
