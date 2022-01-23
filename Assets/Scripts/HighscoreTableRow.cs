using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTableRow : MonoBehaviour
{
    public Text score;
    public Text playerName;
    public Text rankText;

    public void Show(ScoreData scoreData, int rank)
    {
        score.text = scoreData.Score.ToString();
        playerName.text = scoreData.Name;
        rankText.text = rank.ToString();
    }
}
