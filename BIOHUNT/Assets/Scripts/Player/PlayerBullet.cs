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
    public float distance = 1f;
    public LayerMask hitLayer;
    //private float randomness = Random.Range(-10f, 10f); // bullet random rotation

    // Update is called once per frame
    void FixedUpdate(){
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, distance, hitLayer);
        if(ray.collider != null)
        {
            if(ray.collider.tag == "Enemy"){
                Debug.Log("Enemy Hit");
                ray.collider.GetComponent<ZombieController>().DamageEnemy(damageOfBullet);
                Destroy(gameObject);
            }
            else {
                Debug.Log("Hit Something else");
                //Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
        // about bullet movement
        bulletRB.velocity = transform.right * bulletSpeed;
    }

    private void OnBecameInvisible(){
        Destroy(gameObject);
    }
}