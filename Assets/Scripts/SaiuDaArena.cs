using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaiuDaArena : MonoBehaviour
{
    public GameObject player;
   public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")){

            player.transform.position = new Vector2(0, 0);
        }
    }
}
