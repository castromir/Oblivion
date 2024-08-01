using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaivaEstado : MonoBehaviour
{
    public BossRaiva bossRaiva;
    public BossVida bossVida;

    private BossFase currentFase = BossFase.Fase1;
    private bool isMovingToPlayer = false;

    private enum BossFase
    {
        Fase1,
        Fase2
    }

    private enum BossEstado
    {
        Padrao,
        AtaquePunho,
        AtaqueLaminasDeFogo,
        AtaqueInvestida
    }

    void Start()
    {
        bossRaiva = GetComponent<BossRaiva>();
        bossVida = GetComponent<BossVida>();
        StartCoroutine(BossBehavior());
    }

    void Update()
    {
        if (currentFase == BossFase.Fase1 && bossVida.vidaAtual <= bossVida.GetVidaMaxima() / 2)
        {
            // Transição para a Fase 2
            currentFase = BossFase.Fase2;
            bossRaiva.IncreaseSpeed(); // Supondo que esse método aumente a velocidade do boss
            bossRaiva.ReduceCooldowns(); // Supondo que esse método reduza os tempos de recarga
        }
    }

    private IEnumerator BossBehavior()
    {
        while (true)
        {
            if (currentFase == BossFase.Fase1)
            {
                if (!isMovingToPlayer && bossRaiva.IsPlayerClose() && !bossRaiva.IsPunhoOnCooldown())
                {
                    Debug.Log("Fase 1: Ataque de Punho");
                    StartCoroutine(MoveToPlayerAndAttack());
                }
                else if (!bossRaiva.IsPlayerClose() && !bossRaiva.IsLaminasDeFogoOnCooldown())
                {
                    Debug.Log("Fase 1: Ataque de Lâminas de Fogo");
                    bossRaiva.AtaqueLaminasDeFogo();
                }
                else if (!bossRaiva.IsInvestidaOnCooldown())
                {
                    Debug.Log("Fase 1: Ataque de Investida");
                    bossRaiva.AtaqueInvestida();
                }
            }
            else if (currentFase == BossFase.Fase2)
            {
                Debug.Log("Fase 2: Selecionando ataque aleatório");
                int randomAttack = Random.Range(0, 3);
                switch (randomAttack)
                {
                    case 0:
                        if (!isMovingToPlayer && !bossRaiva.IsPunhoOnCooldown())
                        {
                            Debug.Log("Fase 2: Ataque de Punho");
                            StartCoroutine(MoveToPlayerAndAttack());
                        }
                        break;
                    case 1:
                        if (!bossRaiva.IsLaminasDeFogoOnCooldown())
                        {
                            Debug.Log("Fase 2: Ataque de Lâminas de Fogo");
                            bossRaiva.AtaqueLaminasDeFogo();
                        }
                        break;
                    case 2:
                        if (!bossRaiva.IsInvestidaOnCooldown())
                        {
                            Debug.Log("Fase 2: Ataque de Investida");
                            bossRaiva.AtaqueInvestida();
                        }
                        break;
                }
            }

            yield return new WaitForSeconds(1f); 
        }
    }

    private IEnumerator MoveToPlayerAndAttack()
    {
        isMovingToPlayer = true;
        Vector3 playerPosition = bossRaiva.player.transform.position;
        while (Vector2.Distance(transform.position, playerPosition) > 1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, bossRaiva.velocidade * Time.deltaTime);
            yield return null;
        }
        bossRaiva.AtaquePunho();
        isMovingToPlayer = false;
    }
}
