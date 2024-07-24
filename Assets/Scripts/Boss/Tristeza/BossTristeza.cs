using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTristeza : Boss
{
    private GameObject auraInstancia;
    public GameObject auraPrefab;
    public Transform auraPosicao;

    [SerializeField] private float auraScale = 4f;
    private float auraAtaqueScale = 6f;

    public GameObject avisoPrefab;
    private List<Vector3> quadradosPosicoes = new List<Vector3>();
    private List<Vector3> avisoPosicoes = new List<Vector3>();

    public float intervaloContaminacao = 5f; // Intervalo entre as mudanças de contaminação
    public float tempoContaminacao = 5f; // Tempo que a contaminação permanece
    private Coroutine contaminacaoCoroutine;

    void Start()
    {
        // Definir posições dos quadrados (losangos) para auras
        quadradosPosicoes.Add(new Vector3(-8.5f, 0.5f, 0f)); // Ajuste de acordo com a posição real dos quadrados na sua arena
        quadradosPosicoes.Add(new Vector3(-0.25f, 4.5f, 0f));
        quadradosPosicoes.Add(new Vector3(8f, 0.25f, 0f));
        quadradosPosicoes.Add(new Vector3(-0.05f, -3.8f, 0f));

        // Definir posições dos avisos
        avisoPosicoes.Add(new Vector3(-8.5f, 0.5f, 0f)); // Ajuste de acordo com a posição real dos avisos na sua arena
        avisoPosicoes.Add(new Vector3(-0.25f, 4.5f, 0f));
        avisoPosicoes.Add(new Vector3(8f, 0.25f, 0f));
        avisoPosicoes.Add(new Vector3(-0.05f, -3.8f, 0f));

        // Instancia a aura e a anexa ao BossTristeza
        auraInstancia = Instantiate(auraPrefab, transform.position, Quaternion.identity, auraPosicao);
        // Ajusta a escala da aura
        auraInstancia.transform.localScale = new Vector3(auraScale, auraScale, auraScale);
        // Centraliza a aura no Boss
        auraInstancia.transform.localPosition = Vector3.zero;

        // Inicia a contaminação dos quadrados
        contaminacaoCoroutine = StartCoroutine(ContaminarQuadrados());
    }

    void Update()
    {
        // Qualquer outra lógica específica do BossTristeza
    }

    private IEnumerator ContaminarQuadrados()
    {
        while (true)
        {
            // Escolher três quadrados aleatórios
            List<int> indices = new List<int>();
            while (indices.Count < 3)
            {
                int newIndex = Random.Range(0, quadradosPosicoes.Count);
                if (!indices.Contains(newIndex))
                {
                    indices.Add(newIndex);
                }
            }

            // Escolher posições de aviso correspondentes
            Vector3 avisoPos1 = avisoPosicoes[indices[0]];
            Vector3 avisoPos2 = avisoPosicoes[indices[1]];
            Vector3 avisoPos3 = avisoPosicoes[indices[2]];

            // Instanciar avisos nos quadrados selecionados
            GameObject aviso1 = Instantiate(avisoPrefab, avisoPos1, Quaternion.identity);
            GameObject aviso2 = Instantiate(avisoPrefab, avisoPos2, Quaternion.identity);
            GameObject aviso3 = Instantiate(avisoPrefab, avisoPos3, Quaternion.identity);

            aviso1.transform.localScale = new Vector3(5f, 5f, 5f);
            aviso2.transform.localScale = new Vector3(5f, 5f, 5f);
            aviso3.transform.localScale = new Vector3(5f, 5f, 5f);

            // Esperar um tempo antes de instanciar as auras
            yield return new WaitForSeconds(2f); // Ajuste o tempo do aviso conforme necessário

            // Instanciar as auras nos quadrados selecionados
            GameObject aura1 = Instantiate(auraPrefab, quadradosPosicoes[indices[0]], Quaternion.identity);
            GameObject aura2 = Instantiate(auraPrefab, quadradosPosicoes[indices[1]], Quaternion.identity);
            GameObject aura3 = Instantiate(auraPrefab, quadradosPosicoes[indices[2]], Quaternion.identity);

            aura1.transform.localScale = new Vector3(auraAtaqueScale, auraAtaqueScale, auraAtaqueScale);
            aura2.transform.localScale = new Vector3(auraAtaqueScale, auraAtaqueScale, auraAtaqueScale);
            aura3.transform.localScale = new Vector3(auraAtaqueScale, auraAtaqueScale, auraAtaqueScale);

            // Destruir os avisos
            Destroy(aviso1);
            Destroy(aviso2);
            Destroy(aviso3);

            // Esperar tempo de contaminação antes de destruir as auras
            yield return new WaitForSeconds(tempoContaminacao);

            // Destruir as auras
            Destroy(aura1);
            Destroy(aura2);
            Destroy(aura3);

            // Esperar um tempo antes de contaminar novos quadrados
            yield return new WaitForSeconds(intervaloContaminacao);
        }
    }

    public override string[] GetDirecoesEstaticas()
    {
        return new string[] { "Felicidade Andar N", "Felicidade Andar O", "Felicidade Andar S", "Felicidade Andar L" };
    }

    public override string[] GetDirecoesHabilidade()
    {
        return new string[] { "Felicidade Investida N", "Felicidade Investida O", "Felicidade Investida S", "Felicidade Investida L" };
    }
}
