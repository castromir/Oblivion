using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaiuDaArena : MonoBehaviour
{
    public GameObject player;
    public Vector2 safeSpawnPoint = new Vector2(-5, 0); // Define um ponto seguro para mover o player
    public Vector2 alternateSafeSpawnPoint = new Vector2(7, 0); // Define um ponto seguro alternativo
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
        // Retorna o ponto alternativo seguro
        return alternateSafeSpawnPoint;
    }
}
