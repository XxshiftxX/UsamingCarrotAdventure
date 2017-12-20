using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float status_MoveSpeed = 1f;
    public float status_Jump = 1f;

    public float playerHP = 100;

    public bool isGameOver = false;

    public SpriteRenderer blankHpBar;
    public SpriteRenderer greenHpBar;
    public SpriteRenderer redHpBar;
    public Text ScoreText; 

    public int score;

    Rigidbody2D rigidbody;
    SpriteRenderer spriteRenderer;
    Animator animator;

    //Collider2D lastTriggeredCollieder;

    Vector3 movement;
    bool isJumping = false;
    bool isCooldown;

    // Use this for initialization
    private void Start () {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        animator = gameObject.GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	private void Update () {
		if(Input.GetButtonDown("Jump") && !animator.GetBool("isJumping"))
        {
            isJumping = true;
            animator.SetTrigger("Jump");
            animator.SetBool("isJumping", true);
        }

        if(playerHP > 100)
        {
            playerHP = 100;
        }

        if(playerHP < 0)
        {
            playerHP = 1000;
            GameOver();
        }

        greenHpBar.size = new Vector2(playerHP / 100 * 3 + 1, 0.7f);
        redHpBar.size = greenHpBar.size;
        if (playerHP < 70)
        {
            greenHpBar.color = new Color(greenHpBar.color.r, greenHpBar.color.g, greenHpBar.color.b, (playerHP-20) / 50);
        }
	}

    private void FixedUpdate()
    {
        if (!isGameOver)
        {
            Move();
            Jump();
        }
    }

    private void Move()
    {
        Vector3 velocity = Vector4.zero;

        
        if(Input.GetAxisRaw ("Horizontal") < 0)
        {
            velocity = Vector2.left;
            spriteRenderer.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            velocity = Vector2.right;
            spriteRenderer.flipX = true;
        }

        transform.position += velocity * status_MoveSpeed * Time.deltaTime;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "케토톱")
        {
            Destroy(collision.gameObject);
            playerHP += 10;
        }
        else if (collision.gameObject.name == "RedZone")
        {
            GameOver();
        }
        else if (collision.gameObject.name.StartsWith("block"))
        {
            if(score < int.Parse(collision.gameObject.name.Substring(5)))
            {
                score = int.Parse(collision.gameObject.name.Substring(5));
                ScoreText.text = score.ToString();
            }
            if (animator.GetBool("isJumping"))
            {
                playerHP -= 2;
                animator.SetBool("isJumping", false);
            }
        }
    }

    private void Jump()
    {
        if (!isJumping)
            return;

        rigidbody.velocity = Vector2.zero;

        Vector2 velocity = new Vector2(0, status_Jump);
        rigidbody.AddForce(velocity, ForceMode2D.Impulse);

        isJumping = false;
    }

    public void GameOver()
    {
        isGameOver = true;

        Collider2D collider = gameObject.GetComponent<Collider2D>();
        animator.SetTrigger("GameOver");

        blankHpBar.enabled = false;
        greenHpBar.enabled = false;
        redHpBar.enabled = false;
        collider.isTrigger = true;
        rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
