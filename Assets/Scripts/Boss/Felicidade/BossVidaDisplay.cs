using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossVidaDisplay : MonoBehaviour
{
    public Slider barraVida;
    public BossVida bossVida;

    void Start()
    {
        barraVida.maxValue = bossVida.GetVidaMaxima();
        barraVida.value = bossVida.vidaAtual;
    }

    void Update()
    {
        barraVida.value = bossVida.vidaAtual;
    }
}
