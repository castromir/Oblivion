using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuPausa : MonoBehaviour
{
    public GameObject painelOpcoes;
    public AudioSource hoverSound;
    public AudioSource musica;
    public GameObject painelPause;
    private float valorMusica;
    private float valorEfeito;
    public Slider efeitoSlider;
    public Slider musicaSlider;


    void Start()
    {
        float valorMusica = PlayerPrefs.GetFloat("musica");
        float valorEfeito = PlayerPrefs.GetFloat("efeito");
        VolumeMusica(valorMusica);
        VolumeEfeitos(valorEfeito);
        efeitoSlider.value = valorEfeito;
        musicaSlider.value = valorMusica;
    }

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
        //Time.timeScale = 0;
    }

    public void Continue()
    {
        painelPause.SetActive(false);
        //Time.timeScale = 1;
    }

    public void Sair()
    {
        //se estiver jogando no editor da unity usar o comando abaixo, caso contrário, coloca-lo como comentário.
        //UnityEditor.EditorApplication.isPlaying = false;

        //Se o jogo estiver compilado usar comando abaixo.
        Application.Quit();
    }
    public void VolumeMusica(float value)
    {
        musica.volume = value;
        float valorMusica = value;
        PlayerPrefs.SetFloat("musica", value);
    }
    public void VolumeEfeitos(float value)
    {
        hoverSound.volume = value;
        float valorEfeito = value;
        PlayerPrefs.SetFloat("efeito", value);
    }
    public void VoltarMenuOpcoes()
    {
        painelOpcoes.SetActive(false);
    }
    public void MostrarPainelOpcoes()
    {
        painelOpcoes.SetActive(true);
    }
    public void PlayHoverSound()
    {
        if (hoverSound != null)
        {
            hoverSound.Play();
        }
    }
}

