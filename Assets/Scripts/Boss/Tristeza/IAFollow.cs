using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAFollow : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float acceptableDistance;
    private float distance;
   
   
    

    void Start()
    {

    }

    void Update()
    {

        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        if (distance < acceptableDistance)
        {
            MoverParaPosicao(player.transform.position, speed);


        }

    }

    public void MoverParaPosicao(Vector3 targetPosition, float velocidade)
    {
        transform.position = Vector2.MoveTowards(this.transform.position, targetPosition, velocidade * Time.deltaTime);
    }
}