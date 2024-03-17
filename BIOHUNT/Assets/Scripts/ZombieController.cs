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

    [Header("Animation & Effect")]
    public Animator anim;
    public GameObject gotHitEffect;

    [Header("Health")]
    public int health = 150;

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
        }
        else
        {
            moveDirection = Vector3.zero;
        }

        moveDirection.Normalize();

        theRB.velocity = moveDirection * moveSpeed;


        if(moveDirection != Vector3.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

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
}
