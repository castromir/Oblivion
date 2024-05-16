using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMiraFeitico : MonoBehaviour
{
    private Transform miraTransformar;
    public Feiticos feiticoAtual; // variavel para armazenar o feitico atual

    private void Start()
    {
        miraTransformar = transform.Find("LivroMira");
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

            angulo -= 90f;

            miraTransformar.eulerAngles = new Vector3(0, 0, angulo);
            Debug.Log(angulo);
        }
    }
    
    private void ManipularDisparo()
    {
        if (feiticoAtual != null)
        {
            feiticoAtual.Ativar(gameObject);
        }
        else
        {
            Debug.LogWarning("Nenhum feitiço atual selecionado!");
        }
    }
}
