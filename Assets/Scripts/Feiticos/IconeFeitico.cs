using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IconeFeitico : MonoBehaviour
{
    public Image iconeImagem;
    public Text tempoRecargaTexto;
    public GuardaFeitico guardaFeitico;
    public GameObject playerMago;  // Referência ao objeto PlayerMago

    private void Start()
    {
        // Obtém o componente GuardaFeitico do objeto PlayerMago
        if (playerMago != null)
        {
            guardaFeitico = playerMago.GetComponent<GuardaFeitico>();
        }

        if (guardaFeitico == null)
        {
            Debug.LogError("GuardaFeitico não foi encontrado no objeto PlayerMago.");
            return;
        }

        if (guardaFeitico.feiticos.Length > 0 && guardaFeitico.feiticos[0] != null)
        {
            guardaFeitico.feiticos[0].AoDesativarMagia += IniciarRecarga;
        }
        else
        {
            Debug.LogError("Feitico não foi atribuído no GuardaFeitico.");
        }
    }

    private void Update()
    {
        if (guardaFeitico == null || guardaFeitico.feiticos.Length == 0 || guardaFeitico.feiticos[0] == null)
        {
            Debug.LogError("GuardaFeitico ou Feitico é nulo no Update.");
            return;
        }

        if (guardaFeitico.estado == GuardaFeitico.FeiticoEstado.recarga)
        {
            AtualizarIconeRecarga(guardaFeitico.tempoRecarga);
        }
    }

    private void IniciarRecarga(GameObject parente)
    {
        StartCoroutine(RecargaCoroutine(guardaFeitico.feiticos[0].tempoRecarga));
    }

    private IEnumerator RecargaCoroutine(float duracao)
    {
        if (iconeImagem != null)
        {
            iconeImagem.color = Color.gray;
        }

        float tempoRestante = duracao;
        while (tempoRestante > 0)
        {
            tempoRestante -= Time.deltaTime;
            AtualizarIconeRecarga(tempoRestante);
            yield return null;
        }

        if (iconeImagem != null)
        {
            iconeImagem.color = Color.white;
        }

        if (tempoRecargaTexto != null)
        {
            tempoRecargaTexto.text = "";  // Limpa o texto de recarga
        }
    }

    private void AtualizarIconeRecarga(float tempoRestante)
    {
        if (tempoRecargaTexto != null)
        {
            if (tempoRestante > 0)
            {
                tempoRecargaTexto.text = Mathf.Ceil(tempoRestante).ToString();
            }
            else
            {
                tempoRecargaTexto.text = "";  // Limpa o texto de recarga se o tempo restante for zero ou menor
            }
        }
    }
}
