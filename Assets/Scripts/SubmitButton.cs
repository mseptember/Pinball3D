using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmitButton : MonoBehaviour
{
    public InputField playerName;
    public ScoreManager scoreManager;

    public void SavePlayerScore()
    {
        scoreManager.SaveCurrentScore(playerName.text);
    }
}
