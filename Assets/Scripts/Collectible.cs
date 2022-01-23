using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public static Collectible instance;

    private void Awake()
    {
        instance = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.StartMultiball();
        gameObject.SetActive(false);
        // ZAGRAJ DŹWIĘK ZŁAPANIA COLLECTIBLE
        FindObjectOfType<AudioManager>().Play("CollectedSound");
    }
}
