using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfTutorial : MonoBehaviour
{
    // void Start() 
    // {
    //     gameObject.SetActive(false);
    // }
    // void Update() 
    // {
    //     GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    //     if(enemies.Length == 0)   
    //     {
    //         gameObject.SetActive(true);
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(3);
        }
    }
}
