using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossVida : MonoBehaviour
{
    [SerializeField] private int vidaMaxima;

    public int vidaAtual;

    public SpriteRenderer bossSR;
    public IABoss bossMovimento;

    private void Start()
    {
        vidaAtual = vidaMaxima;
    }

    public void ReceberDano(int quantidadeDano)
    {
        vidaAtual -= quantidadeDano;

        if (vidaAtual <= 0) 
        {
            bossSR.enabled        = false;
            bossMovimento.enabled = false;
        }
    }
}
