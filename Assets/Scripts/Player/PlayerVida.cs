using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerVida : MonoBehaviour
{
    [SerializeField] public int vidaMaxima = 3;
    [SerializeField] private float repulsaoQuantidade = 10f;
    [SerializeField] private float danoTempoRecuperacao = 1f;

    public int vidaAtual;
    private bool consegueReceberDano = true;
    private bool invulneravelFeitico = false;  // Invulnerabilidade causada pelo feitiço Euforia
    private Repulsao repulsao;
    private Color corOriginal;

    public SpriteRenderer playerSR;
    public SpriteRenderer playerLivroSR;
    public IsometricPlayerMovementController playerMovimento;
    public PlayerMiraFeitico playerLivroAcoes;

    private void Awake()
    {
        repulsao = GetComponent<Repulsao>();
    }

    private void Start()
    {
        vidaAtual = vidaMaxima;
        corOriginal = playerSR.color;
    }

    public void ReceberDano()
    {
        if (consegueReceberDano && !invulneravelFeitico)
        {
            vidaAtual -= 1;
            if (vidaAtual <= 0)
            {
                playerSR.enabled = false;
                playerLivroSR.enabled = false;
                playerMovimento.enabled = false;
                playerLivroAcoes.enabled = false;
                StartCoroutine(HandleDeath());
            }
            else
            {
                StartCoroutine(VisualDano());
                StartCoroutine(TempoDeInvulnerabilidade());
            }
        }
    }

    private IEnumerator TempoDeInvulnerabilidade()
    {
        consegueReceberDano = false;
        yield return new WaitForSeconds(danoTempoRecuperacao);
        consegueReceberDano = true;
    }

    private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Lobby");
    }

    private IEnumerator VisualDano()
    {
        playerSR.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        playerSR.color = corOriginal;
    }

    public void AtivarEuforia()
    {
        StartCoroutine(EuforiaCoroutine());
    }

    private IEnumerator EuforiaCoroutine()
    {
        invulneravelFeitico = true;
        float duracaoEuforia = 2f;
        float tempoRestante = duracaoEuforia;

        while (tempoRestante > 0)
        {
            playerSR.color = Color.yellow;
            yield return new WaitForSeconds(0.1f);
            playerSR.color = corOriginal;
            yield return new WaitForSeconds(0.1f);
            tempoRestante -= 0.2f;
        }

        invulneravelFeitico = false;
        playerSR.color = corOriginal;
    }
}
