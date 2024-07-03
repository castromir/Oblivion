using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FelicidadeEstado : MonoBehaviour
{
    private IABoss felicidade;
    private GameObject player;
    private BossVida bossVida;

    public SpriteRenderer bossSR;
    public Color avisoCor = Color.red;
    public float avisoDuracao = 1f;
    private Color corOriginal;

    private float velocidade = 2f;

    private float investidaVelocidade = 6f;
    private float investidaTempo = 2f; // Duração da investida
    private float investidaDelay = 1.5f; // Inicialização do delay
    private float investidaTimer = 0f; // Temporizador da investida
    private float prepararInvestidaDelay = 1f; // Delay antes da investida
    private Vector3 investidaDirecao;

    enum BossEstado
    {
        Inativo,
        Padrao,
        PreparandoInvestida,
        Investida
    }

    BossEstado estado = BossEstado.Padrao;

    void Awake()
    {
        felicidade = GetComponent<IABoss>();
        player = felicidade.player;
        bossVida = GetComponent<BossVida>(); // Inicialização da referência
    }

    void Start()
    {
        bossSR = GetComponent<SpriteRenderer>();
        corOriginal = bossSR.color;
    }

    private void Update()
    {
        if (bossVida.isDead) return; // Verifica se o boss está morto

        switch (estado)
        {
            case BossEstado.Inativo:
                break;

            case BossEstado.Padrao:

                investidaDelay -= Time.deltaTime;
                Vector3 posicaoPlayer = player.transform.position;

                if (investidaDelay > 0) // investida em recarga, boss apenas se move
                {
                    felicidade.MoverParaPosicao(player.transform.position, velocidade);
                }
                else
                {
                    if (ConsegueInvestida(posicaoPlayer, player))
                    {
                        Debug.Log("Preparando para investida");
                        investidaDirecao = (posicaoPlayer - RetornarPosicao()).normalized;
                        estado = BossEstado.PreparandoInvestida;
                        StartCoroutine(AvisoInvestida());
                    }
                    else
                    {
                        // Caminhando em direção ao jogador
                        felicidade.MoverParaPosicao(player.transform.position, velocidade);
                    }
                }
                break;

            case BossEstado.PreparandoInvestida:
                prepararInvestidaDelay -= Time.deltaTime;
                if (prepararInvestidaDelay <= 0)
                {
                    Debug.Log("Iniciando investida");
                    investidaVelocidade = 10f; // Velocidade inicial da investida
                    investidaTimer = investidaTempo; // Reinicia o temporizador da investida
                    estado = BossEstado.Investida;
                }
                break;

            case BossEstado.Investida:
                if (investidaTimer > 0)
                {
                    transform.position += investidaDirecao * investidaVelocidade * Time.deltaTime; // Atualizando posição durante a investida

                    float investidaVelocidadeReducaoMultiplicador = 1f;
                    investidaVelocidade -= investidaVelocidade * investidaVelocidadeReducaoMultiplicador * Time.deltaTime;

                    investidaTimer -= Time.deltaTime;

                    float acertoDistancia = 3f;
                    RaycastHit2D raycastHit2D = Physics2D.Raycast(RetornarPosicao(), investidaDirecao, acertoDistancia);
                    if (raycastHit2D.collider != null)
                    {
                        Player player = raycastHit2D.collider.GetComponent<Player>();
                        if (player != null)
                        {
                            investidaVelocidade = 60f;
                            investidaDirecao *= -1f;
                        }
                    }
                }
                else
                {
                    estado = BossEstado.Padrao;
                    investidaDelay = 1.5f;
                    DefinirEstadoNormal();
                    Debug.Log("Fim da investida, retornando ao estado padrão.");
                }
                break;
        }
    }

    private bool ConsegueInvestida(Vector3 posicaoAlvo, GameObject alvoGameObject)
    {
        float distanciaAlvo = Vector3.Distance(RetornarPosicao(), posicaoAlvo);

        float investidaDistanciaMax = 5f;
        if (distanciaAlvo > investidaDistanciaMax)
        {
            Debug.Log("boolfalse");
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
        // Muda a cor do boss para a cor de aviso
        bossSR.color = avisoCor;

        // Aguarda o tempo de aviso
        yield return new WaitForSeconds(avisoDuracao);

        // Restaura a cor original do boss
        bossSR.color = corOriginal;

        // Muda o estado para PreparandoInvestida após o aviso
        prepararInvestidaDelay = 1f; // Reinicia o delay de preparação
    }
}
