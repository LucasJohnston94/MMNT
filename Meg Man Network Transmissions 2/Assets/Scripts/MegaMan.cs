using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Provides Main Character Functionality
/// </summary>
public class MegaMan : MonoBehaviour
{
    Rigidbody2D megaManMovement;

    [SerializeField]
    float moveSpeed = 5f;
    [SerializeField]
    float jumpForce = 10f;
    //[SerializeField]
    //float sprintMultiplier = 2f;

    public Transform firePoint;
    [SerializeField]
    GameObject bulletPrefab;

    
    public bool isJumping = false;

    float moveHorizontal;
    bool m_FacingRight;

    private Animator anim;

    private void Start()
    {
        megaManMovement = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
        {
            Jump();
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        
        if (moveHorizontal < 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (moveHorizontal > 0 && m_FacingRight)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {

        moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal * moveSpeed, megaManMovement.velocity.y);
        megaManMovement.velocity = movement;
        anim.SetFloat("running", Mathf.Abs(movement.x));
        
     
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;

        transform.Rotate(0, 180, 0);
    }

    private void Jump()
    {  
        // Jumping logic
        megaManMovement.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isJumping = true;

        // Animation
        anim.SetBool("jumping", true);
        
    }

    private void Shoot()
    {
        // Shooting logic
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }


    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isJumping = false;
            anim.SetBool("jumping", false);
        }
    }
}