using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteDoTeleport : MonoBehaviour
{
    public bool teste = false;
    // Start is called before the first frame update


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Chao"))
        {
            teste = true;
            Debug.Log("Esta no chao");
        }
        Debug.Log("Colisao detectada");
        
    }
}
