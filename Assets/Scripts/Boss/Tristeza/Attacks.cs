using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 5f;
    public int numberOfProjectiles = 8;
    public float spreadAngle = 360f;

    private float canAttack=0f;
    public float AttackTime =5f;

    void Start()
    {
        
    }
    void Update()
    {
        canAttack += Time.deltaTime;
        if(canAttack > AttackTime) 
            LaunchProjectilesInAllDirections();
            
    }

    void LaunchProjectilesInAllDirections()
    {
        float angleStep = spreadAngle / numberOfProjectiles;
        float angle = 0f;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float projectileDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float projectileDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 projectileMoveVector = new Vector3(projectileDirX, projectileDirY, 0f);
            Vector2 projectileDir = (projectileMoveVector - transform.position).normalized;

            GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileDir.x * projectileSpeed, projectileDir.y * projectileSpeed);

            angle += angleStep;
        }
        canAttack = 0f;
    }
}

