using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoFonte : MonoBehaviour
{
    public PlayerVida playerVida;
    private void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.tag == "Player")
        {
            playerVida.ReceberDano();
        }
    }
}
