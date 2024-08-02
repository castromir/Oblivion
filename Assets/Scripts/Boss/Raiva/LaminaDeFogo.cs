using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaminaDeFogo : MonoBehaviour
{
    public float velocidade = 5f;
    private Vector2 direcao;

    public void IniciarAtaque(Vector3 posicaoPlayer)
    {
        direcao = (posicaoPlayer - transform.position).normalized;
    }

    void Update()
    {
        transform.Translate(direcao * velocidade * Time.deltaTime);
        // Destruir a lâmina após 5 segundos para evitar acumulação na cena
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("LaminaDeFogo colidiu com: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerVida playerVida = collision.gameObject.GetComponent<PlayerVida>();
            if (playerVida != null)
            {
                playerVida.ReceberDano();
               //Debug.Log("Lâmina de Fogo colidiu com o player e causou dano.");
            }
            Destroy(gameObject);
        }
    }
}