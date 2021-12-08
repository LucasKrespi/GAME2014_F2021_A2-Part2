using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanPlataform : MonoBehaviour
{
    private bool isColliding = false;
    private Rigidbody2D Object;

    [SerializeField]
    private float fanForce;

    // Update is called once per frame
    void Update()
    {
        
        if(Object && isColliding)
        {
            Object.AddForce(new Vector2(0.0f, fanForce));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isColliding = true;
        Object = collision.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isColliding = false;
    }
 
}
