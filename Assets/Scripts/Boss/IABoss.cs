using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABoss : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float acceptableDistance;
    private Powers powers;

    private float distance;
    public float timerDistance;
    private float timer = 0f;
    public float dashDistance;

    void Start()
    {
        powers = GetComponent<Powers>();
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        if (distance < acceptableDistance)
        {
            MoverParaPosicao(player.transform.position, speed);
            timer += Time.deltaTime;
            if (timer > timerDistance)
            {
                powers.SpawnStar();
                timer = 0f;
            }
        }
        if (distance < dashDistance)
        {
            powers.DashAtacck();
        }
    }

    public void MoverParaPosicao(Vector3 targetPosition, float velocidade)
    {
        transform.position = Vector2.MoveTowards(this.transform.position, targetPosition, velocidade * Time.deltaTime);
    }
}
