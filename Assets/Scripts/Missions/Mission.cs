using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mission
{
    public int missionId; // each mission must have an ID
    public string description; // mission description

    [Space] // helpful in the inspectior view in Unity; more clean view
    public bool active; // is mission active
    public bool permanentActive; // for permanent active missions (always active, even afrter restarting missions)
    public bool missionComplete; // is mission completed
    [Space]
    public bool restartOnNextBall; // if mission should restart after ball fall down
    public bool stopOnBallEnd; // if misiion can continue after ball fall donw
    public bool resetOnComplete; // if mission is repeatable
    public bool canTriggerMultiball; // if mission can trigger multiball
    [Space]
    public int score; // how many points player gets after compliting mission
    public int amountToComplete; // how many times player has to do something, e.g. how many times ball should hit bumpers
    public int currentAmount; // counter of the above, e.g. 2 of 4

    public void ResetMission()
    {
        if (resetOnComplete)
        {
            if (permanentActive)
            {
                active = true;
            }
            else
            { 
                active = false;
            }
            missionComplete = false;
            currentAmount = 0;
        }
    }

    public void DeactivateMission()
    {
        /*
        if permanentActive mission flag is set to true then activate it by setting active flag to false, otherwise set active flag to false
        */
        if (permanentActive)
        {
            active = true;
        }
        else
        {
            active = false;
            currentAmount = 0;
        }

    }

    public void ResetRolloverMission()
    {
        ScoreManager.instance.ResetRolloverLights();
    }

    public void UpdateMission()
    {
        if (active && !missionComplete)
        {
            currentAmount++;

            // CHECK IF THE MISSION IS COMPLETE
            CheckMissionComplete();
        }
    }

    void CheckMissionComplete()
    {
        if (currentAmount >= amountToComplete)
        {
            Debug.Log("Misja "+ missionId + " zakończona pomyślnie!");
            missionComplete = true;
            active = false;

            if (canTriggerMultiball) // if mission can start multiball, activate multiball GameObject, then multiball courotine will start OnTrigger inside Collectible
            {
                Collectible.instance.gameObject.SetActive(true);
            }

            ScoreManager.instance.AddScore(score);
            // ZAGRAJ DŹWIĘK - mission complete
            GameObject.FindObjectOfType<AudioManager>().Play("MissionComplete");
            
            // if mission to rollovermission to reset świateł, i dopiero potem reset misji
            if (missionId == 3)
            {
                ResetRolloverMission();
            }
            else
            {
                ResetMission();
            }
        }
    }

    public bool GetMissionActive()
    {
        return active;
    }
}
