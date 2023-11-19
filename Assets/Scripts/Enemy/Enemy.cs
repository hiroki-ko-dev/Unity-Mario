using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    SEnemyBase _base;
    int level;

    public List<Move> Moves { get; set; }

    public Enemy(SEnemyBase pBase, int pLevel)
    {
        _base = pBase;
        level = pLevel;
    }

    public int Attack
    {
        get { return Mathf.FloorToInt((_base.Attack * level) / 100f) + 5; }
    }

    public int Defense
    {
        get { return Mathf.FloorToInt((_base.Defense * level) / 100f) + 5; }
    }

}
