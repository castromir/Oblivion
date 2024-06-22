using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instancia { get; private set; } //garante que so exista 1 player

    // public event EventHandler AoLiberarFeitico; //ainda vai ser criado

    private Feiticos Teleporte;
    private Feiticos DisparoArcano;

    public Vector3 RetornarPosicao()
    {
        return transform.position;
    }
}
