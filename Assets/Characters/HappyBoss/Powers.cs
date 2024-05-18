using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers : MonoBehaviour
{
    //Relacionados ao ataque de estrela
    public GameObject player;
    public GameObject starPower;
    public float compensarCamera;
    public float lowestPoit, highestPoit;


    public void SpawnStar()
    {


        Vector3 spawnPos = player.transform.position;
        spawnPos.y = player.transform.position.y + Random.Range(lowestPoit, highestPoit) + compensarCamera;
        spawnPos.x = player.transform.position.x + Random.Range(lowestPoit, highestPoit);

        Instantiate(starPower, spawnPos, Quaternion.identity);




    }
    
    //Relacionados ao ataque de chegar perto
   
    public float dashSpeed;
    public void DashAtacck()
    {
        

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, dashSpeed * Time.deltaTime);

    }
    
}
