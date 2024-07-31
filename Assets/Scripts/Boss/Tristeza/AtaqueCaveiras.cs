using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueCaveiras : MonoBehaviour
{
    public GameObject caveiraPrefab; // Prefab da caveira
    public Transform[] linhasSpawn; // Posições de spawn das linhas de caveiras

    private float intervaloCaveira = 0.5f; // Intervalo entre caveiras na linha
    private float velocidadeCaveira = 5f; // Velocidade das caveiras

    private Coroutine ataqueCaveirasCoroutine;

    public void IniciarAtaque()
    {
        if (ataqueCaveirasCoroutine == null)
        {
            ataqueCaveirasCoroutine = StartCoroutine(LancarCaveiras());
        }
    }

    public void PararAtaque()
    {
        if (ataqueCaveirasCoroutine != null)
        {
            StopCoroutine(ataqueCaveirasCoroutine);
            ataqueCaveirasCoroutine = null;
        }
    }

    private IEnumerator LancarCaveiras()
    {
        while (true)
        {
            foreach (Transform linha in linhasSpawn)
            {
                StartCoroutine(LancarLinhaCaveiras(linha));
            }

            // Tempo de espera entre cada série de caveiras
            yield return new WaitForSeconds(5f);
        }
    }

    private IEnumerator LancarLinhaCaveiras(Transform linha)
    {
        for (int i = 0; i < 5; i++) // Ajuste o número de caveiras conforme necessário
        {
            GameObject caveira = Instantiate(caveiraPrefab, linha.position, Quaternion.identity);
            Rigidbody2D rb = caveira.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.left * velocidadeCaveira; // Ajuste a direção e velocidade conforme necessário

            yield return new WaitForSeconds(intervaloCaveira);
        }
    }

    public void SetIntervaloCaveira(float intervalo)
    {
        this.intervaloCaveira = intervalo;
    }

    public void SetVelocidadeCaveira(float velocidade)
    {
        this.velocidadeCaveira = velocidade;
    }
}
