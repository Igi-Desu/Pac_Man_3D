using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEyes : MonoBehaviour
{
    // Start is called before the first frame update
    enum Eyes {up=0,right=1,down=2,left=3};
    [SerializeField]Sprite[] EyeSprites = new Sprite[4];
    SpriteRenderer spriterenderer;
    
    void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    public void changeEyes(Vector2 dir)
    {
        if (dir == Vector2.up)
        {
            spriterenderer.sprite = EyeSprites[(int)Eyes.up];
        }
        else if(dir==Vector2.left)
        {
            spriterenderer.sprite = EyeSprites[(int)Eyes.left];
        }
        else if (dir == Vector2.right)
        {
            spriterenderer.sprite = EyeSprites[(int)Eyes.right];
        }
        else if (dir == Vector2.down)
        {
            spriterenderer.sprite = EyeSprites[(int)Eyes.down];
        }
    }
}
