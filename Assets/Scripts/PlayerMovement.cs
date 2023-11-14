using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] LayerMask blockLayer;
    Rigidbody2D rigidBody2D;

    float speed = 0;
    float jumpPower = 400;

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
        rigidBody2D = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector2(4, 4);
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
        if (IsGround() && Input.GetKeyDown("space"))
        {
            Jump();
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
                speed = 5;
                transform.localScale = new Vector2(4, 4);
                break;
            case MOVE_DIRECTION.LEFT:
                speed = -5;
                transform.localScale = new Vector2(-4, 4);
                break;
        }
        rigidBody2D.velocity = new Vector2(speed, rigidBody2D.velocity.y);
    }

    void Jump()
    {
        rigidBody2D.AddForce(Vector2.up * jumpPower);
    }

    bool IsGround()
    {
        return Physics2D.Linecast(transform.position - transform.right * 0.3f, transform.position - transform.up * 0.1f, blockLayer)
            || Physics2D.Linecast(transform.position + transform.right * 0.3f, transform.position - transform.up * 0.1f, blockLayer);
    }
}
