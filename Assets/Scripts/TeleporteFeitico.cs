using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TeleporteFeitico : Feiticos
{
    public override void Ativar(GameObject parente)
    {
        Vector3 mousePosicao = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosicao.z = 0;

        //if (mousePosicao.y.T)

        parente.transform.position = mousePosicao;
    }
}
