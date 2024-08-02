using System.Collections;
using UnityEngine;

public class VortexDeFogo : MonoBehaviour
{
    public float tempoDeVida = 5f; // Tempo de vida do vórtice antes de desaparecer
    public float escalaInicial = 1f; // Escala inicial do vórtice
    public float escalaFinal = 5f; // Escala final do vórtice
    public float duracaoCrescimento = 2f; // Duração do crescimento do vórtice
    public float intervaloDano = 1f; // Intervalo entre aplicações de dano em segundos

    private Coroutine danoCoroutine;

    private void Start()
    {
        StartCoroutine(CrescerEDestruir());
    }

    private IEnumerator CrescerEDestruir()
    {
        float tempoPassado = 0f;
        Vector3 escalaInicialVector = new Vector3(escalaInicial, escalaInicial, escalaInicial);
        Vector3 escalaFinalVector = new Vector3(escalaFinal, escalaFinal, escalaFinal);

        while (tempoPassado < duracaoCrescimento)
        {
            transform.localScale = Vector3.Lerp(escalaInicialVector, escalaFinalVector, tempoPassado / duracaoCrescimento);
            tempoPassado += Time.deltaTime;
            yield return null;
        }

        // Manter o vórtice na escala final por um tempo antes de destruir
        yield return new WaitForSeconds(tempoDeVida - duracaoCrescimento);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerVida playerVida = other.gameObject.GetComponentInParent<PlayerVida>();
            if (playerVida != null)
            {
                Debug.Log("player");
                // Inicia o Coroutine para aplicar dano
                danoCoroutine = StartCoroutine(AplicarDano(playerVida));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Para o Coroutine quando o jogador sai da área
            if (danoCoroutine != null)
            {
                StopCoroutine(danoCoroutine);
                danoCoroutine = null;
            }
        }
    }

    private IEnumerator AplicarDano(PlayerVida playerVida)
    {
        while (true)
        {
            playerVida.ReceberDano();
            yield return new WaitForSeconds(intervaloDano);
        }
    }
}
