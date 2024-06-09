using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [Header("Health")]
    public Slider healthSlider;
    [Header("Ammo")]
    public TextMeshProUGUI ammoText;
    [Header("Special Pop UI")]
    public GameObject deathScreen;
    public GameObject needReloadMessage;
    public GameObject nowReloadingMessage;

    private void Awake() 
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
