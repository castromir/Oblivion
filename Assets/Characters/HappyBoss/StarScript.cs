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

    void Start()
    {
        powers = FindObjectOfType<Powers>();
        inicio = transform.position.y;

        Vector3 spawnPos = transform.position;
        spawnPos.y = spawnPos.y - powers.compensarCamera;
        markStar = Instantiate(mark,spawnPos, Quaternion.identity);
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
}
