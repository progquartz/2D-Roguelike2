﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Sprite dmgSprite; // 플레이어가 벽을 쳤을때 벽을 성공적으로 쳤음을 보여줄 수 있는 스프라이트.
    public int hp = 4;
    public AudioClip chopSound1;
    public AudioClip chopSound2;


    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DamageWall (int loss)
    {
        SoundManager.instance.RandomizeSfx(chopSound1, chopSound2);
        spriteRenderer.sprite = dmgSprite;
        hp -= loss;
        if(hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
