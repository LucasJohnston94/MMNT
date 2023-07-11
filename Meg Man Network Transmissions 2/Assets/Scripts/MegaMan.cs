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
    [SerializeField]
    float sprintMultiplier = 2f;
    [SerializeField]
    GameObject projectilePrefab;

    int projectileCount = 0;
    float projectileDelay;
    
    public bool isJumping = false;

    float moveHorizontal;

    private void Start()
    {
        megaManMovement = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Move();        
        //Shoot();
    }

    private void Move()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal * moveSpeed, megaManMovement.velocity.y);
        megaManMovement.velocity = movement;
     
    }

    private void Jump()
    {  
        Debug.Log("in");
        megaManMovement.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isJumping = true;
        
    }

    private void Shoot()
    {
        if (Input.GetAxis("Shoot") != 0 && projectileDelay <= 1)
        {
            while (projectileCount < 3)
            {
                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity, transform);
                Projectile projectileScript = projectile.GetComponent<Projectile>();
                projectileCount++;
                projectileDelay += Time.deltaTime;
                if (projectileScript != null)
                {
                    projectileScript.SetDamageMultiplier(5f);
                }
                Debug.Log("MegaMan shoots");
            }
            projectileDelay = 0;
       }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
    }
}