using CodeMonkey.Utils;
using System.Collections;
using UnityEngine;

public class FelicidadeLaser : MonoBehaviour
{
    [SerializeField] public static float laserTempoRecarga = 1f;
    public float laserRecarga = laserTempoRecarga;
    public float laserDuracao = 1f; // Duração que o laser fica ativo
    public GameObject laserPrefab;  // Prefab do laser
    public Transform laserSpawnPoint;  // Ponto de origem do laser

    public GameObject player;
    private Transform playerTransform;
    private GameObject currentLaser;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        laserRecarga -= Time.deltaTime;
        if (ConsegueLaser())
        {
            DispararLaser();
            laserRecarga = laserTempoRecarga;  // Reiniciar o tempo de recarga
        }
    }

    public bool ConsegueLaser()
    {
        return laserRecarga <= 0;
    }

    public void DispararLaser()
    {
        Vector3 posicaoInicial = laserSpawnPoint.position;
        Vector3 posicao;

        Vector3 direcao = (player.transform.position - laserSpawnPoint.position).normalized;
        float eulerZ = UtilsClass.GetAngleFromVectorFloat(direcao) - 90;
        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
        //float distancia = Vector3.Distance(posicaoInicial, posicaoFinal);

        currentLaser = Instantiate(laserPrefab, laserSpawnPoint.position, Quaternion.Euler(0, 0, angulo));
        currentLaser.SetActive(true);
        StartCoroutine(DesativarLaserAposTempo(currentLaser, laserDuracao));
    }

    private IEnumerator DesativarLaserAposTempo(GameObject laser, float duracao)
    {
        yield return new WaitForSeconds(duracao);
        Destroy(laser); // Destroi o laser após a duração
    }

    public void SetPlayerTransform(Transform player)
    {
        playerTransform = player;
    }
}
