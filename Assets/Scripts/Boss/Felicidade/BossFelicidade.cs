using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFelicidade : Boss
{
    public GameObject player;
    public float speed;
    public float acceptableDistance;
    private Powers powers;

    private float distance;
    public float timerDistance;
    private float timer = 0f;
    public float dashDistance;

    BossRenderer bossRender;

    void Start()
    {
        powers = GetComponent<Powers>();
        bossRender = GetComponent<BossRenderer>();
    }

    void Update()
    {
        
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        if (distance < acceptableDistance )
        {
            //MoverParaPosicao(player.transform.position, direction, speed, false);
            timer += Time.deltaTime;
            if (timer > timerDistance)
            {
                powers.SpawnStar();
                timer = 0f;
            }
        }
        
    }

    public override string[] GetDirecoesEstaticas()
    {
        return new string[] { "Felicidade Andar N", "Felicidade Andar O", "Felicidade Andar S", "Felicidade Andar L" };
    }

    public override string[] GetDirecoesHabilidade()
    {
        return new string[] { "Felicidade Investida N", "Felicidade Investida O", "Felicidade Investida S", "Felicidade Investida L"};
    }

    public void MoverParaPosicao(Vector3 targetPosition, Vector2 direcao, float velocidade, bool investida)
    {
        bossRender.SetDirection(direcao, investida);
        transform.position = Vector2.MoveTowards(this.transform.position, targetPosition, velocidade * Time.deltaTime);
    }    
}
