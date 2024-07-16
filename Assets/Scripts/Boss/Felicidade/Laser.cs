using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public PlayerVida playerVida;

    public float duracao = 1f;  // Duração do laser em segundos
    public float velocidade = 10f;  // Velocidade do laser
    private Vector3 direcao;

    private void Awake()
    {
        playerVida = FindObjectOfType<PlayerVida>();
    }

    private void OnEnable()
    {
        StartCoroutine(DesativarAposDuracao());
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
            // Acessa o componente PlayerVida e chama o método ReceberDano
            
                playerVida.ReceberDano();
                Debug.Log("Player atingido pelo laser!");
        }
    }
}
