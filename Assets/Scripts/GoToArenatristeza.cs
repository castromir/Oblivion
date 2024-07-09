﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GoToArenatristeza : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
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

    private void OnTriggerExitt2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            text.gameObject.SetActive(false);
    }
}