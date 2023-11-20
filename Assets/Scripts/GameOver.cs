using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    private AudioSource audioSource;
    [SerializeField] AudioClip gameOverSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(gameOverSound);
        StartCoroutine(WaitAndCallTitle());
    }

    IEnumerator WaitAndCallTitle()
    {
        // 3秒待機
        yield return new WaitForSeconds(4f);

        // Titleメソッドを呼び出す
        gameManager.Title();
    }
}
