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
    
    bool isJumping = false;

    private void Start()
    {
        megaManMovement = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        Shoot();
    }
    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(moveHorizontal, 0, 0) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Allows sprinting
        if (Input.GetAxis("Run") > 0 && Input.GetAxis("Horizontal") != 0)
        {
            Vector3 sprint = new Vector3(moveHorizontal, 0, 0) * moveSpeed * sprintMultiplier * Time.deltaTime;
            transform.Translate(sprint);
        }
    }
    private void Jump()
    {
       
        if (Input.GetAxis("Jump") > 0 && isJumping == false)
        {
            Debug.Log("in");
            megaManMovement.AddForce(Vector3.up * jumpForce, ForceMode2D.Force);
            isJumping = true;
        }
    }
    private void Shoot()
    {
        if (Input.GetAxis("Shoot") != 0)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity, transform);
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            if (projectileScript != null)
            {
                projectileScript.SetDamageMultiplier(5f);
            }
            Debug.Log("MegaMan shoots");
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}