using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private EnemyMoveStrategy enemyMoveStrategy;

    Rigidbody2D rigidBody2D;
    Collider2D enemyCollider;

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();
        enemyMoveStrategy = GetComponent<EnemyMoveStrategy>();
    }

    private void Update()
    {
        transform.localScale = new Vector2(-4, 4);
        Move();
    }

    private void Move()
    {
        if (enemyMoveStrategy != null)
        {
            enemyMoveStrategy.Move(rigidBody2D);
        }
    }

    public void blowAway()
    {
        // Collider2Dコンポーネントを取得し無効にする

        if (enemyCollider != null)
        {
            enemyCollider.enabled = false;
        }

        // 放物線運動を実現するための力を設定
        float upwardForce = 200f; // 上方向への力
        float sidewaysForce = 50f; // 横方向への力（敵の向きに依存する）

        // オブジェクトの向きに基づいて左または右に力を加える
        Vector2 forceDirection = transform.right * sidewaysForce;

        transform.eulerAngles = new Vector3(0, 0, 180);

        // 敵を上方向にも打ち上げる
        forceDirection += Vector2.up * upwardForce;

        // 力を加える
        rigidBody2D.AddForce(forceDirection);

    }
}
