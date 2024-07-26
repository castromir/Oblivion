using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IconeFeitico : MonoBehaviour
{
    public Image iconeImagem;
    public Text tempoRecargaTexto;
    public GuardaFeitico guardaFeitico;
    public int indexFeitico = 0;  // Índice do feitiço a ser monitorado

    private Coroutine recargaCoroutine;

    private void OnEnable()
    {
        if (guardaFeitico == null)
        {
            //Debug.LogError("GuardaFeitico não foi atribuído.");
            return;
        }

        if (guardaFeitico.feiticos.Length > 0 && guardaFeitico.feiticos[indexFeitico].feitico != null)
        {
            guardaFeitico.feiticos[indexFeitico].feitico.AoDesativarMagia -= IniciarRecarga;
            guardaFeitico.feiticos[indexFeitico].feitico.AoDesativarMagia += IniciarRecarga;
        }
        else
        {
            Debug.LogError("Feitico não foi atribuído no GuardaFeitico.");
        }

        AtualizarIconeRecarga(0);
    }

    private void OnDestroy()
    {
        if (guardaFeitico != null && guardaFeitico.feiticos.Length > 0 && guardaFeitico.feiticos[indexFeitico].feitico != null)
        {
            guardaFeitico.feiticos[indexFeitico].feitico.AoDesativarMagia -= IniciarRecarga;
        }

        if (recargaCoroutine != null)
        {
            StopCoroutine(recargaCoroutine);
        }
    }

    private void Update()
    {
        if (guardaFeitico == null || guardaFeitico.feiticos.Length == 0 || guardaFeitico.feiticos[indexFeitico].feitico == null)
        {
            return;
        }

        var feiticoInfo = guardaFeitico.feiticos[indexFeitico];

        if (feiticoInfo.estado == GuardaFeitico.FeiticoEstado.recarga)
        {
            AtualizarIconeRecarga(feiticoInfo.tempoRecarga);
        }
        else if (feiticoInfo.estado == GuardaFeitico.FeiticoEstado.pronto)
        {
            AtualizarIconeRecarga(0);
        }
    }

    private void IniciarRecarga(GameObject parente)
    {
        if (recargaCoroutine != null)
        {
            StopCoroutine(recargaCoroutine);
        }
        recargaCoroutine = StartCoroutine(RecargaCoroutine(guardaFeitico.feiticos[indexFeitico].feitico.tempoRecarga));
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
