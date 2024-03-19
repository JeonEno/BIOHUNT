using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    [Header("Component")]
    public Rigidbody2D theRB;

    [Header("Movement Value")]
    public float moveSpeed;
    public float rangeToChasePlayer;
    private Vector3 moveDirection;
    public SpriteRenderer spRenderer;

    [Header("Animation & Effect")]
    public Animator anim;
    public GameObject gotHitEffect;

    [Header("Health")]
    public int health = 150;
    // public float damageInterval = 1f; // Interval between each damage to the player
    // private float damageTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer)
        {
            moveDirection = PlayerController.instance.transform.position - transform.position;

            // flip zombie sprite to face forward where they move
            if (moveDirection.x > 0)
                spRenderer.flipX = false; // Face right
            else if (moveDirection.x < 0)
                spRenderer.flipX = true; // Face left
        }
        else
        {
            moveDirection = Vector3.zero;
        }

        moveDirection.Normalize();

        theRB.velocity = moveDirection * moveSpeed;

        // move animation
        if(moveDirection != Vector3.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    // enemy damaged system
    public void DamageEnemy(int damage)
    {
        health -= damage;

        Instantiate(gotHitEffect, transform.position, transform.rotation);

        if(health <= 0)
        {
            moveSpeed = 0;
            anim.SetTrigger("isDead");
        }
    }
    
    // damage to player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealthManager.instance.DamagePlayer();
        }
    }
}
