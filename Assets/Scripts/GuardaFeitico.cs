using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardaFeitico : MonoBehaviour
{
    public Feiticos feitico;
    float tempoRecarga;
    float tempoDuracao;

    enum FeiticoEstado
    {
        pronto,
        ativo,
        recarga
    }
    FeiticoEstado estado = FeiticoEstado.ativo;

    public KeyCode atalho;
    
    void Update()
    {
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
