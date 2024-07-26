using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public PlayerVida playerVida;

    public float duracao = .2f;  // Duração do laser em segundos
    public float velocidade = 10f;  // Velocidade do laser
    private Vector3 direcao;

    private Coroutine desativarCoroutine;

    private void Awake()
    {
        playerVida = FindObjectOfType<PlayerVida>();
    }

    private void OnEnable()
    {
        desativarCoroutine = StartCoroutine(DesativarAposDuracao());
    }

    private void OnDisable()
    {
        if (desativarCoroutine != null)
        {
            StopCoroutine(desativarCoroutine);
        }
    }

    private void Update()
    {
        transform.Translate(direcao * velocidade * Time.deltaTime);
    }

    public void ConfigurarDirecao(Vector3 direcao)
    {
        this.direcao = direcao.normalized;
    }

    private IEnumerator DesativarAposDuracao()
    {
        yield return new WaitForSeconds(duracao);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerVida == null)
            {
                playerVida = collision.GetComponent<PlayerVida>();
            }

            if (playerVida != null)
            {
                playerVida.ReceberDano();
                Debug.Log("Player atingido pelo laser!");
            }
        }
    }
}
