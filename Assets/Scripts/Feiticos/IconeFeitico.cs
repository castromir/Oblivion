using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IconeFeitico : MonoBehaviour
{
    public Image iconeImagem;
    public Text tempoRecargaTexto;
    public GuardaFeitico guardaFeitico;
    public int feiticoIndex; // Índice do feitiço na lista de feiticos do GuardaFeitico

    private GuardaFeitico.FeiticoData feiticoData;

    private void Start()
    {
        guardaFeitico = GetComponent<GuardaFeitico>();
        feiticoData = guardaFeitico.feiticos[feiticoIndex];
        feiticoData.feitico.AoDesativarMagia += IniciarRecarga;
    }

    private void Update()
    {
        if (feiticoData.estado == GuardaFeitico.FeiticoEstado.recarga)
        {
            AtualizarIconeRecarga(feiticoData.tempoRecargaAtual);
        }
    }

    private void IniciarRecarga(GameObject parente)
    {
        StartCoroutine(RecargaCoroutine(feiticoData.feitico.tempoRecarga));
    }

    private IEnumerator RecargaCoroutine(float duracao)
    {
        iconeImagem.color = Color.gray;

        float tempoRestante = duracao;
        while (tempoRestante > 0)
        {
            tempoRestante -= Time.deltaTime;
            AtualizarIconeRecarga(tempoRestante);
            yield return null;
        }

        iconeImagem.color = Color.white;
        tempoRecargaTexto.text = "";
    }

    private void AtualizarIconeRecarga(float tempoRestante)
    {
        tempoRecargaTexto.text = Mathf.Ceil(tempoRestante).ToString();
    }
}
