using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 2f;
    public float jumpforce = 4f;
    public bool haskey = false;
    public bool isclimbing = false;
    public GameObject groundcheck;
    public GameObject keypicture;
    private Animator animator;
    private float horizontal;
    public GameManager manager;
    private SpriteRenderer sr;
    [SerializeField] private bool lemon; // лемон = есть игрок на земле или нет

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        lemon = true;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && lemon)
        {
            animator.SetTrigger("jump");
            rb.AddForce(transform.up * jumpforce, ForceMode2D.Impulse);
            lemon = false;
        }
        CheckGround();
        Flip();
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        if (rb != null)
        {

            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        if(horizontal != 0f)
        {
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundcheck.transform.position, 0.2f);
        lemon = colliders.Length > 1;
    }

    public void Flip()
    {
        if(horizontal > 0)
        {
            sr.flipX = false;
        }
        if(horizontal < 0)
        {
            sr.flipX = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("KeyTag"))
        {
            haskey = true;
            keypicture.gameObject.SetActive(true);
            Destroy(other.gameObject);
            Debug.Log("key");
        }
        if(other.CompareTag("BoxKeyTag") && haskey == true)
        {
            manager.AnimationFinish();
        }
        if (other.CompareTag("FallLinieTag"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (other.CompareTag("FruitHealth"))
        {
            if(manager.livy < manager.maxhaertslivy)
            {
                manager.livy++;
            }
            Destroy(other.gameObject);
            manager.UpdateLifePanel();
        }
        if (other.CompareTag("EnemyCollect"))
        {
            manager.livy--;
            Destroy(other.gameObject);
            manager.UpdateLifePanel();
        }
        if (other.CompareTag("Stairs"))
        {
            rb.gravityScale = 0;
            isclimbing = true;
        }
        if (other.CompareTag("TeleportTag"))
        {
            TeleportScript teleportscript = other.gameObject.GetComponent<TeleportScript>();
            if (!teleportscript.isteleporting)
            {
                teleportscript.Unlock();
                teleportscript.Teleporting(gameObject);
            }
        }
        if (other.CompareTag("SnowmanCollection"))
        {
            manager.snowman++;
            manager.SnowmanTextUpdate();
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Stairs") && isclimbing)
        {
            float vertical = Input.GetAxis("Vertical");
            transform.Translate(Vector2.up * speed * vertical * Time.deltaTime);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Stairs"))
        {
            rb.gravityScale = 1;
            isclimbing = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SpikeTag"))
        {
            manager.livy--;
            manager.UpdateLifePanel();
        }
    }
}
