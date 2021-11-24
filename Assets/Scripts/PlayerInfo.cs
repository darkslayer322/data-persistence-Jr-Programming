using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public string name;
    public int score;


    public void Update(int score)
    {
        this.score = score;
    }


    public void SaveData()
    {
        SaveSystem.SaveData(this);
    }

    public PlayerInfo(string name)
    {
        this.name = name;
        this.score = 0;
    }
    public PlayerInfo(PlayerInfo playerInfo)
    {
        this.name = playerInfo.name;
        this.score = playerInfo.score;
    }

}