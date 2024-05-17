using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMiraFeitico : MonoBehaviour
{
    private Transform miraTransformar;
    private Animator livroUsarFeiticoAnimator;
    public Feiticos feiticoAtual; // variavel para armazenar o feitico atual

    private void Start()
    {
        miraTransformar = transform.Find("LivroMira");
        livroUsarFeiticoAnimator = miraTransformar.GetComponent<Animator>();

        // evento OnMagia do feitiço atual
        if (feiticoAtual != null)
        {
            Debug.Log("Func");
            feiticoAtual.OnAtivarMagia += AtivarAnimacao;
            feiticoAtual.OnDesativarMagia += DesativarAnimacao;
        }
        else
        {
            Debug.LogError("feiticoAtual é nulo no Start");
        }
    }
    private void Update()
    {
        ManipularMira();
        ManipularDisparo();
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
            Debug.Log(angulo);
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
        if (Input.GetKeyDown(KeyCode.Space))
            livroUsarFeiticoAnimator.SetTrigger("AtivarMagia");

        if (Input.GetKeyDown(KeyCode.V))
            livroUsarFeiticoAnimator.SetTrigger("DesativarMagia");
    }
}
