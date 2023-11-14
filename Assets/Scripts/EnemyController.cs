using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] LayerMask blockLayer;
    Rigidbody2D rigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector2(-4, 4);
        rigidBody2D.velocity = new Vector2(-3, rigidBody2D.velocity.y);
    }
}
