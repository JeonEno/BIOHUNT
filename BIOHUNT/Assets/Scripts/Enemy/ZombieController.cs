using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    public static ZombieController instance;

    [Header("Component")]
    public Rigidbody2D theRB;
    public NavMeshAgent agent;

    [Header("Movement Value")]
    //public float moveSpeed;
    //private Vector3 moveDirection;
    public float rangeToChasePlayer;
    public SpriteRenderer spRenderer;
    public Transform target;

    [Header("Animation & Effect")]
    public Animator anim;
    public GameObject gotHitEffect;

    [Header("Health")]
    public int health = 150;


    // Start is called before the first frame update
    private void Awake() 
    {
        instance = this;
    }
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        var agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(PlayerHealthManager.instance.currentHealth >= 0 && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer)
        {
            //moveDirection = PlayerController.instance.transform.position - transform.position;  
            agent.SetDestination(target.position);
            
            if(health > 0)
            {
                flipSprite();
            }
        }
        else
        {
            agent.velocity = Vector3.zero;
        }

        // moveDirection.Normalize();
        // theRB.velocity = moveDirection * moveSpeed;

        // move animation
        if(agent.velocity != Vector3.zero)
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

        if(health < 0)
        {
            agent.speed = 0;
            anim.SetTrigger("isZomDead");

            // disable collider after zom died
            gameObject.GetComponent<Collider2D>().enabled = false;
            StartCoroutine(WaitForDead());
        }
    }

    IEnumerator WaitForDead() 
    {
        yield return new WaitForSeconds(1.1f);
        Destroy(gameObject);
    }
    
    // damage to player
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DamagePlayer();
        }
    }

    private void DamagePlayer()
    {
        PlayerHealthManager.instance.DamagePlayer();
    }

    void flipSprite() //flip zombies sprite head to player location
    {
        if (target.transform.position.x > transform.position.x)
            spRenderer.flipX = false; // Face right
        else
            spRenderer.flipX = true; // Face left
    }
}
