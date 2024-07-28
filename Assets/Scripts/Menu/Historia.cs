using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Historia : MonoBehaviour
{
    private int caso;
    public GameObject fala1;
    public GameObject fala2;
    public GameObject fala3;
    public GameObject fala4;
    public GameObject fala5;
    public GameObject fala6;


    void Start()
    {
        caso = 0;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            caso = 6;
            switchCaso();

        }
    }

    private void switchCaso()
    {

        switch(caso)
        {
            case 0:
                fala1.SetActive(true);
                fala2.SetActive(false);
                fala3.SetActive(false);
                fala4.SetActive(false);
                fala5.SetActive(false);
                fala6.SetActive(false);
                break;
            case 1:
                fala1.SetActive(false);
                fala2.SetActive(true);
                fala3.SetActive(false);
                fala4.SetActive(false);
                fala5.SetActive(false);
                fala6.SetActive(false);
                break;
            case 2:
                fala1.SetActive(false);
                fala2.SetActive(false);
                fala3.SetActive(true);
                fala4.SetActive(false);
                fala5.SetActive(false);
                fala6.SetActive(false);
                break;
            case 3:
                fala1.SetActive(false);
                fala2.SetActive(false);
                fala3.SetActive(false);
                fala4.SetActive(true);
                fala5.SetActive(false);
                fala6.SetActive(false);
                break;
            case 4:
                fala1.SetActive(false);
                fala2.SetActive(false);
                fala3.SetActive(false);
                fala4.SetActive(false);
                fala5.SetActive(true);
                fala6.SetActive(false);
                break;
            case 5:
                fala1.SetActive(false);
                fala2.SetActive(false);
                fala3.SetActive(false);
                fala4.SetActive(false);
                fala5.SetActive(false);
                fala6.SetActive(true);
                break;
            case 6:
                SceneManager.LoadScene(2);
                break;
            default:
                SceneManager.LoadScene(2);
                break;

        }
    }

    public void next()
    {
        if (caso < 8)
        {
            caso++;
            switchCaso();
        }
    }

}
