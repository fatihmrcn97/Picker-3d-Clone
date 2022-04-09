using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSystem : MonoBehaviour
{
    public static LevelSystem instance;

    [SerializeField] private GameObject startMenu, gameMenu, gameOverMenu, finishMenu;

    [SerializeField] private TextMeshProUGUI scoreTxt, currentLevelTxt, NextLvlTxt;

    [SerializeField] private List<Image> images;

    [SerializeField] private List<GameObject> Levels;

    [SerializeField] private GameObject player;

    public bool gameActive = false;

    private int currentLevel;

    int Score;
    float speed;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("currentLevel");
        Levels[currentLevel].SetActive(true);

        currentLevelTxt.text = (currentLevel + 1).ToString();
        NextLvlTxt.text = (currentLevel + 2).ToString();
    }
    
    private void LevelTextLoad()
    { 
        currentLevelTxt.text = (currentLevel + 1).ToString();
        NextLvlTxt.text = (currentLevel + 2).ToString();
    }

    private void StageImagesBackToWhite()
    {
        for (int i = 0; i < images.Count; i++)
        {
            images[i].color = Color.white;
        }
    }

    public void StartLevel()
    {
       // if(currentLevel>1)
        player.transform.position = new Vector3(currentLevel * 168, 0.33f, 0);

        gameActive = true;
        PlayerController.instance.SetSpeed(4);
        startMenu.SetActive(false);
        gameMenu.SetActive(true);
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
        PlayerPrefs.SetInt("currentLevel", currentLevel + 1);
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
        currentLevel = PlayerPrefs.GetInt("currentLevel");
        finishMenu.SetActive(false);
        yield return new WaitForSeconds(10f);
        // Bir sonraki levele git
        GoNextLevel();
        LevelTextLoad();
        StageImagesBackToWhite();
    }

    private void GoNextLevel()
    { 
        if (Levels.Count > currentLevel)
        {
        Levels[currentLevel-1].SetActive(false);
        Levels[currentLevel].SetActive(true);
        }
        else
        {
            Debug.Log("More level create");
        }
     
    }


}
