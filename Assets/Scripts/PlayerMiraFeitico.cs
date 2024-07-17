using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMiraFeitico : MonoBehaviour
{
    public event EventHandler<AoUsarFeiticoEventArgs> AoUsarFeitico;
    public class AoUsarFeiticoEventArgs : EventArgs
    {
        public Vector3 posicaoPontaLivro;
        public Vector3 posicaoDisparo;
        public GameObject objetoAcertado;
    }

    private Transform miraTransformar;
    private Transform miraPontaLivroTransformar;
    private Animator livroUsarFeiticoAnimator;

    private void Awake()
    {
        miraTransformar = transform.Find("LivroMira");
        livroUsarFeiticoAnimator = miraTransformar.GetComponent<Animator>();
        miraPontaLivroTransformar = miraTransformar.Find("PosicaoPontaLivro");
    }
    private void Update()
    {
        ManipularMira();
        ManipularDisparo();

        if (livroUsarFeiticoAnimator.GetCurrentAnimatorStateInfo(0).IsName("livroUsarFeitico") &&
            livroUsarFeiticoAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            livroUsarFeiticoAnimator.SetBool("estaAtivo", false);
        }
    }

    private void ManipularMira()
    {
        if (miraTransformar != null)
        {
            Vector3 mousePosicao = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosicao.z = 0;

            Vector3 miraDirecao = (mousePosicao - transform.position).normalized;
            float angulo = Mathf.Atan2(miraDirecao.y, miraDirecao.x) * Mathf.Rad2Deg;

            angulo -= 90f; // Deixar a parte de frente do livro mirando para o cursor

            miraTransformar.eulerAngles = new Vector3(0, 0, angulo);
        }
    }

    private void AtivarAnimacao(GameObject parente)
    {
        livroUsarFeiticoAnimator.SetTrigger("AtivarMagia");
        Debug.Log("Ativou");
    }

    private void DesativarAnimacao(GameObject parente)
    {
        livroUsarFeiticoAnimator.SetTrigger("DesativarMagia");
        Debug.Log("Desativou");
    }

    private void ManipularDisparo()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosicao = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosicao.z = 0;

            livroUsarFeiticoAnimator.SetTrigger("AtivarMagia"); //ao usar magia ativa a animacao
            livroUsarFeiticoAnimator.SetBool("estaAtivo", true); //checar se a animacao acabou

            AoUsarFeitico?.Invoke(this, new AoUsarFeiticoEventArgs
            {
                posicaoPontaLivro = miraPontaLivroTransformar.position,
                posicaoDisparo = mousePosicao,
            });
        }

    }
}
