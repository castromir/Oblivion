using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    public GameObject painelCreditos;
    public GameObject painelOpcoes;
    public AudioSource hoverSound;
    public AudioSource musica;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Novo_Jogo()
    {
        //colocar o nome da primera cena do jogo;
        PlayerPrefs.SetInt("tutorial", 0);//Starta tutorial com um valor padrao a cada novo jogo
        PlayerPrefs.SetInt("euforiaComplete", 0);//starta euforia com um valor padrao a cada novo jogo
        PlayerPrefs.SetInt("felicidade", 0);// starta a arena da felicidade como padrao a cada novo jogo
        PlayerPrefs.SetInt("melancoliaComplete", 0);
        PlayerPrefs.SetInt("tristeza", 0);
        SceneManager.LoadScene(1); //0 Menu,1 Hitoria, 2 Lobby, 3 Arena Felicidade, 4 Arena Tristeza, 5 Arena raiva
    }

    public void Continuar()
    {
        //colocar o nome da cena que vai continuar;
        SceneManager.LoadScene("");
    }

    public void Sair()
    {
        //se estiver jogando no editor da unity usar o comando abaixo, caso contrário, coloca-lo como comentário.
       // UnityEditor.EditorApplication.isPlaying = false;

        //Se o jogo estiver compilado usar comando abaixo.
        Application.Quit();
    }

    public void MostrarPainelCredito()
    {
        painelCreditos.SetActive(true);
    }
    public void MostrarPainelOpcoes()
    {
        painelOpcoes.SetActive(true);
    }


    public void VoltarMenuCreditos()
    {
        painelCreditos.SetActive(false);
    }
    public void VoltarMenuOpcoes()
    {
        painelOpcoes.SetActive(false);
    }

    public void PlayHoverSound()
    {
        if (hoverSound != null)
        {
            hoverSound.Play();
        }
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
}
