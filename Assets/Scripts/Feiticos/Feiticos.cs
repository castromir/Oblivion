using UnityEngine;
using System;

public abstract class Feiticos : ScriptableObject
{
    public string nome;
    public float tempoRecarga;
    public float tempoDuracao;

    public event Action<GameObject> AoAtivarMagia;
    public event Action<GameObject> AoDesativarMagia;

    public abstract void Ativar(GameObject parente);

    public virtual void RecargaComecar(GameObject parente)
    {
        AoDesativarMagia?.Invoke(parente);
    }

    protected void InvokeAoAtivarMagia(GameObject parente)
    {
        AoAtivarMagia?.Invoke(parente);
    }
}
