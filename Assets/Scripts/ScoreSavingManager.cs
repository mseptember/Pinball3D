using System.Collections.Generic;
using UnityEngine;
using System.Linq;

    public struct ScoreData
    {
        public int Score;
        public string Name;

        public ScoreData(int score, string name)
        {
        Score = score;
        Name = name;
        }
    }

public static class ScoreSavingManager
{
    private const string scoreKeyStr = "score";
    private const string scoreNameKeyStr = "scoreName";
    private const  int maxScoreCount = 10;
    public static List<ScoreData> scoreDatas = new List<ScoreData>(maxScoreCount);

    public static void LoadFromFile()
    {
        scoreDatas.Clear();
        int scoreCount = 0;
        string scoreKey = $"{scoreKeyStr}0";
        string scoreNameKey = $"{scoreNameKeyStr}0";
        while (PlayerPrefs.HasKey(scoreKey) && scoreCount <= maxScoreCount)
        {
            int score = PlayerPrefs.GetInt(scoreKey);
            string name = PlayerPrefs.GetString(scoreNameKey);
            scoreDatas.Add( new ScoreData(score, name));
            scoreCount++;
            
            scoreKey = $"{scoreKeyStr}{scoreCount}";
            scoreNameKey = $"{scoreNameKeyStr}{scoreCount}";
        }
    }

    public static void SaveToFile()
    {
        for (int i = 0; i < scoreDatas.Count; i++)
        {
            PlayerPrefs.SetInt($"{scoreKeyStr}{i}", scoreDatas[i].Score);
            PlayerPrefs.SetString($"{scoreNameKeyStr}{i}", scoreDatas[i].Name);
        }
    }
    
    public static void SaveScore(string playerName, int score)
    {
        if (!CanScoreBeSaved(score))
            return;
        
        scoreDatas.Add(new ScoreData(score, playerName));
        
        if (scoreDatas.Count > maxScoreCount)
        {
            int lowestScoreIdx = 0;
            int lowestScore = int.MaxValue;
            for (int i = 0; i < scoreDatas.Count; i++)
            {
                if (scoreDatas[i].Score < lowestScore)
                {
                    lowestScore = scoreDatas[i].Score;
                    lowestScoreIdx = i;
                }
            }
            
            scoreDatas.RemoveAt(lowestScoreIdx);
        }
        
        SaveToFile();
    }

    public static bool CanScoreBeSaved(int score)
    {
        if(score == 0)
            return false;
            
        if (scoreDatas.Count < maxScoreCount)
            return true;

        return scoreDatas.Any(scoreData => scoreData.Score <= score);
    }
}
