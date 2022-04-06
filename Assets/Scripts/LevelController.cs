using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;
    public bool gameActive = false;

    public GameObject startMenu, gameMenu, gameOverMenu, finishMenu;

    public TextMeshProUGUI scoreTxt, currentLevelTxt, NextLvlTxt;

    int Score;

    private int currentLevel;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentLevel = PlayerPrefs.GetInt("currentLevel");
        if (SceneManager.GetActiveScene().name != "Level " + currentLevel)
            SceneManager.LoadScene("Level " + currentLevel);
        else
        {
            currentLevelTxt.text = (currentLevel + 1).ToString();
            NextLvlTxt.text = (currentLevel + 2).ToString();
        }
    }

    public void StartLevel()
    {
        PlayerController.instance.SetSpeed(4);
        startMenu.SetActive(false);
        gameMenu.SetActive(true);
        gameActive = true;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadNextLevel()
    {
        gameActive = true;
        StartCoroutine(loadNextLevel());

    }
    public void GameOver()
    {
        gameMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        gameActive = false;
    }
    public void FinishGame()
    {
        PlayerPrefs.SetInt("currentLevel", currentLevel+1);
        scoreTxt.text = Score.ToString();
        //gameMenu.SetActive(false);
        finishMenu.SetActive(true);
       

    }

    public void ChangeScore(int increment)
    {
        Score += increment;
        scoreTxt.text = Score.ToString();
    }

    IEnumerator loadNextLevel()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Level "+(currentLevel + 1));
    }
}
