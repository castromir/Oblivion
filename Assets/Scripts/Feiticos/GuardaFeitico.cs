using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GuardaFeitico : MonoBehaviour
{
    public FeiticoEstadoInfo[] feiticos;  // Array de estados de feitiços

    public enum FeiticoEstado
    {
        pronto,
        ativo,
        recarga
    }

    private void OnEnable()
    {
        // Inicializa o estado dos feitiços
        for (int i = 0; i < feiticos.Length; i++)
        {
            if (feiticos[i].feitico == null) continue;

            feiticos[i].feitico.Inicializar();
            feiticos[i].estado = FeiticoEstado.pronto;  // Inicializa o estado como pronto
        }
    }

    void Update()
    {
        for (int i = 0; i < feiticos.Length; i++)
        {
            if (feiticos[i].feitico == null) continue;

            switch (feiticos[i].estado)
            {
                case FeiticoEstado.pronto:
                    if (Input.GetKeyDown(feiticos[i].atalho))
                    {
                        feiticos[i].feitico.Ativar(gameObject);
                        feiticos[i].estado = FeiticoEstado.ativo;
                        feiticos[i].tempoDuracao = feiticos[i].feitico.tempoDuracao;
                    }
                    break;
                case FeiticoEstado.ativo:
                    if (feiticos[i].tempoDuracao > 0)
                    {
                        feiticos[i].tempoDuracao -= Time.deltaTime;
                    }
                    else
                    {
                        feiticos[i].feitico.RecargaComecar(gameObject);
                        feiticos[i].estado = FeiticoEstado.recarga;
                        feiticos[i].tempoRecarga = feiticos[i].feitico.tempoRecarga;
                    }
                    break;
                case FeiticoEstado.recarga:
                    if (feiticos[i].tempoRecarga > 0)
                    {
                        feiticos[i].tempoRecarga -= Time.deltaTime;
                    }
                    else
                    {
                        feiticos[i].estado = FeiticoEstado.pronto;
                    }
                    break;
            }
        }
    }

    private void OnDestroy()
    {
        // Desinscrever-se de eventos
        for (int i = 0; i < feiticos.Length; i++)
        {
            if (feiticos[i].feitico == null) continue;

            feiticos[i].feitico.DesinscreverEventos();
        }
    }
}