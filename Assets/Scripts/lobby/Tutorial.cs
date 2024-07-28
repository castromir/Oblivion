using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private int caso=0;
    public GameObject movimentacao;
    public GameObject teleport;
    public GameObject ataque;
    public GameObject tutorial;
    public bool tutorialComplete = false;
    public int tutorialCompleteInt;

    public int euforiaComplete;
    public GameObject euforia;

    public int arenaFelicidadeInt;

    void Start()
    {
        caso = 0;
        tutorialCompleteInt = PlayerPrefs.GetInt("tutorial");
        arenaFelicidadeInt = PlayerPrefs.GetInt("felicidade");
        euforiaComplete = PlayerPrefs.GetInt("euforiaComplete");


        if (tutorialCompleteInt == 1)
        {
            tutorialComplete = true;
            tutorial.SetActive(false);
        }
        
        if (arenaFelicidadeInt == 1)
        {
            tutorialComplete = false;
            tutorial.SetActive(false);

            euforia.SetActive(true);
            
            
            
        }
        if (euforiaComplete == 1)
        {
            euforia.SetActive(false);
        }
    }

    private void switchTutorial()
    {
        switch (caso)
        {
            case 0:
                movimentacao.SetActive(true);
                teleport.SetActive(false);
                ataque.SetActive(false);
                break;
            case 1:
                movimentacao.SetActive(false);
                teleport.SetActive(true);
                ataque.SetActive(false);
                break;
            case 2:
                movimentacao.SetActive(false);
                teleport.SetActive(false);
                ataque.SetActive(true);
                break;
            default:
                movimentacao.SetActive(false);
                teleport.SetActive(false);
                ataque.SetActive(false);
                break;
        }
    }

    public void prox()
    {
        if(caso<2)
        {
            caso++;
            switchTutorial();
        }
    }
    public void ant()
    {
        if (caso>0)
        {
            caso--;
            switchTutorial();
        }
    }
    
    public void tutorialCompleto()
    {
        tutorialComplete = true;
        tutorial.SetActive(false);
        PlayerPrefs.SetInt("tutorial", 1);
    }
    public void euforiaCompleto()
    {
        euforia.SetActive(false);
        PlayerPrefs.SetInt("euforiaComplete", 1);    
    }

}
