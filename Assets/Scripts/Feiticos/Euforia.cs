using UnityEngine;

[CreateAssetMenu]
public class Euforia : Feiticos
{
    public override void Ativar(GameObject parente)
    {

        PlayerVida playerVida = parente.GetComponent<PlayerVida>();
        if (playerVida != null)
        {
            playerVida.AtivarEuforia();
        }
        InvokeAoAtivarMagia(parente); // Chama o evento ao ativar a magia
    }
}
