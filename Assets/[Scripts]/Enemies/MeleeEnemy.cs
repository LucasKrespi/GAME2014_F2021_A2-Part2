using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Movement")]
    public float runForce;
    public Transform lookAhead;
    public Transform lookForward;
    public LayerMask groundLayerMask;
    public LayerMask wallLayerMask;
    public bool isGroundAhead;


    [Header("Hit Response")]
    public float bounceForce;


    private Animator animatorController;
    private Rigidbody2D rigidbody;

    private bool isfliping = false;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animatorController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        LookAhead();
        LookForward();

        MoveEnemy();

   
    }

    private void FixedUpdate()
    {
        if (rigidbody.velocity.x != 0)
        {
            animatorController.SetBool("run", true);
        }
        else
        {
            animatorController.SetBool("run", false);
        }
    }

    private void LookAhead()
    {
        var hit = Physics2D.Linecast(transform.position, lookAhead.position, groundLayerMask);
        isGroundAhead = hit;
    }

    private void LookForward()
    {
        if (!isfliping)
        {
            var hit = Physics2D.Linecast(transform.position, lookForward.position, wallLayerMask);
            if (hit)
            {
                StartCoroutine(WaitToFLip());
            }
        }
    }
    private void MoveEnemy()
    {
        if (!isfliping)
        {
            if (isGroundAhead)
            {
                rigidbody.AddForce(Vector2.left * runForce * transform.localScale.x);
                rigidbody.velocity *= 0.90f;
            }
            else
            {

                StartCoroutine(WaitToFLip());

            }
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        isfliping = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(collision.transform);
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
       if(collision.tag == "Player")
       {
            StartCoroutine(hitAndDie());
            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, bounceForce));
       }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position, lookAhead.position);
        Gizmos.DrawLine(transform.position, lookForward.position);
    }

    IEnumerator WaitToFLip()
    {
        isfliping = true;
        yield return new WaitForSeconds(2);
        Flip();
    }

    IEnumerator hitAndDie()
    {
        animatorController.SetBool("hit", true);
        yield return new WaitForSeconds(0.18f);

        Destroy(this.gameObject);
    }
}
