using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLoader : MonoBehaviour
{
    private void Awake()
    {
        ScoreSavingManager.LoadFromFile();    
    }
}
