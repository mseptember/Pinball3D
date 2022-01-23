using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public bool isStarter; // TRIGGER TO START MISSION
    public bool isCounter; // COUNT RUNNING MISSION

    public bool showGizmos;
    [Space]
    public int triggerId; // MISSION ID TO TRIGGER

    private void OnTriggerEnter(Collider colider)
    {
        // zmienna potrzebna jeśli misja ma liczyć odbicia dopiero po aktywowaniu misji
        //bool startedAlready = MissionManager.instance.CheckIfMissionStarted(triggerId);

        if (isStarter)
        {
            MissionManager.instance.StartMission(triggerId);
        }

        // jeżeli misja ma liczyć odbicia dopiero po uruchomieniu misji to do warunku ifa wrzuca się dodatkowo zmienną startedAlready
        if (isCounter)
        {
            if (GetComponent<Rollover>())
            {
                if (!GetComponent<Rollover>().isChecked)
                {
                    MissionManager.instance.UpdateMission(triggerId);
                }
            }
            else
            {
                MissionManager.instance.UpdateMission(triggerId);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        // zmienna potrzebna jeśli misja ma liczyć odbicia dopiero po aktywowaniu misji
        //bool startedAlready = MissionManager.instance.CheckIfMissionStarted(triggerId);

        if (isStarter)
        {
            MissionManager.instance.StartMission(triggerId);
        }

        //(isCounter && startedAlready) jeżeli misja ma dodać +1 do countera dopiero po aktywowaniu misji
        if (isCounter)
        {
            if (GetComponent<Bumper>())
            {
                if (!GetComponent<Bumper>().isAdded)
                {
                    GetComponent<Bumper>().isAdded = true;
                    MissionManager.instance.UpdateMission(triggerId);
                }
            }
            else // przypadek w którym można uderzać kilka razy jeden obiekt (target) żeby wykonać misję
            {
                MissionManager.instance.UpdateMission(triggerId);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = new Color32(0, 0, 255, 125);
            Gizmos.matrix = transform.localToWorldMatrix;
            //Gizmos.DrawCube(Vector3.zero, Vector3.one);
            Gizmos.DrawCube(new Vector3(0, 1, 0), new Vector3(3, 2, 3));
        }
    }

}
