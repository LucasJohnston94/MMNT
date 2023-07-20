using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    int speed = 20;
    public Rigidbody2D rb;

    // Start is called before the first frame update

    IEnumerator destroyTimer()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
    
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);
        Destroy(gameObject);
    }

    private void Update()
    {
        StartCoroutine(destroyTimer());
    }

}
