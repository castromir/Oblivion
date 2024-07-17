﻿using System.Collections;
using UnityEngine;

public class FelicidadeEstado : MonoBehaviour
{
    private BossFelicidade felicidade;
    private GameObject player;
    private BossVida bossVida;
    private FelicidadeLaser laser;

    public SpriteRenderer bossSR;
    public Color avisoCor = Color.red;
    public float avisoDuracao = 1f;
    private Color corOriginal;

    private float velocidade = 3f;
    private float investidaVelocidade = 8f;
    private float investidaTempo = 2f; // Duração da investida
    static private float investidaDelayTempo = 1.5f; // Inicialização do delay
    private float investidaDelay = investidaDelayTempo; // Temporizador da investida
    static private float prepararInvestidaDelayTempo = 1f; // Delay antes da investida
    private float prepararInvestidaDelay = prepararInvestidaDelayTempo;
    private float investidaTimer = 0f;

    static public float laserDelayTempo = 1.5f; // Tempo de delay do laser
    private readonly float laserDelay = laserDelayTempo;

    private Vector3 direcao;

    enum BossEstado
    {
        Inativo,
        Padrao,
        PreparandoInvestida,
        Investida,
        Laser
    }

    BossEstado estado = BossEstado.Padrao;

    void Awake()
    {
        felicidade = GetComponent<BossFelicidade>();
        player = felicidade.player;
        bossVida = GetComponent<BossVida>();
        laser = GetComponent<FelicidadeLaser>();
        laser.SetPlayerTransform(player.transform);  // Configurar o playerTransform no FelicidadeLaser
    }

    void Start()
    {
        bossSR = GetComponent<SpriteRenderer>();
        corOriginal = bossSR.color;
    }

    private void Update()
    {
        if (bossVida.vidaAtual <= bossVida.GetVidaMaxima() / 4)
        {
            velocidade = 6f;
            laserDelayTempo = 0.25f;
            investidaDelayTempo = 0.5f;
            prepararInvestidaDelayTempo = 0.3f;
        }
        else if (bossVida.vidaAtual <= bossVida.GetVidaMaxima() / 1.5)
        {
            velocidade = 4.5f;
            laserDelayTempo = 0.8f;
            investidaDelayTempo = 1f;
            prepararInvestidaDelayTempo = 0.5f;
        }

        if (bossVida.isDead) return; // Verifica se o boss está morto

        switch (estado)
        {
            case BossEstado.Inativo:
                break;

            case BossEstado.Padrao:
                investidaDelay -= Time.deltaTime;
                Vector3 posicaoPlayer = player.transform.position;
                direcao = (posicaoPlayer - RetornarPosicao()).normalized;

                if (investidaDelay > 0 && !laser.ConsegueLaser()) // investida e laser em recarga, boss apenas se move
                {
                    felicidade.MoverParaPosicao(player.transform.position, direcao, velocidade);
                }
                else if (laser.ConsegueLaser()) // investida em recarga, laser pronto
                {
                    // Ativar o estado de disparo do laser
                    StartCoroutine(EstadoLaser());
                }
                else
                {
                    if (ConsegueInvestida(posicaoPlayer, player)) // só investida
                    {
                        estado = BossEstado.PreparandoInvestida;
                        StartCoroutine(AvisoInvestida());
                    }
                    else
                    {
                        // Caminhando em direção ao jogador
                        felicidade.MoverParaPosicao(player.transform.position, direcao, velocidade);
                    }
                }
                break;

            case BossEstado.PreparandoInvestida:
                prepararInvestidaDelay -= Time.deltaTime;
                if (prepararInvestidaDelay <= 0)
                {
                    investidaVelocidade = 10f; // Velocidade inicial da investida
                    investidaTimer = investidaTempo; // Reinicia o temporizador da investida
                    estado = BossEstado.Investida;
                }
                break;

            case BossEstado.Investida:
                if (investidaTimer > 0)
                {
                    transform.position += direcao * investidaVelocidade * Time.deltaTime; // Atualizando posição durante a investida
                    felicidade.Renderizar(direcao, true);

                    float investidaVelocidadeReducaoMultiplicador = 1f;
                    investidaVelocidade -= investidaVelocidade * investidaVelocidadeReducaoMultiplicador * Time.deltaTime;

                    investidaTimer -= Time.deltaTime;

                    float acertoDistancia = 3f;
                    RaycastHit2D raycastHit2D = Physics2D.Raycast(RetornarPosicao(), direcao, acertoDistancia);
                    if (raycastHit2D.collider != null)
                    {
                        Player player = raycastHit2D.collider.GetComponent<Player>();
                        if (player != null)
                        {
                            investidaVelocidade = 60f;
                            direcao *= -1f;
                        }
                    }
                }
                else
                {
                    estado = BossEstado.Padrao;
                    investidaDelay = investidaDelayTempo;
                    DefinirEstadoNormal();
                }
                break;
        }
    }

    private IEnumerator EstadoLaser()
    {
        estado = BossEstado.Laser;

        int numeroDeDisparos = 1;

        if (bossVida.vidaAtual <= bossVida.GetVidaMaxima() / 5)
        {
            numeroDeDisparos = 3;
        }
        else if (bossVida.vidaAtual <= bossVida.GetVidaMaxima() / 2)
        {
            numeroDeDisparos = 2;
        }

        for (int i = 0; i < numeroDeDisparos; i++)
        {
            yield return new WaitForSeconds(laserDelay);
            laser.DispararLaser();
            Debug.Log("Laser disparado!");
        }

        estado = BossEstado.Padrao;
    }

    private bool ConsegueInvestida(Vector3 posicaoAlvo, GameObject alvoGameObject)
    {
        float distanciaAlvo = Vector3.Distance(RetornarPosicao(), posicaoAlvo);

        float investidaDistanciaMax = 5f;
        if (distanciaAlvo > investidaDistanciaMax)
        {
            return false;
        }

        return true;
    }

    private void DefinirEstadoNormal()
    {
        estado = BossEstado.Padrao;
    }

    public Vector3 RetornarPosicao()
    {
        return transform.position;
    }

    private IEnumerator AvisoInvestida()
    {
        bossSR.color = avisoCor;
        yield return new WaitForSeconds(avisoDuracao);
        bossSR.color = corOriginal;

        prepararInvestidaDelay = prepararInvestidaDelayTempo;
    }
}
