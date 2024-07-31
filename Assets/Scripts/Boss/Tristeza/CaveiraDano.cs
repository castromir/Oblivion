using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveiraDano : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerVida playerVida = collision.gameObject.GetComponent<PlayerVida>();
            if (playerVida != null)
            {
                playerVida.ReceberDano();
            }

            // Destrua a caveira após a colisão
            Destroy(gameObject);
        }
    }
}
