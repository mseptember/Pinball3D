using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demoSpriteBlink : MonoBehaviour
{
    // Start is called before the first frame update

    public SpriteRenderer sprite_mesh;
    public Sprite[] text_sprite;
    public float blinkspeed = 0.5f;
    public float max_cycle = 4;
    
    void Start()
    {
        StartCoroutine(blinkTarget());
    }

    
    int bid = 0;
    IEnumerator blinkTarget(){
          while(true) 
         { 
            yield return new WaitForSeconds(blinkspeed);
            sprite_mesh.sprite = text_sprite[bid];
            bid += 1;
            if(bid >= max_cycle)
            {
                bid = 0;
            }
        }
    }
}
