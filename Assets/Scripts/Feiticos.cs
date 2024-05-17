using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feiticos : ScriptableObject
{
    public string nome;
    public float tempoRecarga;
    public float tempoDuracao;
    public event System.Action<GameObject> OnAtivarMagia;
    public event System.Action<GameObject> OnDesativarMagia;

    public virtual void Ativar(GameObject parente) 
    {
        OnAtivarMagia?.Invoke(parente);
    }
    public virtual void RecargaComecar(GameObject parente)
    {
        OnDesativarMagia?.Invoke(parente);
    }
}

