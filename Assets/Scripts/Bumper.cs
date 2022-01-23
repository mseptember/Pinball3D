using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]
public class Bumper : MonoBehaviour
{
    [SerializeField] float power = 5f;
    public bool isLighted, isAdded;

    Light bumperLight;
    Light bumperLightChildren;
    Animator anim;

    List<Bumper> bumpersList = new List<Bumper>();
    public int score;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        isLighted = false;
        isAdded = false;
        bumperLight = GetComponent<Light>();
        bumperLightChildren = transform.GetChild(0).GetComponent<Light>();
        bumperLight.enabled = false;
        bumperLightChildren.enabled = false;

        foreach (Bumper bumper in FindObjectsOfType(typeof(Bumper)))
        {
            bumpersList.Add(bumper);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (!isLighted)
        {
            isLighted = true;
            bumperLight.enabled = true;
            bumperLightChildren.enabled = true;
            ScoreManager.instance.AddLightedObject(gameObject);
            // ZAGRAJ DŹWIĘK - rozświetlenie bumpera
            FindObjectOfType<AudioManager>().Play("BumperLightSound");
        }

        foreach (ContactPoint contact in col.contacts)
        {
            contact.otherCollider.attachedRigidbody.AddForce(-1 * contact.normal * power, ForceMode.Impulse);
            
            // add points for every bumper hit
            ScoreManager.instance.AddScore(score);
            // ZAGRAJ DŹWIĘK - odbicie od bumpera
            FindObjectOfType<AudioManager>().Play("BumperSound");
        }

        if (anim != null)
        {
            anim.SetTrigger("activate");
        }
    }


}
