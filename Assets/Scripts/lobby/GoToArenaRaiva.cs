using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GoToArenaRaiva : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text;
    public Text textNaoPode;
    public GameObject tutorial;
    private Tutorial tutorialScript;

    public GameObject arenaFelicidade;
    private GoToArenaFelicidade arenafelicidade;

    private bool podeIrPraArena = false;


    void Start()
    {
        tutorialScript = tutorial.GetComponent<Tutorial>();
        arenafelicidade = arenaFelicidade.GetComponent<GoToArenaFelicidade>();

        text.gameObject.SetActive(false);
        textNaoPode.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (podeIrPraArena)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                text.gameObject.SetActive(false);
                SceneManager.LoadSceneAsync(5); //0 Menu,1 Hitoria, 2 Lobby, 3 Arena Felicidade, 4 Arena Tristeza, 5 Arena raiva
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GoToArenaFelicidade.arenaFelicidadeFeita && PlayerPrefs.GetInt("tristeza") == 1)
        {
            if (other.CompareTag("Player"))
            {
                if (PlayerPrefs.GetInt("felicidade", 0) == 1)
                {
                    text.gameObject.SetActive(true);
                    podeIrPraArena = true;

                }
            }

        }
        else
        {
            if (other.CompareTag("Player"))
            {
                if (tutorialScript.tutorialComplete)
                {
                    textNaoPode.gameObject.SetActive(true);
                    podeIrPraArena = false;
                }
            }
        }

        if (podeIrPraArena == false)
        {
            if (other.CompareTag("Player"))
            {
                if (tutorialScript.tutorialComplete)
                {
                    textNaoPode.gameObject.SetActive(true);
                    podeIrPraArena = false;

                }


            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            text.gameObject.SetActive(false);
            textNaoPode.gameObject.SetActive(false);
            podeIrPraArena = false;
        }
    }
}

