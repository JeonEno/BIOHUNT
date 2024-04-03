using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [Header("Movement")]
    public float moveSpeed;
    private Vector2 moveInput;
    
    [Header("Component")]
    public Rigidbody2D rb2D;
    public Transform gunPivot;
    private Camera theCam;

    [Header("Shooting")]
    public GameObject bulletToFire;
    public Transform firePoint;
    public float timeGapShots;
    private float shotCounter;
    public int currentClip; 
    public int maxClip = 20;
    public float reloadTime = 3f;

    [Header("Animation")]
    public Animator anim;

    private void Awake() 
    {
        instance = this;
    }

    void Start()
    {
        theCam = Camera.main;
        currentClip = maxClip;
    }

    void FixedUpdate() 
    {
        Movement();

        PlayerShooting();        
    }
    void Update()
    {   
        //reload system
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(Input.GetMouseButton(0)||Input.GetMouseButtonDown(0))
            {
                Debug.Log("player is now shooting");
            }
            else
            {
                StartCoroutine(Reload());
                return;
            }
        }
    }
    public void Movement()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        rb2D.velocity = moveInput * moveSpeed;

        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = theCam.WorldToScreenPoint(transform.localPosition);
        
        //flip player X local scale about mouse position
        if(mousePos.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            gunPivot.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            gunPivot.localScale = new Vector3(1f, 1f, 1f);
        }

        //rotate gunPivot
        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gunPivot.rotation = Quaternion.Euler(0, 0, angle);

        if(moveInput != Vector2.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    public void PlayerShooting()
    {
        // fire Bullet
        if(currentClip > 0 && Input.GetMouseButtonDown(0))
        {
            shotCounter -= Time.deltaTime;

            if(shotCounter <= 0)
            {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                shotCounter = timeGapShots;
                currentClip -= 1;
            }
        }

        //Time gap between shots fire
        if(currentClip > 0 && Input.GetMouseButton(0))
        {
            if(moveInput != Vector2.zero)
            {
                anim.SetBool("isMoving", false);
                anim.SetBool("isShooting", true);
            }
            else
            {
                anim.SetBool("isShooting", false);
            }

            float moveSpeed = 2f;
            
            rb2D.velocity = moveInput * moveSpeed;

            shotCounter -= Time.deltaTime;

            if(shotCounter <= 0)
            {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                shotCounter = timeGapShots;
                currentClip -= 1;
            }
        }
        else
        {
            // anim.SetBool("isMoving", true);
            anim.SetBool("isShooting", false);
        }
    }
    
    IEnumerator Reload()
    {
        yield return new WaitForSeconds (reloadTime);
        currentClip = maxClip;
    }
}
