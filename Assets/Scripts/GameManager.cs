using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void Title()
    {
        SceneManager.LoadScene("Title");
    }
}
