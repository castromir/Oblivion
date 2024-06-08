using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alvo : MonoBehaviour
{
    private static List<Alvo> alvoLista;

    public static Alvo RetornarMaisProximo(Vector3 posicao, float distanciaMaxima)
    {
        Alvo maisProximo = null;

        foreach (Alvo alvo in alvoLista)
        {
            if (Vector3.Distance(posicao, alvo.RetornarPosicao()) <= distanciaMaxima)
            {
                if (maisProximo == null)
                {
                    maisProximo = alvo;
                }
                else
                {
                    if (Vector3.Distance(posicao, alvo.RetornarPosicao()) < Vector3.Distance(posicao, maisProximo.RetornarPosicao()))
                    {
                        maisProximo = alvo;
                    }


                }
            }
        }
        return maisProximo;
    }

    private void Awake()
    {
        if (alvoLista == null) alvoLista = new List<Alvo>();

        alvoLista.Add(this);
    }

    public Vector3 RetornarPosicao()
    {
        return transform.position;
    }
}

