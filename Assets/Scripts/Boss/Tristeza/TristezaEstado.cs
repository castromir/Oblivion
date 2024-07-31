using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TristezaEstado : MonoBehaviour
{
    BossTristeza bossTristeza;
    BossVida bossVida;
    AtaqueCaveiras ataqueCaveiras;

    enum BossEstado
    {
        Padrao,
        Machucado,
        MuitoMachucado
    }

    BossEstado estado;

    void Start()
    {
        bossTristeza = GetComponent<BossTristeza>();
        bossVida = GetComponent<BossVida>();
        ataqueCaveiras = GetComponent<AtaqueCaveiras>();
        estado = BossEstado.Padrao;
    }

    void Update()
    {
        if (bossVida.isDead)
        {
            StopAllCoroutines();
            PlayerPrefs.SetInt("tristeza", 1);
            SceneManager.LoadScene("Lobby");
            return;
        }

        switch (estado)
        {
            case BossEstado.Padrao: // Acima de metade da vida
                if (bossVida.vidaAtual <= bossVida.GetVidaMaxima() / 2 && bossVida.vidaAtual > bossVida.GetVidaMaxima() / 4)
                {
                    estado = BossEstado.Machucado;
                }
                else if (bossVida.vidaAtual <= bossVida.GetVidaMaxima() / 4)
                {
                    estado = BossEstado.MuitoMachucado;
                }
                break;

            case BossEstado.Machucado: // Metade da vida até 1/4

                ataqueCaveiras.SetCaveiraSpeed(4.5f);
                ataqueCaveiras.SetCaveirasPorOnda(2);

                if (bossVida.vidaAtual <= bossVida.GetVidaMaxima() / 4)
                {
                    estado = BossEstado.MuitoMachucado;
                }

                bossTristeza.SetIntervaloContaminacao(0.5f);
                bossTristeza.SetTempoContaminacao(1.3f);
                break;

            case BossEstado.MuitoMachucado: // 1/4 pra baixo

                ataqueCaveiras.SetCaveiraSpeed(5.5f);
                ataqueCaveiras.SetCaveirasPorOnda(3);

                bossTristeza.SetIntervaloContaminacao(0.35f);
                bossTristeza.SetTempoContaminacao(1f);
                break;
        }
    }
}
