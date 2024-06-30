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
        if (distance < acceptableDistance )
        {
            MoverParaPosicao(player.transform.position, speed);
            timer += Time.deltaTime;
            if (timer > timerDistance)
            {
                powers.SpawnStar();
                timer = 0f;
            }
        }
        
    }

    public void MoverParaPosicao(Vector3 targetPosition, float velocidade)
    {
        transform.position = Vector2.MoveTowards(this.transform.position, targetPosition, velocidade * Time.deltaTime);
    }
    /**
    //Dash
    private bool allowedToDash;
    private bool inDash;
    private float dashingPower = 10f;
    private float dashingTime = 0.1f;
    private float dashingCooldown = 10f;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private Rigidbody2D rb;




    public IEnumerator DashAtacck()
    {
        allowedToDash = false;
        inDash = true;
        float sideX, sideY;

        if (player.transform.position.x - gameObject.transform.position.x < 0)
            sideX = -1;
        else
            sideX = 1;

        if (player.transform.position.y - gameObject.transform.position.y < 0)
            sideY = -1;
        else
            sideY = 1;

        rb.velocity = new Vector2(sideX * dashingPower, sideY * dashingPower);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        inDash = false;
        yield return new WaitForSeconds(dashingCooldown);
        allowedToDash = true;


    }

    //Dentro do Update
    if (distance < dashDistance && allowedToDash)
        {
            StartCoroutine(DashAtacck());
        }
    **/
}
