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
    private GoToArenaFelicidade arenafeliciaScript;


    void Start()
    {
        tutorialScript = tutorial.GetComponent<Tutorial>();
        arenafeliciaScript = arenaFelicidade.GetComponent<GoToArenaFelicidade>();

        text.gameObject.SetActive(false);
        textNaoPode.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (tutorialScript.tutorialComplete)
        {
            if (arenafeliciaScript.arenaFeita)
            {
                if (other.CompareTag("Player"))
                {
                    text.gameObject.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        text.gameObject.SetActive(false);
                        SceneManager.LoadSceneAsync(1); // 0 é felicidade, 1 é tristeza e 2 é o lobby
                    }
                }
            }
            else
            {
                if (other.CompareTag("Player"))
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

