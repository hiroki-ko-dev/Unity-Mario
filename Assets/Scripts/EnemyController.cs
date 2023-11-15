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

    public void blowAway()
    {
        // Collider2Dコンポーネントを取得し無効にする
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
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
