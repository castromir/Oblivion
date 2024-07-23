using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TristezaEstado : MonoBehaviour
{
    public TristezaAtaqueArea ataqueArea;

    public float ataqueAreaRecarga = TristezaAtaqueArea.ataqueAreaTempoRecarga;
    enum BossEstado
    {
        Padrao,
        AtaqueArea
    }

    BossEstado estado;
    void Start()
    {
        estado = BossEstado.Padrao;
    }

    void Update()
    {
        

        switch (estado) 
        {
            case BossEstado.Padrao:

                ataqueAreaRecarga -= Time.deltaTime;

                if (ataqueAreaRecarga <= 0)
                {
                    estado = BossEstado.AtaqueArea;
                }

                break;

            case BossEstado.AtaqueArea:
                break;
        }
    }
}
