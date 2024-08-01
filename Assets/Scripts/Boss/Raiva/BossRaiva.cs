using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRaiva : Boss
{
    public GameObject player;

    public float velocidade = 5f;
    public float velocidadeInvestida = 20f; 
    public float tempoDeRecargaPunho = 2f;
    public float tempoDeRecargaLaminasDeFogo = 3f;
    public float tempoDeRecargaInvestida = 5f;
    public float delayInvestida = 1f; 
    private float proximoPunho;
    private float proximasLaminasDeFogo;
    private float proximaInvestida;
    private Transform playerTransform;

    public GameObject laminaDeFogoPrefab;

    private BossRenderer bossRenderer;
    private bool isInvesting = false;

    public Vector2 pontoInvestida1;
    public Vector2 pontoInvestida2;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        proximoPunho = Time.time;
        proximasLaminasDeFogo = Time.time;
        proximaInvestida = Time.time;
        bossRenderer = GetComponent<BossRenderer>();
    }

    void Update()
    {
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
        }
        else
        {
            Debug.LogError("laminaDeFogoPrefab não está atribuído.");
        }

        proximasLaminasDeFogo = Time.time + tempoDeRecargaLaminasDeFogo;
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

        StartCoroutine(MovimentarParaPonto(pontoInicial, pontoFinal));
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

    public override string[] GetDirecoesEstaticas()
    {
        return new string[] { "Raiva N", "Raiva O", "Raiva S", "Raiva L" };
    }

    public override string[] GetDirecoesHabilidade()
    {
        return new string[] { "Raiva N", "Raiva O", "Raiva S", "Raiva L" };
    }
}
