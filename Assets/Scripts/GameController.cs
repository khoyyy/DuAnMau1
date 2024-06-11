using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] int score = 0;
    [SerializeField] int live = 3;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI liveText;
    private int Length;

    private void Awake()
    {
        var numGameSessions = FindObjectOfType<GameController>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        liveText.text = live.ToString();
        scoreText.text = score.ToString();
    }

    // Tăng điểm 
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    private void DecreaseLive()
    {
        live--;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        liveText.text = live.ToString();
    }
    private void ResetGame()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
    public void ProcessPlayerDeath()
    {
        if (live > 1)
        {
            DecreaseLive();
        }
        else
        {
            ResetGame();
        }
    }
    public int GetScore()
    {
        return score;
    }
}
