using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.Mathematics;
using Unity.VisualScripting;
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
    public float shakeIntensity = 4f;

    [Header("Animation")]
    public Animator anim;
    [SerializeField] private Animator _muzzleFlashAnim; 

    private void Awake() 
    {
        instance = this;
    }

    void Start()
    {
        theCam = Camera.main;
        currentClip = maxClip;

        UIController.instance.ammoText.text = currentClip.ToString() + "/" + maxClip.ToString();
    }

    void FixedUpdate() 
    {
        Movement();

        PlayerShooting();    
    }
    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.R))
        {
            ReloadClip();    
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

        // Player Run
        if(Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 7;
            anim.SetBool("isRunning", true);
        }
        else
        {
            moveSpeed = 5;
            anim.SetBool("isRunning", false);
        }
        // Walk or Idle anim
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
        if(Input.GetMouseButton(0) || Input.GetMouseButton(0))
        {
            if(currentClip > 0)
            {
                shotCounter -= Time.deltaTime;

                if(shotCounter <= 0)
                {
                    Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                    shotCounter = timeGapShots;
                    currentClip -= 1;

                    _muzzleFlashAnim.SetTrigger("isShoot");
                    CinemachineShake.Instance.ShakeCamera(shakeIntensity, 0.06f);
                }
            }
        }

        if(currentClip == 0)
        {
            UIController.instance.reloadMessage.SetActive(true);
        }
        else
        {
            UIController.instance.reloadMessage.SetActive(false);
        }

        UIController.instance.ammoText.text = currentClip.ToString() + "/" + maxClip.ToString();
    }

    void ReloadClip()
    {
        if(Input.GetMouseButton(0)||Input.GetMouseButtonDown(0))
        {
            Debug.Log("player is now shooting");
        }
        else
        {
            StartCoroutine(Reload());
            Debug.Log("Reloading...");
            return;
        }
    }
    
    IEnumerator Reload()
    {
        yield return new WaitForSeconds (reloadTime);
        currentClip = maxClip;
    }
}