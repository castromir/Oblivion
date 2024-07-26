using UnityEngine;
using System;

public abstract class Feiticos : ScriptableObject
{
    public string nome;
    public float tempoRecarga;
    public float tempoDuracao;

    public event Action<GameObject> AoAtivarMagia;
    public event Action<GameObject> AoDesativarMagia;

    // Método abstrato que deve ser implementado nas classes derivadas
    public abstract void Ativar(GameObject parente);

    // Método virtual que pode ser sobrescrito nas classes derivadas
    public virtual void RecargaComecar(GameObject parente)
    {
        AoDesativarMagia?.Invoke(parente);
    }

    // Método para inicializar ou resetar o feitiço se necessário
    public virtual void Inicializar()
    {
        // Inicialização de propriedades específicas do feitiço
        // Pode ser sobrescrito nas classes derivadas para comportamento específico
    }

    // Método protegido para invocar o evento AoAtivarMagia
    protected void InvokeAoAtivarMagia(GameObject parente)
    {
        AoAtivarMagia?.Invoke(parente);
    }

    // Método para desinscrever todos os eventos
    public void DesinscreverEventos()
    {
        AoAtivarMagia = null;
        AoDesativarMagia = null;
    }
}
