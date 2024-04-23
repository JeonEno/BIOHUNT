using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public static PlayerHealthManager instance;

    [Header("Health")]
    public int currentHealth;
    public int maxHealth;

    [Header("Invinceble")]
    public float damageInvinceLenght = 1f;
    private float invinceCount;


    private void Awake() 
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(invinceCount > 0)
        {
            invinceCount -= Time.deltaTime;
        }
    }

    public void DamagePlayer()
    {
        if(invinceCount <= 0)
        {
            currentHealth -= 25;

            invinceCount = damageInvinceLenght;
        
            if(currentHealth <= 0)
            {
                //PlayerController.instance.gameObject.SetActive(false);
                PlayerController.instance.anim.SetTrigger("isDead");
                PlayerController.instance.moveSpeed = 0;

                UIController.instance.deathScreen.SetActive(true);
                Cursor.visible = true;
            }
            UIController.instance.healthSlider.maxValue = maxHealth;
            UIController.instance.healthSlider.value = currentHealth;
        }
    }
}
