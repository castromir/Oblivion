﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaivaEstado : MonoBehaviour
{
    public BossRaiva bossRaiva;
    public BossVida bossVida;

    private BossFase currentFase = BossFase.Fase1;
    private BossEstado currentEstado = BossEstado.Padrao;
    private bool isMovingToPlayer = false;

    private enum BossFase
    {
        Fase1,
        Fase2
    }

    private enum BossEstado
    {
        Padrao,
        AtaqueVortex,
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
        if (bossVida.isDead)
        {
            StopAllCoroutines();
            return;
        }

        if (currentFase == BossFase.Fase1 && bossVida.vidaAtual <= bossVida.GetVidaMaxima() / 2)
        {
            // Transição para a Fase 2
            currentFase = BossFase.Fase2;
            bossRaiva.IncreaseSpeed();
            bossRaiva.ReduceCooldowns();
        }
    }

    private IEnumerator BossBehavior()
    {
        while (true)
        {
            if (currentFase == BossFase.Fase1)
            {
                if (!isMovingToPlayer && bossRaiva.IsPlayerClose() && !bossRaiva.IsVortexOnCooldown())
                {
                    //Debug.Log("Fase 1: Ataque de Vórtice");
                    StartCoroutine(MoveToPlayerAndAttack());
                }
                else if (!bossRaiva.IsPlayerClose() && !bossRaiva.IsLaminasDeFogoOnCooldown())
                {
                    //Debug.Log("Fase 1: Ataque de Lâminas de Fogo");
                    bossRaiva.AtaqueLaminasDeFogo();
                }
                else if (!bossRaiva.IsInvestidaOnCooldown())
                {
                    //Debug.Log("Fase 1: Ataque de Investida");
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
                        if (!isMovingToPlayer && !bossRaiva.IsVortexOnCooldown())
                        {
                            Debug.Log("Fase 2: Ataque de Vórtice");
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

            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator MoveToPlayerAndAttack()
    {
        isMovingToPlayer = true;
        Transform playerTransform = bossRaiva.player.transform;

        while (Vector2.Distance(transform.position, playerTransform.position) > 1.5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, bossRaiva.GetVelocidade() * Time.deltaTime);
            yield return null;
        }

        bossRaiva.AtaqueVortex();
        isMovingToPlayer = false;
    }
}
