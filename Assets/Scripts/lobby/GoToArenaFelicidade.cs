using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GoToArenaFelicidade : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text;
    public GameObject tutorial;
    private Tutorial tutorialScript;
    public bool arenaFeita = false;

    void Start()
    {
        tutorialScript = tutorial.GetComponent<Tutorial>();
        text.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        
           if (other.CompareTag("Player"))
            {
               if (tutorialScript.tutorialComplete)
               {
                    text.gameObject.SetActive(true);
               }

               if (Input.GetKeyDown(KeyCode.E))
               {
                    arenaFeita = true;
                    text.gameObject.SetActive(false);
                    SceneManager.LoadSceneAsync(2); //0 Menu, 1 Lobby, 2 Arena Felicidade, 3 Arena Tristeza, 4 Arena raiva  
               }

            }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            text.gameObject.SetActive(false);
        }
    }
}
