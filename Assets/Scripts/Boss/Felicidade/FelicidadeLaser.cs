using CodeMonkey.Utils;
using System.Collections;
using UnityEngine;

public class FelicidadeLaser : MonoBehaviour
{
    public GameObject avisoPrefab;
    private GameObject avisoAtual;

    public float avisoDelay = 1f;

    [SerializeField] public static float laserTempoRecarga = 7f;
    public float laserRecarga = laserTempoRecarga;
    public float laserDuracao = 0.5f;
    public float comprimento = 10f;
    public GameObject laserPrefab;
    public Transform laserSpawnPoint;

    public GameObject player;
    private Transform playerTransform;
    private GameObject currentLaser;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
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

        Vector3 posicaoAviso = player.transform.position;
        avisoAtual = Instantiate(avisoPrefab, posicaoAviso, Quaternion.identity);
        avisoAtual.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        avisoAtual.transform.SetParent(null);
        avisoAtual.SetActive(true);

        StartCoroutine(AguardarAvisoEContinuar(avisoDelay, angulo, direcao));
    }

    private IEnumerator AguardarAvisoEContinuar(float delay, float angulo, Vector3 direcao)
    {
        yield return new WaitForSeconds(delay);

        currentLaser = Instantiate(laserPrefab, laserSpawnPoint.position, Quaternion.Euler(0, 0, angulo));
        currentLaser.transform.position += direcao * (currentLaser.transform.localScale.x / 2);
        currentLaser.transform.localScale = new Vector3(comprimento, currentLaser.transform.localScale.y, currentLaser.transform.localScale.z);
        currentLaser.SetActive(true);

        StartCoroutine(RemoverAvisoAposTempo(avisoAtual, laserDuracao));
        StartCoroutine(DesativarLaserAposTempo(currentLaser, laserDuracao));
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
}
