using UnityEngine;

[CreateAssetMenu]
public class TeleporteFeitico : Feiticos
{
    public override void Ativar(GameObject parente)
    {
        Vector3 mousePosicao = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosicao.z = 0;

        parente.transform.position = mousePosicao;

        InvokeAoAtivarMagia(parente); // Chama o evento ao ativar a magia
    }
}
