using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score;
    private int addLifeScore = 0;
    
    private List<GameObject> lightedObjectsList = new List<GameObject>();
    private List<GameObject> rolloversList = new List<GameObject>();
    
    public static ScoreManager instance;
    public BumperPresetsHolder bumperPresetsHolderInstance;
    public int loops;
    public List<GameObject> objectsList = new List<GameObject>();
    
    private void Awake()
    {
        instance = this;

        bumperPresetsHolderInstance.ActivateRandomPreset();
        
        foreach (GameObject go in FindObjectsOfType(typeof(GameObject), false))
        {
            if (go.GetComponent<Bumper>() || go.GetComponent<Target>())
            {
                objectsList.Add(go);
            }
        }

        foreach (GameObject go in FindObjectsOfType(typeof(GameObject), false))
        {
            if (go.GetComponent<Rollover>())
            {
                rolloversList.Add(go);
            }
        }
    }

    private void Start()
    {
        UIManager.instance.UpdateScoreText(score);
        UIManager.instance.UpdateLoopsText(loops);
    }
    
    public void SaveCurrentScore(string playerName)
    {
        ScoreSavingManager.SaveScore(playerName, ReadFinalScore());
    }

    public void AddScore(int number)
    {
        score += number * (loops + 1);
        addLifeScore += number;
        UIManager.instance.UpdateScoreText(score);
    }

    public void ResetScore()
    {
        score = 0;
        addLifeScore = score;
        UIManager.instance.UpdateScoreText(score);
    }

    public void AddLoop()
    {
        // ZAGRAJ DŹWIĘK - loop
        FindObjectOfType<AudioManager>().Play("LoopSound");
        loops++;
        UIManager.instance.UpdateLoopsText(loops);
    }

    public void ResetLoops()
    {
        loops = 0;
        UIManager.instance.UpdateLoopsText(loops);
    }

    public void AddLightedObject(GameObject go)
    {
        lightedObjectsList.Add(go);
        UIManager.instance.UpdateLightsText(lightedObjectsList.Count);
        IsEveryObjectLighted();
    }

    // function resets lights of loop objects
    public void ResetLights()
    {
        foreach (GameObject go in lightedObjectsList)
        {
            go.GetComponent<Light>().enabled = false;
            if (go.transform.childCount > 0)
            {
                go.transform.GetChild(0).GetComponent<Light>().enabled = false;
            }

            if (go.GetComponent<Bumper>() != null)
            {
                go.GetComponent<Bumper>().isLighted = false;
                go.GetComponent<Bumper>().isAdded = false;
            }
            else if (go.GetComponent<Target>() != null)
            {
                go.GetComponent<Target>().isLighted = false;
                //go.GetComponent<Target>().isAdded = false;
            }
        }
        
        lightedObjectsList.Clear();
        bumperPresetsHolderInstance.ActivateRandomPreset();
        UIManager.instance.UpdateLightsText(lightedObjectsList.Count);
    }

    // Function resets all lights (including rollovers)
    public void ResetAllLights()
    {
        foreach (GameObject go in lightedObjectsList)
        {
            go.GetComponent<Light>().enabled = false;
            if (go.transform.childCount > 0)
            {
                go.transform.GetChild(0).GetComponent<Light>().enabled = false;
            }

            if (go.GetComponent<Bumper>() != null)
            {
                go.GetComponent<Bumper>().isLighted = false;
                go.GetComponent<Bumper>().isAdded = false;
            }
            else if (go.GetComponent<Target>() != null)
            {
                go.GetComponent<Target>().isLighted = false;
                //go.GetComponent<Target>().isAdded = false;
            }
        }

        foreach (var rollover in rolloversList)
        {
            rollover.transform.GetChild(0).GetComponent<Light>().enabled = false;
            rollover.GetComponent<Rollover>().isChecked = false;
        }
        
        lightedObjectsList.Clear();
        bumperPresetsHolderInstance.ActivateRandomPreset();
        UIManager.instance.UpdateLightsText(lightedObjectsList.Count);
    }

    public void ResetRolloverLights()
    {
        StartCoroutine(WaitBeforeResetRollovers());
    }

    public void IsEveryObjectLighted()
    {
        if (lightedObjectsList.Count.Equals(objectsList.Count))
        {
            AddLoop();
            
            StartCoroutine(WaitBeforeTurnOffLights());
        }
    }

    public int ReadFinalScore()
    {
        if (loops > 0)
        {
            return (loops +1) * score;
        }
        else
        {
            return score;
        }
    }
    
    // After getting 100000 points the player gets extra life
    public void CheckAddLife()
    {
        if (addLifeScore >= 100000)
        {
            GameManager.instance.AddLife();
            addLifeScore = 0;
        }
    }

    IEnumerator WaitBeforeTurnOffLights()
    {
        for (int i = 0; i < 5; i++)
        {
            foreach (GameObject go in lightedObjectsList)
            {
                go.GetComponent<Light>().enabled = false;
                if (go.transform.childCount > 0)
                {
                    go.transform.GetChild(0).GetComponent<Light>().enabled = false;
                }
            }

            // ZAGRAJ DŹWIĘK - gaszenie rozświetlonych
            FindObjectOfType<AudioManager>().Play("SwitchLightsSound");
            yield return new WaitForSeconds(0.5f);

            foreach (GameObject go in lightedObjectsList)
            {
                go.GetComponent<Light>().enabled = true;
                if (go.transform.childCount > 0)
                {
                    go.transform.GetChild(0).GetComponent<Light>().enabled = true;
                }
            }

            // ZAGRAJ DŹWIĘK - zapalanie światła
            FindObjectOfType<AudioManager>().Play("SwitchLightsSound");
            yield return new WaitForSeconds(0.5f);
        }
        
        ResetLights();
    }

    IEnumerator WaitBeforeResetRollovers()
    {
        GameManager.instance.AddLife();
        
        for (int i = 0; i < 3; i++)
        {
            foreach (GameObject go in rolloversList)
            {
                go.transform.GetChild(0).GetComponent<Light>().enabled = false;
                go.GetComponent<Rollover>().isChecked = false;
            }

            // ZAGRAJ DŹWIĘK - gaszenie rozświetlonych
            FindObjectOfType<AudioManager>().Play("SwitchLightsSound");
            yield return new WaitForSeconds(0.5f);

            foreach (GameObject go in rolloversList)
            {
                go.transform.GetChild(0).GetComponent<Light>().enabled = true;
                go.GetComponent<Rollover>().isChecked = true;
            }

            // ZAGRAJ DŹWIĘK - zapalanie światła
            FindObjectOfType<AudioManager>().Play("SwitchLightsSound");
            yield return new WaitForSeconds(0.5f);
        }
        
        foreach (GameObject go in rolloversList)
        {
            go.transform.GetChild(0).GetComponent<Light>().enabled = false;
            go.GetComponent<Rollover>().isChecked = false;
        }
        
        // Reset rollover mission
        MissionManager.instance.ResetMissionById(3);
    }
}