using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BossEnemyScript : MonoBehaviour
{
    public float speed = 2f;
    public Transform a, b, c;
    private SpriteRenderer sr;
    [SerializeField] private int enemylive = 4;
    public Slider sliderfod;
    private Rigidbody2D rb2d;
    private Collider2D c2d;
    public GameManager gamemanager;
    public Slider slimy;

    void Start()
    {
        transform.position = a.position;
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        c2d = GetComponent<Collider2D>();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, b.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, b.position) < 0.1f)
        {
            Transform temp = b;
            b = a;
            a = temp;
            sr.flipX = !sr.flipX;
        }
        if (enemylive == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, c.position, speed * 2 * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gamemanager.livy--;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemylive--;
            slimy.value = enemylive;
            if (enemylive <= 0)
            {
                rb2d.bodyType = RigidbodyType2D.Dynamic;
                c2d.enabled = false;
            }
        }
        if (collision.gameObject.CompareTag("FallLinieTag"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("enemyfood"))
        {
            enemylive = 4;
            StartCoroutine(Fixiki());
            if(enemylive == 4)
            {
                StopCoroutine(Fixiki());
            }
        }
    }
    IEnumerator Fixiki()
    {
        while(enemylive < 4)
        {
            yield return new WaitForSeconds(3);
            sliderfod.value = 4 - enemylive;
            slimy.value = enemylive;
        }
    }
}