﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerVida : MonoBehaviour
{
    [SerializeField] public int vidaMaxima = 3;
    [SerializeField] private float repulsaoQuantidade = 10f;
    [SerializeField] private float danoTempoRecuperacao = 1f;

    public int vidaAtual;
    private bool consegueReceberDano = true;
    private Repulsao repulsao;


    public SpriteRenderer playerSR;
    public SpriteRenderer playerLivroSR;
    public IsometricPlayerMovementController playerMovimento;
    public PlayerMiraFeitico playerLivroAcoes;

    private void Awake()
    {
        repulsao = GetComponent<Repulsao>();
    }

    private void Start()
    {
        vidaAtual = vidaMaxima;
    }

    public void ReceberDano()
    {
        if (consegueReceberDano)
        {
            vidaAtual -= 1;
            if (vidaAtual <= 0)
            {
                playerSR.enabled = false;
                playerLivroSR.enabled = false;
                playerMovimento.enabled = false;
                playerLivroAcoes.enabled = false;
                StartCoroutine(HandleDeath());
            }
            else
            {
                StartCoroutine(TempoDeInvulnerabilidade());
            }
        }
    }

    private IEnumerator TempoDeInvulnerabilidade()
    {
        consegueReceberDano = false;
        yield return new WaitForSeconds(danoTempoRecuperacao);
        consegueReceberDano = true;
    }

    private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Lobby");
    }
}
