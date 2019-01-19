using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighscoreData
{
    public string Username;
    public int Score;

    public HighscoreData(string _Username, int _Score)
    {
        Username = _Username;
        Score = _Score;
    }
}
