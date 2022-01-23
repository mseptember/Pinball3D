using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        // ZAGRAJ DŹWIĘK - strata kulki
        FindObjectOfType<AudioManager>().Play("LostBallSound");
        Destroy(col.gameObject);
        
        MissionManager.instance.ResetAllMissions();

        GameManager.instance.UpdateBallsOnPlayfield(-1);
        GameManager.instance.CreateNewBall();
    }
}
