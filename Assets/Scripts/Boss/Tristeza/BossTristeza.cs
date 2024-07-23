using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTristeza : Boss
{
    private GameObject auraInstancia;
    public GameObject auraPrefab;
    public Transform auraPosicao;

    public float auraScale = 3f;


    void Start()
    {
        // Instancia a aura e a anexa ao BossTristeza
        auraInstancia = Instantiate(auraPrefab, transform.position, Quaternion.identity, auraPosicao);
        // Ajusta a escala da aura
        auraInstancia.transform.localScale = new Vector3(auraScale, auraScale, auraScale);
        // Centraliza a aura no Boss
        auraInstancia.transform.localPosition = Vector3.zero;
    }

    void Update()
    {
        // Qualquer outra lógica específica do BossTristeza
    }

    public override string[] GetDirecoesEstaticas()
    {
        return new string[] { "Felicidade Andar N", "Felicidade Andar O", "Felicidade Andar S", "Felicidade Andar L" };
    }

    public override string[] GetDirecoesHabilidade()
    {
        return new string[] { "Felicidade Investida N", "Felicidade Investida O", "Felicidade Investida S", "Felicidade Investida L" };
    }
}
