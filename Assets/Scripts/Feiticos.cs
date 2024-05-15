using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feiticos : ScriptableObject
{
    public string nome;
    public float tempoRecarga;
    public float tempoDuracao;

    public virtual void Ativar(GameObject parente) {}
    public virtual void RecargaComecar(GameObject parente) {}
}

