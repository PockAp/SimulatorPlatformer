using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    public float speed = 1f;
    public GameManager manager;
    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    public float jumpforce = 4f;
    private Collider2D c2d;
    public Transform a;
    public Transform b;
    void Start()
    {
        transform.position = a.position;
        sr = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        c2d = GetComponent<Collider2D>();
    }

   
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, b.position, speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, b.position)< 0.1f)
        {
            Transform temp = b;
            b = a;
            a = temp;
            sr.flipX = !sr.flipX;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            manager.livy--;
            GameObject player = collision.gameObject;
            if(player != null)
            {
                player.GetComponent<Rigidbody2D>().AddForce(transform.up * jumpforce, ForceMode2D.Impulse);
            }
            manager.UpdateLifePanel();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rb2d.bodyType = RigidbodyType2D.Dynamic;
            c2d.enabled = false;
        }
        if(other.gameObject.CompareTag("FallLinieTag"))
        {
            Destroy(gameObject);
        }
    }
}
