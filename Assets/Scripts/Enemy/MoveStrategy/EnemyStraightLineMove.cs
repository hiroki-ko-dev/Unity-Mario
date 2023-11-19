using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStraightLineMove : MonoBehaviour, EnemyMoveStrategy
{
    public void Move(Rigidbody2D rigidBody)
    {
        rigidBody.velocity = new Vector2(-3, rigidBody.velocity.y);
    }
}
