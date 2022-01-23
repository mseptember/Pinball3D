using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demoLetterCycle : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer[] sprite_mesh;
    public Sprite[] text_sprite;
    public Sprite[] text_sprite_highlight;
    public float blinkspeed = 0.5f;

    
    void Start()
    {
        StartCoroutine(cycleText());
    }

    
    int bid = 0;
    IEnumerator cycleText(){
          while(true) 
         { 
           
            yield return new WaitForSeconds(blinkspeed);
             for(int i=0; i < text_sprite.Length; i++)
            {
                sprite_mesh[i].sprite = text_sprite[i];
            }
            sprite_mesh[bid].sprite = text_sprite_highlight[bid];
            bid += 1;
            if(bid >= text_sprite.Length)
            {
                bid = 0;
            }
        }
    }
}
