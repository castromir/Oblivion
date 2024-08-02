using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRaiva : Boss
{
    public GameObject player;
    private BossVida bossVida;

    private float velocidade = 1.5f;
    private float velocidadeInvestida = 14f;
    private float tempoDeRecargaVortex = 10f;
    private float tempoDeRecargaLaminasDeFogo = 3f;
    private float tempoDeRecargaInvestida = 12f;
    private float delayInvestida = 1f;
    private float proximoVortex;
    private float proximasLaminasDeFogo;
    private float proximaInvestida;
    private Transform playerTransform;

    public GameObject vortexDeFogoPrefab;
    public GameObject laminaDeFogoPrefab;
    public GameObject avisoPrefab;

    private BossRenderer bossRenderer;
    private bool isInvesting = false;
    private bool isAttackingVortex = false;

    public Vector2 pontoInvestida1;
    public Vector2 pontoInvestida2;

    public float distanciaEntreAvisos = 5f;

    private List<GameObject> avisos = new List<GameObject>();

    void Start()
    {
        bossVida = GetComponent<BossVida>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        bossRenderer = GetComponent<BossRenderer>();
        Reiniciar();
    }

    void OnEnable()
    {
        Reiniciar();
    }

    void Reiniciar()
    {
        proximoVortex = Time.time;
        proximasLaminasDeFogo = Time.time;
        proximaInvestida = Time.time;
        isInvesting = false;
        isAttackingVortex = false;
        avisos.Clear();
    }

    void Update()
    {
        if (bossVida.isDead)
        {
            DesativarBoss();
            return;
        }

        if (!isInvesting && !isAttackingVortex)
        {
            if (!IsVortexOnCooldown())
            {
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, velocidade * Time.deltaTime);

                if (direction != Vector2.zero)
                {
                    bossRenderer.SetDirection(direction, false);
                }
            }
        }
    }

    public bool IsPlayerClose()
    {
        float distancia = Vector2.Distance(transform.position, player.transform.position);
        return distancia < 6f;
    }

    public bool IsVortexOnCooldown()
    {
        return Time.time < proximoVortex;
    }

    public void AtaqueVortex()
    {
        if (isInvesting || isAttackingVortex) return;
        if (vortexDeFogoPrefab != null)
        {
            isAttackingVortex = true;
            GameObject vortex = Instantiate(vortexDeFogoPrefab, transform.position, Quaternion.identity);
            StartCoroutine(EncerrarVortex());
        }
        else
        {
            Debug.LogError("vortexDeFogoPrefab não está atribuído.");
        }

        proximoVortex = Time.time + tempoDeRecargaVortex;
    }

    private IEnumerator EncerrarVortex()
    {
        yield return new WaitForSeconds(tempoDeRecargaVortex);
        isAttackingVortex = false;
    }

    public bool IsLaminasDeFogoOnCooldown()
    {
        return Time.time < proximasLaminasDeFogo;
    }

    public void AtaqueLaminasDeFogo()
    {
        if (isInvesting || isAttackingVortex) return;
        if (laminaDeFogoPrefab != null)
        {
            StartCoroutine(LancarLaminasDeFogo());
        }
        else
        {
            Debug.LogError("laminaDeFogoPrefab não está atribuído.");
        }

        proximasLaminasDeFogo = Time.time + tempoDeRecargaLaminasDeFogo;
    }

    private IEnumerator LancarLaminasDeFogo()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject laminaDeFogo = Instantiate(laminaDeFogoPrefab, transform.position, Quaternion.identity);

            LaminaDeFogo laminaScript = laminaDeFogo.GetComponent<LaminaDeFogo>();
            if (laminaScript != null)
            {
                laminaScript.IniciarAtaque(playerTransform.position);
            }
            else
            {
                Debug.LogError("LaminaDeFogo script não encontrado no prefab.");
            }

            yield return new WaitForSeconds(0.9f);
        }
    }

    public bool IsInvestidaOnCooldown()
    {
        return Time.time < proximaInvestida;
    }

    public void AtaqueInvestida()
    {
        if (isInvesting || isAttackingVortex) return;

        isInvesting = true;

        Vector2 pontoInicial;
        Vector2 pontoFinal;

        if (Vector2.Distance(transform.position, pontoInvestida1) < Vector2.Distance(transform.position, pontoInvestida2))
        {
            pontoInicial = pontoInvestida1;
            pontoFinal = pontoInvestida2;
        }
        else
        {
            pontoInicial = pontoInvestida2;
            pontoFinal = pontoInvestida1;
        }

        CriarAvisos(pontoInicial, pontoFinal);

        StartCoroutine(MovimentarParaPonto(pontoInicial, pontoFinal));
    }

    private void CriarAvisos(Vector2 pontoInicial, Vector2 pontoFinal)
    {
        Vector2 direcao = (pontoFinal - pontoInicial).normalized;
        float distancia = Vector2.Distance(pontoInicial, pontoFinal);
        int numeroDeAvisos = Mathf.FloorToInt(distancia / 1.5f);

        for (int i = 0; i <= numeroDeAvisos; i++)
        {
            Vector2 posicaoAviso = pontoInicial + direcao * (i * 2);
            GameObject aviso = Instantiate(avisoPrefab, posicaoAviso, Quaternion.identity);
            avisos.Add(aviso);
        }
    }

    private IEnumerator MovimentarParaPonto(Vector2 pontoInicial, Vector2 pontoFinal)
    {
        while (Vector2.Distance(transform.position, pontoInicial) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, pontoInicial, velocidadeInvestida * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(delayInvestida);

        while (Vector2.Distance(transform.position, pontoFinal) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, pontoFinal, velocidadeInvestida * Time.deltaTime);
            yield return null;
        }

        isInvesting = false;
        proximaInvestida = Time.time + tempoDeRecargaInvestida;

        DestruirAvisos();
    }

    private void DestruirAvisos()
    {
        foreach (GameObject aviso in avisos)
        {
            Destroy(aviso);
        }
        avisos.Clear();
    }

    public void IncreaseSpeed()
    {
        velocidade *= 1.5f;
    }

    public void ReduceCooldowns()
    {
        tempoDeRecargaVortex *= 0.5f;
        tempoDeRecargaLaminasDeFogo *= 0.5f;
        tempoDeRecargaInvestida *= 0.5f;
    }

    public void DesativarBoss()
    {
        StopAllCoroutines();
        isInvesting = false;
        isAttackingVortex = false;
        bossRenderer.enabled = false;
    }

    public float GetVelocidade()
    {
        return velocidade;
    }

    public override string[] GetDirecoesEstaticas()
    {
        return new string[] { "Raiva N", "Raiva O", "Raiva S", "Raiva L" };
    }

    public override string[] GetDirecoesHabilidade()
    {
        return new string[] { "Raiva N", "Raiva O", "Raiva S", "Raiva L" };
    }
}
