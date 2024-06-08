using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour
{
    private float inicio;
    public float moveSpeed;
    public Powers powers;
    private GameObject markStar;
    public GameObject mark;
    public PlayerVida playerVida;
    private bool isOnGoud;

    void Start()
    {
        isOnGoud = false;

        powers = FindObjectOfType<Powers>();
        inicio = transform.position.y;

        Vector3 spawnPos = transform.position;
        spawnPos.y = spawnPos.y - powers.compensarCamera;
        markStar = Instantiate(mark,spawnPos, Quaternion.identity);

        playerVida = FindObjectOfType<PlayerVida>();
    }

    // Update is called once per frame
    void Update()
    {
    
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if (transform.position.y < inicio - powers.compensarCamera)
        {
            
            Destroy(gameObject);
            Destroy(markStar);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mark"))
        {
            isOnGoud = true;
            Debug.Log("ta no chao");
        }
        

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerVida.ReceberDano();
            Debug.Log("ta escostando");
        }
    }
}
