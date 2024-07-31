using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueCaveiras : MonoBehaviour
{
    public GameObject caveiraPrefab; // Prefab da caveira
    public Transform spawnPoint; // Ponto de spawn da caveira
    private float caveiraSpeed = 3.5f; // Velocidade das caveiras
    public float duracaoCaveira = 5f; // Duração antes de destruir a caveira
    private int caveirasPorOnda = 1; // Quantidade de caveiras por onda

    private Transform playerTransform; // Transform do player

    private void Start()
    {
        // Buscar o player na cena
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }

        // Começar o ataque das caveiras
        StartCoroutine(LancarCaveiras());
    }

    private IEnumerator LancarCaveiras()
    {
        while (true)
        {
            if (playerTransform != null)
            {
                for (int i = 0; i < caveirasPorOnda; i++)
                {
                    // Instanciar a caveira
                    GameObject caveira = Instantiate(caveiraPrefab, spawnPoint.position, Quaternion.identity);

                    // Adicionar comportamento de flutuar à caveira
                    Rigidbody2D rb = caveira.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.isKinematic = true;

                        // Calcular a direção do spawn point até o player
                        Vector2 direction = (playerTransform.position - spawnPoint.position).normalized;
                        rb.velocity = direction * caveiraSpeed;
                    }

                    // Destruir a caveira após a duração especificada
                    Destroy(caveira, duracaoCaveira);

                    // Pequeno intervalo entre cada caveira lançada
                    yield return new WaitForSeconds(0.9f); 
                }
            }

            // Esperar um tempo antes de lançar a próxima onda de caveiras
            yield return new WaitForSeconds(4f); 
        }
    }

    public void SetCaveiraSpeed(float speed)
    {
        caveiraSpeed = speed;
    }

    public void SetCaveirasPorOnda(int quantidade)
    {
        caveirasPorOnda = quantidade;
    }
}
