using CodeMonkey.Utils;
using System.Collections;
using UnityEngine;

public class FelicidadeLaser : MonoBehaviour
{
    [SerializeField] public static float laserTempoRecarga = 7f;
    public float laserRecarga = laserTempoRecarga;
    public float laserDuracao = 0.5f; // Duração que o laser fica ativo
    public float comprimento = 10f;
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
        // Calcula a direção do laser
        Vector3 direcao = (player.transform.position - laserSpawnPoint.position).normalized;

        // Calcula o ângulo em radianos e depois converte para graus
        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;

        // Instancia o laser com a rotação correta
        currentLaser = Instantiate(laserPrefab, laserSpawnPoint.position, Quaternion.Euler(0, 0, angulo));

        // Ajusta a posição do laser para estar na frente do ponto de origem (assumindo que o ponto de origem está na extremidade do boss)
        currentLaser.transform.position += direcao * (currentLaser.transform.localScale.x / 2);

        // Ajusta o comprimento do laser
        currentLaser.transform.localScale = new Vector3(comprimento, currentLaser.transform.localScale.y, currentLaser.transform.localScale.z);

        // Ativa o laser
        currentLaser.SetActive(true);

        // Inicia a corrotina para desativar o laser após a duração especificada
        StartCoroutine(DesativarLaserAposTempo(currentLaser, laserDuracao));

        // Reinicia o tempo de recarga do laser
        laserRecarga = laserTempoRecarga;
    }

    private IEnumerator DesativarLaserAposTempo(GameObject laser, float duracao)
    {
        // Aguarda a duração do laser
        yield return new WaitForSeconds(duracao);

        // Desativa o laser
        laser.SetActive(false);

        // Destroi o laser após desativar (opcional)
        Destroy(laser);
    }


    public void SetPlayerTransform(Transform player)
    {
        playerTransform = player;
    }
}
