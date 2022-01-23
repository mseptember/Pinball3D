using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public bool isLighted;
    //public bool isAdded;

    Light targetLight;
    Light targetLightChildren;
    
    List<Target> targetsList = new List<Target>();
    public int score;

    void Start()
    {
        targetLight = GetComponent<Light>();
        isLighted = false;
        //isAdded = false;
        targetLightChildren =transform.GetChild(0).GetComponent<Light>();
        targetLight.enabled = false;
        targetLightChildren.enabled = false;

        foreach (Target target in FindObjectsOfType(typeof(Target)))
        {
            targetsList.Add(target);
        }
    }

    private void OnCollisionEnter(Collision _col)
    {
        if (!isLighted)
        {
            isLighted = true;
            targetLight.enabled = true;
            targetLightChildren.enabled = true;
            ScoreManager.instance.AddLightedObject(gameObject);
            // ZAGRAJ DŹWIĘK - rozświetlenie targeta
            FindObjectOfType<AudioManager>().Play("TargetLightSound");
        }

        // add points for every target hit
        ScoreManager.instance.AddScore(score);
        FindObjectOfType<AudioManager>().Play("TargetSound");
    }
}
