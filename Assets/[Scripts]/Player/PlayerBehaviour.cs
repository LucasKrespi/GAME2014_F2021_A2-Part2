using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerBehaviour : MonoBehaviour
{
    [Header("Touch Input")]
    public Joystick joystick;
    [Range(0.1f, 0.9f)]
    public float sensitivity;

    [Header("Movement")] 
    public float horizontalForce;
    public float verticalForce;
    public bool isGrounded;
    public Transform groundOrigin;
    public float groundRadius;
    public LayerMask groundLayerMask;
    [Range(0.1f, 0.9f)]
    public float airControlFactor;

    private Rigidbody2D rigidbody;
    private Animator animatorController;

    [Header("HUD")]
    [SerializeField]
    private List<Image> livesList;
    [SerializeField]
    private TextMeshProUGUI scoreDisplay;
    private int lives;
    private int score;

    [Header("Shoot")]
    public GameObject bullet;
    public Transform bulletSpawpoint;


    private bool isImune = false;

    // Start is called before the first frame update
    void Start()
    {
        lives = 5;
        score = 0;
        rigidbody = GetComponent<Rigidbody2D>();
        animatorController = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        CheckIfGrounded();
        updateDisplay(lives);
        checkGameOver();
    }

    private void Move()
    {
        float x = (Input.GetAxisRaw("Horizontal") + joystick.Horizontal) * sensitivity;  

        if (isGrounded)
        {
            // Keyboard Input
            float y = (Input.GetAxisRaw("Vertical") + joystick.Vertical) * sensitivity;
            float jump = Input.GetAxisRaw("Jump") + ((UIController.jumpButtonDown) ? 1.0f : 0.0f);

            // Check for Flip

            if (x != 0)
            {
                x = FlipAnimation(x);
                animatorController.SetInteger("AnimationChangeState", (int)PlayerAnimationState.RUN);
            }
            else
            {
                animatorController.SetInteger("AnimationChangeState", (int)PlayerAnimationState.IDLE);
            }
            
            // Touch Input
            Vector2 worldTouch = new Vector2();
            foreach (var touch in Input.touches)
            {
                worldTouch = Camera.main.ScreenToWorldPoint(touch.position);
            }

            float horizontalMoveForce = x * horizontalForce;
            float jumpMoveForce = jump * verticalForce; 

            float mass = rigidbody.mass * rigidbody.gravityScale;

            if (jump > 0)
            {
                SoundManager.soundManagerInstace.PlaySound("Jump");
            }

            rigidbody.AddForce(new Vector2(horizontalMoveForce, jumpMoveForce) * mass);
            rigidbody.velocity *= 0.99f; // scaling / stopping hack
        }
        else
        {
            animatorController.SetInteger("AnimationChangeState", (int)PlayerAnimationState.JUMP);

            if (x != 0)
            {
                x = FlipAnimation(x);

                float horizontalMoveForce = x * horizontalForce * airControlFactor;
                float mass = rigidbody.mass * rigidbody.gravityScale;

                rigidbody.AddForce(new Vector2(horizontalMoveForce, 0.0f) * mass);
            }
        }

    }

    private void CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(groundOrigin.position, groundRadius, Vector2.down, groundRadius, groundLayerMask);

        isGrounded = (hit) ? true : false;
    }

    public void Shoot()
    {
        GameObject go = Instantiate(bullet, bulletSpawpoint.position, Quaternion.identity);

        if(transform.localScale.x > 0)
        {
            go.GetComponent<Rigidbody2D>().velocity = new Vector2(10.0f, 0.0f) * 1;
        }
        else
        {
            go.GetComponent<Rigidbody2D>().velocity = new Vector2(10.0f, 0.0f) * -1;
        }
       
    }

    private float FlipAnimation(float x)
    {
        // depending on direction scale across the x-axis either 1 or -1
        x = (x > 0) ? 1 : -1;

        transform.localScale = new Vector3(x, 1.0f);
        return x;
    }


    private void updateDisplay(int currentLives)
    {
        if (currentLives < 5)
            livesList[currentLives].enabled = false;

        scoreDisplay.text = "Score: " + score.ToString();
    }

    private void checkGameOver()
    {
        if(lives <= 0)
        {
            endGame();
        }
    }

    private void endGame()
    {
        PlayerPrefs.SetInt("Score", score);
        SceneManager.LoadScene(3);
    }
    public void addScore(int points)
    {
        score += points;
    }
    // UTILITIES

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundOrigin.position, groundRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(collision.transform);
        }
        if (collision.gameObject.CompareTag("EnemyRange") || collision.gameObject.CompareTag("EnemyMelee"))
        {
            StartCoroutine(GetHit());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(null);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            StartCoroutine(GetHit());
        }
        if (collision.CompareTag("Collectable"))
        {
            score += 100;
        }
        if (collision.CompareTag("Final"))
        {
            endGame();
        }
    }

    IEnumerator GetHit()
    {
        if (!isImune)
        {
            isImune = true;
            SoundManager.soundManagerInstace.PlaySound("Pain");
            animatorController.SetBool("hit", true);

            lives--;

            yield return new WaitForSeconds(animatorController.GetCurrentAnimatorClipInfo(0).Length);

            animatorController.SetBool("hit", false);
            isImune = false;
        }
    }
}
