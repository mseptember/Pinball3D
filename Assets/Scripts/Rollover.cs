using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rollover : MonoBehaviour
{
    [HideInInspector] public bool isChecked;
    Animator animator;
    Collider col;
    Light rolloverLight;
    public List<Rollover> rolloversList = new List<Rollover>();
    public int score;

    void Start()
    {
        col = GetComponent<Collider>();
        isChecked = false;
        rolloverLight = transform.GetChild(0).GetComponent<Light>();
        rolloverLight.enabled = false;

        foreach (Rollover rollover in FindObjectsOfType(typeof(Rollover)))
        {
            rolloversList.Add(rollover);
        }
    }

    private void OnTriggerEnter(Collider colider)
    {
        if (!isChecked)
        {
            isChecked = true;
            rolloverLight.enabled = true;
            // ZAGRAJ DŹWIĘK - rollover
            FindObjectOfType<AudioManager>().Play("RolloverSound");
        }

        // add points for every rollover passed by
        ScoreManager.instance.AddScore(score);
    }
}
