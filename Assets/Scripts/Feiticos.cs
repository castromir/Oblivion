using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feiticos : ScriptableObject
{
    public string nome;
    public float tempoRecarga;
    public float tempoDuracao;

    public event System.Action<GameObject> AoAtivarMagia;
    public event System.Action<GameObject> AoDesativarMagia;

    public virtual void Ativar(GameObject parente) 
    {
        AoAtivarMagia?.Invoke(parente);
    }
    public virtual void RecargaComecar(GameObject parente)
    {
        AoDesativarMagia?.Invoke(parente);
    }
}

