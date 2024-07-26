using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FeiticoEstadoInfo
{
    public Feiticos feitico;
    public GuardaFeitico.FeiticoEstado estado = GuardaFeitico.FeiticoEstado.pronto;
    public float tempoRecarga;
    public float tempoDuracao;
    public KeyCode atalho;
}