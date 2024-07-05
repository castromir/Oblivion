using UnityEngine;

public abstract class Boss : MonoBehaviour
{
    public abstract string[] GetDirecoesEstaticas();
    public abstract string[] GetDirecoesHabilidade();
}