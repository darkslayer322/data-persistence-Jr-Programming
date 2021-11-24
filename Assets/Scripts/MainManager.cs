using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainManager : MonoBehaviour
{

    public static MainManager instance;
    public TMP_InputField nameField;
    public PlayerInfo currentPlayer, highScorePlayer;
    public GameObject highscoreText;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void LoadGame()
    {
        currentPlayer = new PlayerInfo(nameField.text); 
        SceneManager.LoadScene("main");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("menu");
    }

    public void UpdateScoreField()
    {
        TextMeshProUGUI scoreField = GameObject.Find("HighScore").GetComponent<TextMeshProUGUI>();
        if (highScorePlayer != null)
        {

            scoreField.text = "Best Score: " + highScorePlayer.name + " " + highScorePlayer.score;
        }
        else
        {
            scoreField.text = "No highscore set";
        }

    }
    
    public void AddScore(int score)
    {
        
        currentPlayer.Update(score);
        highScorePlayer = SaveSystem.LoadData();
        if (highScorePlayer != null)
        {
            if (currentPlayer.score > highScorePlayer.score)
            {
                SaveSystem.SaveData(currentPlayer); // Write current player to savefile
                highScorePlayer = currentPlayer; // Current player has highscore, so why read from file :shrug:
                UpdateScoreField(); // Update scoreboard
            }
        } else 
        {
            SaveSystem.SaveData(currentPlayer); // Assume no high-scores exist and just save the player iguess
        } 
        
    }

    // Start is called before the first frame update
    void Start()
    {
        nameField = GameObject.Find("NameInputField").GetComponent<TMP_InputField>();
        highScorePlayer = SaveSystem.LoadData();
        UpdateScoreField();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}