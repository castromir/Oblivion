using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TeleporteFeitico : Feiticos
{
    public GameObject testePrefab;
    public override void Ativar(GameObject parente)
    {
        Vector3 mousePosicao = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosicao.z = 0;

        GameObject testeInstance = Instantiate(testePrefab, mousePosicao, Quaternion.identity);

       

        TesteDoTeleport testeScript = testeInstance.GetComponent<TesteDoTeleport>();

        if (testeScript.teste)
        {
            parente.transform.position = mousePosicao;
        }

        Destroy(testeInstance);

    }
}
