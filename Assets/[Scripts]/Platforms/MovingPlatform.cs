using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MovingPlatformDirection
{
    HORIZONTAL = 0,
    VERTICAL,
    DIAGONAL_DOWN,
    DIAGONAL_UP

}
public class MovingPlatform : MonoBehaviour
{
    [Header("Movement")]
    public MovingPlatformDirection direction;

    [Range(0.1f, 10.0f)]
    public float speed;

    [Range(1, 20)]
    public float distance;
    public bool isLooping;

    private Vector2 startingPosition;
    private bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatafor();
        if (isLooping)
        {
            isMoving = true;
        }
    }

    private void MovePlatafor()
    {
        float pingPong = (isMoving) ? Mathf.PingPong(Time.time * speed, distance) : distance;

        if (isLooping && pingPong >= distance - 0.05f)
        {
            isMoving = false;
        }
        switch (direction)
        {
            case MovingPlatformDirection.HORIZONTAL:
                transform.position = new Vector2(startingPosition.x + pingPong, startingPosition.y);
                break;
            case MovingPlatformDirection.VERTICAL:
                transform.position = new Vector2(startingPosition.x, startingPosition.y + pingPong);
                break;
            case MovingPlatformDirection.DIAGONAL_DOWN:
                transform.position = new Vector2(startingPosition.x + pingPong, startingPosition.y - pingPong);
                break;
            case MovingPlatformDirection.DIAGONAL_UP:
                transform.position = new Vector2(startingPosition.x + pingPong, startingPosition.y + pingPong);
                break;

        }

    }
}
