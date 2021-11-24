using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
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
        currentPlayer.name = nameField.text;
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

    void SaveFile()
    {
        string path = Application.persistentDataPath + "/highscore.json";
        string json = JsonUtility.ToJson(currentPlayer);
        File.WriteAllText(path, json);
    }
    void LoadFile()
    {
        string path = Application.persistentDataPath + "/highscore.json";
        Debug.Log(path);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Debug.Log("File loaded");
            if (json != null)
            {
                PlayerInfo highScore = JsonUtility.FromJson<PlayerInfo>(json);
                Debug.Log("Highscore Player grabbed");
                Debug.Log(highScore.name + " " + highScore.score);
                highScorePlayer.name = highScore.name;
                highScorePlayer.score = highScore.score;
                UpdateScoreField();
            }
        }
    }
    
    public void AddScore(int score)
    {
        currentPlayer.Update(score);
        if (currentPlayer.score > highScorePlayer.score)
        {
            UpdateScoreField();
            SaveFile();
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerInfo currentPlayer = new PlayerInfo();
        PlayerInfo highScorePlayer = new PlayerInfo();
        LoadFile();
        nameField = GameObject.Find("NameInputField").GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class PlayerInfo
{
    public string name;
    public int score;

    public void Update(int score)
    {
        this.score = score;
    }
}

