using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossVida : MonoBehaviour
{
    [SerializeField] private int vidaMaxima;

    public int vidaAtual;
    public bool isDead = false;

    public SpriteRenderer bossSR;
    public IABoss bossMovimento;


    private Collider2D[] colliders;

    private void Start()
    {
        vidaAtual = vidaMaxima;
        colliders = GetComponents<Collider2D>();
    }

    public void ReceberDano(int quantidadeDano)
    {
        vidaAtual -= quantidadeDano;

        if (vidaAtual <= 0)
        {
            isDead = true;
            bossSR.enabled = false;
            bossMovimento.enabled = false;

            foreach (var collider in colliders)
            {
                collider.enabled = false;
            }
        }
    }
    public int GetVidaMaxima()
    {
        return vidaMaxima;
    }
}
