using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerBullet : MonoBehaviour
{
    [Header("Component")]
    public Rigidbody2D bulletRB;
    public GameObject impactEffect;

    [Header("BulletValue")]
    public float bulletSpeed = 8f;
    public int damageOfBullet = 50;
    //private float randomness = Random.Range(-10f, 10f); // bullet random rotation

    // Update is called once per frame
    void FixedUpdate()
    {
        // about bullet movement
        bulletRB.velocity = transform.right * bulletSpeed;
    }

    // about bullet hit system
    private void OnTriggerEnter2D(Collider2D other) 
    {
        Instantiate(impactEffect, transform.position, transform.rotation); //*= Quaternion.Euler(0, randomness, 0)
        Destroy(gameObject);

        // give damage only that tag the Enemy
        if(other.tag == "Enemy")
        {
            other.GetComponent<ZombieController>().DamageEnemy(damageOfBullet);
        }
    }

    // if bullet goes of of camera sight then destory gameobject
    private void OnBecameInvisible() 
    {
        Destroy(gameObject);
    }
}