using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rigidbody2D;

    float speed;

    public enum MOVE_DIRECTION
    {
        STOP,
        LEFT,
        RIGHT,

    }

    MOVE_DIRECTION moveDirection = MOVE_DIRECTION.STOP;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        if (x==0)
        {
            moveDirection = MOVE_DIRECTION.STOP;
        }
        else if (x > 0)
        {
            moveDirection = MOVE_DIRECTION.RIGHT;
        }
        else if (x < 0)
        {
            moveDirection = MOVE_DIRECTION.LEFT;
        }
    }

    private void FixedUpdate()
    {
        switch (moveDirection)
        {
            case MOVE_DIRECTION.STOP:
                speed = 0;
                break;
            case MOVE_DIRECTION.RIGHT:
                speed = 3;
                break;
            case MOVE_DIRECTION.LEFT:
                speed = -3;
                break;
        }
        rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
    }
}
