using UnityEngine;

public interface EnemyMoveStrategy
{
    void Move(Rigidbody2D rigidBody);
}