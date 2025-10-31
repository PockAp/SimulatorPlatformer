using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BossEnemyScript : MonoBehaviour
{
    public float speed = 2f;
    public Transform a, b, c;
    private bool iseating;
    public GameObject keyinboss;
    private SpriteRenderer sr;
    [SerializeField] private int enemylive = 4;
    public Slider sliderfod;
    private Rigidbody2D rb2d;
    private Collider2D c2d;
    public GameManager gamemanager;
    public Slider slimy;

    void Start()
    {
        keyinboss.gameObject.SetActive(false);
        transform.position = a.position;
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        c2d = GetComponent<Collider2D>();
    }

    void Update()
    {
        Patrol();
        if (enemylive == 1 || iseating)
        {
            EatGoing();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gamemanager.livy--;
            gamemanager.UpdateLifePanel();
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
                keyinboss.gameObject.SetActive(true);
                c2d.enabled = false;
            }
        }
        if (collision.gameObject.CompareTag("FallLinieTag"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("enemyfood"))
        {
            sliderfod.value = 4;
            StartCoroutine(Fixiki());
        }
    }

    void EatGoing()
    {
        transform.position = Vector3.MoveTowards(transform.position, c.position, speed * 2 * Time.deltaTime);
    }

    void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, b.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, b.position) < 0.1f)
        {
            Transform temp = b;
            b = a;
            a = temp;
            sr.flipX = !sr.flipX;
        }
    }
    IEnumerator Fixiki()
    {
        iseating = true;
        while (enemylive < 4)
        {
            enemylive++;
            slimy.value = enemylive;
            sliderfod.value = 4 - enemylive;
            yield return new WaitForSeconds(2);
        }
        iseating = false;
    }
}