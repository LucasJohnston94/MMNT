using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] 
    float damageMultiplier = 1f;
    [SerializeField]
    float speed = 10f;
    private Rigidbody2D rb;

    //Called before the Start Method
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDamageMultiplier(float multiplier)
    {
        damageMultiplier = multiplier;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle collision and damage calculations
        GameObject otherGameObject = collision.gameObject;

        //Apply damage based on the charge level and damage multiplier
        float chargeLevel = Mathf.Clamp(transform.localScale.x, 0f, 5f);
        float damage = chargeLevel * damageMultiplier;
    }
}
