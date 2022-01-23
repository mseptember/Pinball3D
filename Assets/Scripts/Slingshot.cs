using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(MeshCollider))]
public class Slingshot : MonoBehaviour
{
    [SerializeField] float power = 5f;

    private void OnCollisionEnter(Collision col)
    {

        foreach (ContactPoint contact in col.contacts)
        {
            contact.otherCollider.attachedRigidbody.AddForce(-1 * contact.normal * power, ForceMode.Impulse);
            // ZAGRAJ DŹWIĘK - odbicie od slingshota
            FindObjectOfType<AudioManager>().Play("SlingshotSound");
        }
    }
}
