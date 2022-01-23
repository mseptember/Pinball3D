using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

[RequireComponent(typeof(Rigidbody))]
public class Plunger : MonoBehaviour
{
    float power;
    public float maxPower = 15f;
    public float powerCountPerTick = 5f;
    public Image image;

    Rigidbody ballRb;
    ContactPoint contactPoint;
    
    bool ballReady;

    void Update()
    {
        if(ballReady)
        {
            if(Input.GetKey(KeyCode.LeftControl))
            {
                if(power <= maxPower)
                {
                    power += powerCountPerTick * Time.deltaTime;
                }

                image.fillAmount = power / maxPower;
            }

            if(Input.GetKeyUp(KeyCode.LeftControl))
            {
                if(ballRb != null)
                {
                    ballRb.AddForce(-1 * power * contactPoint.normal, ForceMode.Impulse);
                    // ZAGRAJ DŹWIĘK
                    FindObjectOfType<AudioManager>().Play("PlungerSound");
                }
            }
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        ballReady = true;
        power = 0f;
        contactPoint = col.contacts[0];
        ballRb = contactPoint.otherCollider.attachedRigidbody;
    }

    private void OnCollisionExit(Collision col)
    {
        ballReady = false;
        ballRb = null;
    }
}
