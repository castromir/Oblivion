using System.Collections;
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

    private float velocidade = 2f;
    private float investidaVelocidade = 6f;
    private float investidaTempo = 2f; // Duração da investida
    private float investidaDelay = 1.5f; // Inicialização do delay
    private float investidaTimer = 0f; // Temporizador da investida
    private float prepararInvestidaDelay = 1f; // Delay antes da investida

    static public float laserDelayTempo = 1.5f; // Tempo de delay do laser
    private float laserDelay = laserDelayTempo;

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
                    investidaDelay = 1.5f;
                    DefinirEstadoNormal();
                }
                break;
        }
    }

    private IEnumerator EstadoLaser()
    {
        estado = BossEstado.Laser;

        // Aguarda o tempo do laserDelay antes de disparar o laser
        yield return new WaitForSeconds(laserDelay);

        // Dispara o laser
        laser.DispararLaser();
        Debug.Log("Laser disparado!");

        // Aguarda novamente o tempo do laserDelay após disparar o laser
        yield return new WaitForSeconds(laserDelay);

        // Retorna ao estado padrão
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
