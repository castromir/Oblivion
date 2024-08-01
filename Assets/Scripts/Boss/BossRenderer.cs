using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRenderer : MonoBehaviour
{
    private Boss boss;
    private Animator animator;
    private int lastDirection;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boss = GetComponent<Boss>();
    }

    public void SetDirection(Vector2 direction, bool investida)
    {
        string[] directionArray;
        if (investida)
            directionArray = boss.GetDirecoesHabilidade();
        else
            directionArray = boss.GetDirecoesEstaticas();

        lastDirection = DirectionToIndex(direction, 4);

        if (directionArray != null && directionArray.Length > lastDirection)
        {
            animator.Play(directionArray[lastDirection]);
        }
    }

    public static int DirectionToIndex(Vector2 dir, int sliceCount)
    {
        Vector2 normDir = dir.normalized;
        float step = 360f / sliceCount;
        float halfStep = step / 2;
        float angle = Vector2.SignedAngle(Vector2.up, normDir);
        angle += halfStep;
        if (angle < 0)
        {
            angle += 360;
        }
        float stepCount = angle / step;
        return Mathf.FloorToInt(stepCount);
    }
}
