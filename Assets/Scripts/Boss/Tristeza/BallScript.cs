using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{

    public PlayerVida playerVida;
    void Start()
    {
        playerVida = FindObjectOfType<PlayerVida>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")) 
        {
            playerVida.ReceberDano();
            Debug.Log("Bola de neve da dano");
            Destroy(gameObject);
            
        }
        if (other.gameObject.CompareTag("Parede"))
        {
            Debug.Log("Bateu na parede");
            Destroy(gameObject);
        }
       
    }
}
