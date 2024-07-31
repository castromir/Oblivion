using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaiuDaArena : MonoBehaviour
{
    public GameObject player;
    public Vector2 safeSpawnPoint = new Vector2(-5, 0); // Define um ponto seguro para mover o player
    public LayerMask obstacleLayer; // Define a layer de obstáculos para a verificação

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!Physics2D.OverlapCircle(safeSpawnPoint, 1f, obstacleLayer))
            {
                player.transform.position = safeSpawnPoint;
            }
            else
            {
                // Caso o ponto seguro esteja ocupado, mover para outra posição segura
                player.transform.position = FindAlternateSafePosition();
            }
        }
    }

    private Vector2 FindAlternateSafePosition()
    {
        // Implementar lógica para encontrar uma posição alternativa segura
        // Pode ser uma lista de pontos seguros predefinidos ou uma busca por uma área livre
        return new Vector2(7, 0); // Exemplo de uma posição alternativa
    }
}
