using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoArcano : MonoBehaviour 
{
    [SerializeField] private PlayerMiraFeitico playerMiraFeitico;
    [SerializeField] private Material marcadorFeiticoMaterial;

    [SerializeField] private int dano = 10;

    private void Start()
    {
        playerMiraFeitico.AoUsarFeitico += PlayerMiraFeitico_AoUsarFeitico;
    }

    private void PlayerMiraFeitico_AoUsarFeitico(object sender, PlayerMiraFeitico.AoUsarFeiticoEventArgs e)
    {
        Vector3 direcaoDisparo = (e.posicaoDisparo - e.posicaoPontaLivro).normalized;

        UtilsClass.ShakeCamera(0.1f, .1f);
        DisparoArcanoRaycast.Disparar(e.posicaoPontaLivro, direcaoDisparo, dano);
        CriarMarcadorFeitico(e.posicaoPontaLivro, e.posicaoDisparo);
    }

    private void CriarMarcadorFeitico(Vector3 posicaoInicial, Vector3 posicaoFinal) //coordena a textura do disparo arcano
    {
        Vector3 direcao = (posicaoFinal - posicaoInicial).normalized;
        float eulerZ = UtilsClass.GetAngleFromVectorFloat(direcao) - 90;
        float distancia = Vector3.Distance(posicaoInicial, posicaoFinal);
        Vector3 marcadorPosicaoSpawn = posicaoInicial + direcao * distancia * .5f;

        World_Mesh worldMesh = World_Mesh.Create(marcadorPosicaoSpawn, eulerZ, 3f, distancia, marcadorFeiticoMaterial, null, 10000); //coordena posicao inicial, angulo, distancia, material...

        float timer = .1f;
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
