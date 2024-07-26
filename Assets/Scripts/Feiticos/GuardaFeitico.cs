using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardaFeitico : MonoBehaviour
{
    public Feiticos[] feiticos;  // Array de feitiços
    public float tempoRecarga;
    public float tempoDuracao;

    public enum FeiticoEstado
    {
        pronto,
        ativo,
        recarga
    }
    public FeiticoEstado estado = FeiticoEstado.pronto;  // Inicializa como pronto

    public KeyCode atalho;

    private void OnEnable()
    {
        // Inicializa o estado dos feitiços
        foreach (Feiticos feitico in feiticos)
        {
            if (feitico == null) continue;

            feitico.Inicializar();
        }

        // Define o estado inicial do GuardaFeitico
        estado = FeiticoEstado.pronto;
    }

    void Update()
    {
        foreach (Feiticos feitico in feiticos)
        {
            if (feitico == null) continue;

            switch (estado)
            {
                case FeiticoEstado.pronto:
                    if (Input.GetKeyDown(atalho))
                    {
                        feitico.Ativar(gameObject);
                        estado = FeiticoEstado.ativo;
                        tempoDuracao = feitico.tempoDuracao;
                    }
                    break;
                case FeiticoEstado.ativo:
                    if (tempoDuracao > 0)
                    {
                        tempoDuracao -= Time.deltaTime;
                    }
                    else
                    {
                        feitico.RecargaComecar(gameObject);
                        estado = FeiticoEstado.recarga;
                        tempoRecarga = feitico.tempoRecarga;
                    }
                    break;
                case FeiticoEstado.recarga:
                    if (tempoRecarga > 0)
                    {
                        tempoRecarga -= Time.deltaTime;
                    }
                    else
                    {
                        estado = FeiticoEstado.pronto;
                    }
                    break;
            }
        }
    }

    private void OnDestroy()
    {
        // Desinscrever-se de eventos
        foreach (Feiticos feitico in feiticos)
        {
            if (feitico == null) continue;

            feitico.DesinscreverEventos();
        }
    }
}
