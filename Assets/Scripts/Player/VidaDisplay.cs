using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaDisplay : MonoBehaviour
{
    public int vidaAtual;
    public int vidaMaxima;

    public Sprite  slotVidaVazio;
    public Sprite  slotVidaCheio;
    public Image[] slotsVida;

    public  PlayerVida playerVida;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vidaAtual  = playerVida.vidaAtual;
        vidaMaxima = playerVida.vidaMaxima;

        for (int i = 0; i < slotsVida.Length; i++)
        {
            //alterna entre o slot cheio e o vazio
            if (i < vidaAtual)
            {
                slotsVida[i].sprite = slotVidaCheio;
            }
            else
            {
                slotsVida[i].sprite = slotVidaVazio;
            }

            //mostra o slot = vida maxima do player
            if (i < vidaMaxima)
            {
                slotsVida[i].enabled = true;
            }
            else
            {
                slotsVida[i].enabled = false;
            }
        }
    }
}
