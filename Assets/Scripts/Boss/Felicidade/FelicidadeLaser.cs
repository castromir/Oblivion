﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FelicidadeLaser : MonoBehaviour
{
    public GameObject avisoPrefab;
    private List<GameObject> avisosAtuais = new List<GameObject>();

    public float avisoDelay = 1f;

    [SerializeField] public static float laserTempoRecarga = 7f;
    public float laserRecarga = laserTempoRecarga;
    public float laserDuracao = 0.5f;
    public float comprimento = 0;
    public GameObject laserPrefab;
    public Transform laserSpawnPoint;

    public GameObject player;
    private Transform playerTransform;
    private GameObject currentLaser;

    private int numeroDeAvisos = 7;
    private List<Coroutine> coroutinesAtuais = new List<Coroutine>();

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnEnable()
    {
        // Resetar o laser quando o objeto for habilitado
        ResetarLaser();
    }

    private void Update()
    {
        if (laserRecarga > 0)
        {
            laserRecarga -= Time.deltaTime;
        }
    }

    public bool ConsegueLaser()
    {
        return laserRecarga <= 0;
    }

    public void DispararLaser()
    {
        Vector3 direcao = (player.transform.position - laserSpawnPoint.position).normalized;
        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;

        float distanciaEntreAvisos = 15f / numeroDeAvisos;
        Vector3 posicaoAvisoInicial = laserSpawnPoint.position;

        for (int i = 1; i <= numeroDeAvisos; i++)
        {
            Vector3 posicaoAviso = posicaoAvisoInicial + direcao * distanciaEntreAvisos * i;
            GameObject avisoAtual = Instantiate(avisoPrefab, posicaoAviso, Quaternion.identity);
            avisoAtual.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            avisoAtual.transform.SetParent(null);
            avisoAtual.SetActive(true);
            avisosAtuais.Add(avisoAtual);
        }

        Coroutine coroutine = StartCoroutine(AguardarAvisoEContinuar(avisoDelay, angulo, direcao));
        coroutinesAtuais.Add(coroutine);
    }

    private IEnumerator AguardarAvisoEContinuar(float delay, float angulo, Vector3 direcao)
    {
        yield return new WaitForSeconds(delay);

        currentLaser = Instantiate(laserPrefab, laserSpawnPoint.position, Quaternion.Euler(0, 0, angulo));
        currentLaser.transform.position += direcao * (currentLaser.transform.localScale.x / 2);
        currentLaser.transform.localScale = new Vector3(50f, currentLaser.transform.localScale.y, currentLaser.transform.localScale.z);

        Animator laserAnimator = currentLaser.GetComponent<Animator>();
        if (laserAnimator != null)
        {
            laserAnimator.SetBool("IsFiring", true);
        }

        currentLaser.SetActive(true);

        foreach (var aviso in avisosAtuais)
        {
            Coroutine coroutine = StartCoroutine(RemoverAvisoAposTempo(aviso, laserDuracao));
            coroutinesAtuais.Add(coroutine);
        }
        avisosAtuais.Clear();

        Coroutine desativarLaserCoroutine = StartCoroutine(DesativarLaserAposTempo(currentLaser, laserDuracao));
        coroutinesAtuais.Add(desativarLaserCoroutine);
        laserRecarga = laserTempoRecarga;
    }

    private IEnumerator DesativarLaserAposTempo(GameObject laser, float duracao)
    {
        yield return new WaitForSeconds(duracao);
        laser.SetActive(false);
        Destroy(laser);
    }

    private IEnumerator RemoverAvisoAposTempo(GameObject aviso, float duracao)
    {
        yield return new WaitForSeconds(duracao);
        aviso.SetActive(false);
        Destroy(aviso);
    }

    public void SetPlayerTransform(Transform player)
    {
        playerTransform = player;
    }

    // Método para resetar o laser
    public void ResetarLaser()
    {
        laserRecarga = laserTempoRecarga;
    }

    private void OnDisable()
    {
        // Parar todas as coroutines quando o objeto for desabilitado
        foreach (var coroutine in coroutinesAtuais)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        }
        coroutinesAtuais.Clear();

        // Destruir todos os avisos atuais
        foreach (var aviso in avisosAtuais)
        {
            if (aviso != null)
            {
                Destroy(aviso);
            }
        }
        avisosAtuais.Clear();

        // Destruir o laser atual se ele ainda estiver ativo
        if (currentLaser != null)
        {
            Destroy(currentLaser);
        }
    }
}
