using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class FeiticoEstadoInfo
{
    public string nome; // Nome do feitiço
    public Feiticos feitico; // Referência ao script do feitiço
    public GuardaFeitico.FeiticoEstado estado;
    public float tempoDuracao;
    public float tempoRecarga;
    public KeyCode atalho; // Tecla de atalho para ativar o feitiço
}