using UnityEngine;
using System.Collections;

public class AuraDano : MonoBehaviour
{
    PlayerVida playerVida;

    public float intervaloDano = 1f; // Intervalo entre aplicações de dano em segundos
    private Coroutine danoCoroutine;

    private void Awake()
    {
        playerVida = FindObjectOfType<PlayerVida>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player entrou");
            // Inicia o Coroutine para aplicar dano
            danoCoroutine = StartCoroutine(AplicarDano(playerVida));
        }
        else Debug.Log("player n entrou");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Para o Coroutine quando o jogador sai da área
            if (danoCoroutine != null)
            {
                StopCoroutine(danoCoroutine);
                danoCoroutine = null;
            }
        }
    }

    private IEnumerator AplicarDano(PlayerVida player)
    {
        while (true)
        {
            player.ReceberDano();
            yield return new WaitForSeconds(intervaloDano);
        }
    }
}
