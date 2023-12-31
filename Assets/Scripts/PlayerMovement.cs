using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] LayerMask blockLayer;
    [SerializeField] LayerMask enemyLayer;
    private AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip enemyStompSound;
    public AudioClip damageSound;
    Rigidbody2D rigidBody2D;
    [SerializeField] HealthDisplay health;
    [SerializeField] GameManager gameManager;

    float speed = 0;
    float jumpPower = 400;
    private bool isBlinking = false;

    public float blinkTime = 1f;    // 点滅の継続時間
    public float blinkInterval = 0.05f; // 点滅の間隔
    private SpriteRenderer spriteRenderer;

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
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
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
            if (audioSource != null && enemyStompSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyCollision(collision);
    }

    void EnemyCollision(Collision2D collision)
    {
        // 敵との衝突を検出
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // 敵の上部に接触したかどうかを判断
            if (Physics2D.Linecast(transform.position - transform.right * 0.3f, transform.position - transform.up * 0.1f, enemyLayer)
                || Physics2D.Linecast(transform.position + transform.right * 0.3f, transform.position - transform.up * 0.1f, enemyLayer))
            {
                Jump();

                // 敵オブジェクトのEnemyControllerコンポーネントを取得
                EnemyMove enemyMove = collision.gameObject.GetComponent<EnemyMove>();

                // EnemyControllerのblowAwayメソッドを呼び出す
                if (enemyMove != null)
                {
                    enemyMove.blowAway();
                    if (audioSource != null && enemyStompSound != null)
                    {
                        audioSource.PlayOneShot(enemyStompSound);
                    }
                }
            }
            else
            {
                StartCoroutine(Blink(blinkTime, blinkInterval));
            }
        }
    }

    IEnumerator Blink(float duration, float interval)
    {
        isBlinking = true;
        float time = 0;
        health.TakeDamage(1);

        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }

        while (time < duration)
        {
            // レンダラーの有効/無効を切り替え
            spriteRenderer.enabled = !spriteRenderer.enabled;

            // 次の点滅まで待機
            yield return new WaitForSeconds(interval);

            time += interval;
        }

        // 点滅終了時にレンダラーを必ず有効にする
        spriteRenderer.enabled = true;
        if (health.getHealthLenght() <= 0)
        {
            gameManager.GameOver();
        }

        yield return new WaitForSeconds(0.3f);

        isBlinking = false;

        // 点滅が終了した後に敵と接触しているかチェック
        CheckEnemyCollisionAfterBlink();
    }

    void CheckEnemyCollisionAfterBlink()
    {
        // チェックするボックスのサイズと中心位置を設定
        Vector2 boxSize = new Vector2(1f, 1f); // 例として1x1のサイズ
        Vector2 boxCenter = transform.position; // プレイヤーの位置を中心とする

        // 指定したボックス内で敵レイヤーのコライダーとの重なりをチェック
        Collider2D[] enemies = Physics2D.OverlapBoxAll(boxCenter, boxSize, 0f, enemyLayer);

        foreach (var enemy in enemies)
        {
            // 敵との接触を検出した場合、再度Blinkを開始する
            StartCoroutine(Blink(blinkTime, blinkInterval));
            break; // 一つ見つけたらループを抜ける
        }
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
