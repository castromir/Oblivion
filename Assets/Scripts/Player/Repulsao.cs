using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repulsao : MonoBehaviour
{
    public bool sofrendoRepulsao {  get; private set; }

    [SerializeField] private float repulsaoTempo = .2f;

    private Rigidbody2D rigbody;
    
    private void Awake()
    {
        rigbody = GetComponent<Rigidbody2D>();
    }

    public void SofrerRepulsao(Transform fonteDano, float repulsaoImpulso)
    {
        sofrendoRepulsao = true;
        Vector2 diferenca = (transform.position - fonteDano.position).normalized * repulsaoImpulso * rigbody.mass;
        rigbody.AddForce(diferenca, ForceMode2D.Impulse);
        StartCoroutine(RepulsaoRotina());
    }   

    private IEnumerator RepulsaoRotina()
    {
        yield return new WaitForSeconds (repulsaoTempo);
        rigbody.velocity = Vector2.zero;
        sofrendoRepulsao = false;
    }
}
