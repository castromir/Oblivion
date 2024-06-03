using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GerenciadorPlayer : MonoBehaviour
{
    [SerializeField] private PlayerMiraFeitico playerMiraFeitico;
    [SerializeField] private Material marcadorFeiticoMaterial;

    private void Start()
    {
        playerMiraFeitico.AoUsarFeitico += PlayerMiraFeitico_AoUsarFeitico;
    }

    private void PlayerMiraFeitico_AoUsarFeitico(object sender, PlayerMiraFeitico.AoUsarFeiticoEventArgs e)
    {
        UtilsClass.ShakeCamera(0.1f, .1f);
        CriarMarcadorFeitico(e.posicaoPontaLivro, e.posicaoDisparo);
    }

    private void CriarMarcadorFeitico(Vector3 posicaoInicial, Vector3 posicaoFinal) //coordena a textura do disparo arcano
    {
        Vector3 direcao = (posicaoFinal - posicaoInicial).normalized;
        float eulerZ = UtilsClass.GetAngleFromVectorFloat(direcao) - 90;
        float distancia = Vector3.Distance(posicaoInicial, posicaoFinal);
        Vector3 marcadorPosicaoSpawn = posicaoInicial + direcao * distancia * .5f;

        // Create a new material instance to avoid changing the original material
        Material tempMarcadorFeiticoMaterial = new Material(marcadorFeiticoMaterial);
        World_Mesh worldMesh = World_Mesh.Create(marcadorPosicaoSpawn, eulerZ, 6f, distancia, marcadorFeiticoMaterial, null, 10000); //coordena posicao inicial, angulo, distancia, material...

         float timer = .1f;
         //worldMesh.SetUVCoords(new World_Mesh.UVCoords(0, 0, 2, 64));
         FunctionUpdater.Create(() =>
         {
             timer -= Time.deltaTime;
             if (timer <= 0)
             {
                 worldMesh.DestroySelf();
                 return true;
             }
             return false;
         });
    }
}
