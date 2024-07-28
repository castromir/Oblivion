using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GoToArenaTristeza : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text;
    public Text textNaoPode;
    public GameObject tutorial;
    private Tutorial tutorialScript;

    public GameObject arenaFelicidade;
    private GoToArenaFelicidade arenafelicidade;


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

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        
            if (GoToArenaFelicidade.arenaFelicidadeFeita)
            {
                if (other.CompareTag("Player"))
                {
                    if (PlayerPrefs.GetInt("felicidade", 0) == 1)
                    {
                        text.gameObject.SetActive(true);
                    }
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        text.gameObject.SetActive(false);
                        SceneManager.LoadSceneAsync(4); //0 Menu,1 Hitoria, 2 Lobby, 3 Arena Felicidade, 4 Arena Tristeza, 5 Arena raiva
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
        }
    }
}

