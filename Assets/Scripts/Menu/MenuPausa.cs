using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuPausa : MonoBehaviour
{
    public GameObject painelPause;
    // Start is called before the first frame update
  

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    private void Pause()
    {
        painelPause.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        painelPause.SetActive(false);
        Time.timeScale = 1;
    }

    public void Sair()
    {
        //se estiver jogando no editor da unity usar o comando abaixo, caso contrário, coloca-lo como comentário.
        UnityEditor.EditorApplication.isPlaying = false;

        //Se o jogo estiver compilado usar comando abaixo.
        //Application.Quit();
    }
}
