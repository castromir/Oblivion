using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GoToArenaTristeza : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text;
    void Start()
    {
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
            text.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                text.gameObject.SetActive(false);
                SceneManager.LoadSceneAsync(1); // 0 é felicidade, 1 é tristeza e 2 é o lobby
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

