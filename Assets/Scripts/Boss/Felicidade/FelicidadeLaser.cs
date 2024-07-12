using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FelicidadeLaser : MonoBehaviour
{
    [SerializeField] public static float laserTempoRecarga = 8f;
    public float laserRecarga = laserTempoRecarga;
    public float laserDistancia = 100f;

    private void Awake()
    {

    }

    private void Update()
    {
        laserRecarga -= Time.deltaTime;
    }

    public bool ConsegueLaser()
    {
        if (laserRecarga > 0)
        {
            return false;
        }
        return true;
    }
}
