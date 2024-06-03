using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

    // Start is called before the first frame update
    void Start()
    {
        powers = GetComponent<Powers>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        if (distance < acceptableDistance)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

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


}
