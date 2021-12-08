using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{

    [Header("Hit Response")]
    public float bounceForce;

    public Collider2D weakSpot;
    public Collider2D playerCollider;

    public GameObject projectilePrefab;
    public Transform bulletSpawpoint;
    public Animator animatorController;
    private bool isShooting = false;
    private bool canShooting = false;
    private bool isDead= false;
    // Start is called before the first frame update
    private void Update()
    {
        if (!isShooting && canShooting)
        {
            StartCoroutine(Shoot());
        }

        if(Physics2D.IsTouching(weakSpot, playerCollider) && !isDead)
        {
            StartCoroutine(hitAndDie());
            playerCollider.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(0.0f, bounceForce));
            playerCollider.GetComponentInParent<PlayerBehaviour>().addScore(200);
            isDead = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canShooting = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canShooting = false;
        }

    }
    public void InstancianteProjectile()
    {
        GameObject go = Instantiate(projectilePrefab, bulletSpawpoint.position, Quaternion.identity);
        go.GetComponent<Rigidbody2D>().velocity = new Vector2(-10.0f, 0.0f);
    }

    IEnumerator Shoot()
    {
        isShooting = true;
        animatorController.SetBool("shoot", true);

        yield return new WaitForSeconds(Random.Range(0.3f, 2.0f));

        animatorController.SetBool("shoot", false);
        isShooting = false;
    }

    IEnumerator hitAndDie()
    {
        animatorController.SetBool("hit", true);
        yield return new WaitForSeconds(0.25f);

        Destroy(this.gameObject);
    }
}
