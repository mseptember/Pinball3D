using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager instance;

    private void Awake()
    {
        instance = this;
    }

    public List<Mission> missionList = new List<Mission>();

    public void StartMission(int ID)
    {
        foreach (Mission mission in missionList)
        {
            if (mission.missionId == ID)
            {
                // MISSIONS
                if (!mission.missionComplete && !mission.active)
                {
                    mission.active = true;
                    Debug.Log("Misja aktywna: " + mission.active + " ID misji: " + mission.missionId);
                }
            }
        }
    }

    public void UpdateMission(int ID)
    {
        missionList.Find(m => m.missionId == ID).UpdateMission();
    }

    public void ResetAllMissions()
    {
        foreach (Mission mission in missionList)
        {
            if (mission.active)
            {
                mission.DeactivateMission();
            }
        }
    }

    public void ResetMissionById(int ID)
    {
        missionList.Find(m => m.missionId == ID).ResetMission();
    }
}
