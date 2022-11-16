using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    [SerializeField] float speed;
    public Sprite[] sprites;
    SpriteRenderer spriteRenderer;
    float timer;
    int index=0;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        timer = speed;
    }
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = speed;
            index = (index + 1 < sprites.Length) ? index + 1 : 0;
            spriteRenderer.sprite = sprites[index];
        }
    }
}
