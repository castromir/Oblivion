using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRaiva : Boss
{
    public GameObject player;
    private BossVida bossVida;

    private float velocidade = 2.5f;
    private float velocidadeInvestida = 8f;
    private float tempoDeRecargaPunho = 2f;
    private float tempoDeRecargaLaminasDeFogo = 3f;
    private float tempoDeRecargaInvestida = 12f;
    private float delayInvestida = 1f;
    private float proximoPunho;
    private float proximasLaminasDeFogo;
    private float proximaInvestida;
    private Transform playerTransform;

    public GameObject laminaDeFogoPrefab;
    public GameObject avisoPrefab; // Prefab do aviso de exclamação

    private BossRenderer bossRenderer;
    private bool isInvesting = false;

    public Vector2 pontoInvestida1;
    public Vector2 pontoInvestida2;

    public float distanciaEntreAvisos = 5f; // Distância entre os avisos

    private List<GameObject> avisos = new List<GameObject>(); // Lista para armazenar os avisos

    void Start()
    {
        bossVida = GetComponent<BossVida>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        proximoPunho = Time.time;
        proximasLaminasDeFogo = Time.time;
        proximaInvestida = Time.time;
        bossRenderer = GetComponent<BossRenderer>();
    }

    void Update()
    {
        if (bossVida.isDead)
        {
            DesativarBoss();
        }

        if (!isInvesting)
        {
            // Adiciona movimentação básica para visualização de animação
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, velocidade * Time.deltaTime);

            if (direction != Vector2.zero)
            {
                bossRenderer.SetDirection(direction, false);
            }
        }
    }

    public bool IsPlayerClose()
    {
        float distancia = Vector2.Distance(transform.position, player.transform.position);
        return distancia < 6f;
    }

    public bool IsPunhoOnCooldown()
    {
        return Time.time < proximoPunho;
    }

    public void AtaquePunho()
    {
        Debug.Log("Ataque de Punho");
        proximoPunho = Time.time + tempoDeRecargaPunho;
    }

    public bool IsLaminasDeFogoOnCooldown()
    {
        return Time.time < proximasLaminasDeFogo;
    }

    public void AtaqueLaminasDeFogo()
    {
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
        Debug.Log("Preparando para Investida");

        isInvesting = true;

        Vector2 pontoInicial;
        Vector2 pontoFinal;

        // Determina qual ponto está mais próximo do boss
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

        CriarAvisos(pontoInicial, pontoFinal); // Criar avisos antes da investida

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
        // Move rapidamente para o ponto inicial
        while (Vector2.Distance(transform.position, pontoInicial) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, pontoInicial, velocidadeInvestida * Time.deltaTime);
            yield return null;
        }

        // Pequeno delay antes de iniciar a investida
        yield return new WaitForSeconds(delayInvestida);

        // Inicia a investida para o ponto final
        Debug.Log("Iniciando Investida");

        while (Vector2.Distance(transform.position, pontoFinal) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, pontoFinal, velocidadeInvestida * Time.deltaTime);
            yield return null;
        }

        // Conclui a investida
        isInvesting = false;
        proximaInvestida = Time.time + tempoDeRecargaInvestida;
        Debug.Log("Investida Concluída");

        DestruirAvisos(); // Destruir avisos após a investida
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
        velocidade *= 1.5f; // Aumentar a velocidade em 50%
    }

    public void ReduceCooldowns()
    {
        tempoDeRecargaPunho *= 0.5f; // Reduzir tempo de recarga pela metade
        tempoDeRecargaLaminasDeFogo *= 0.5f;
        tempoDeRecargaInvestida *= 0.5f;
    }

    public void DesativarBoss()
    {
        // Desative todos os componentes específicos do Boss da Raiva
        StopAllCoroutines(); // Para todas as corrotinas em andamento
        isInvesting = false;

        // Desative componentes ou scripts adicionais, se necessário
        bossRenderer.enabled = false;

        // Se houver outros componentes específicos do Boss da Raiva, desative-os aqui
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
