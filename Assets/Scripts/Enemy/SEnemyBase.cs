using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SEnemyBase : ScriptableObject
{
    [SerializeField] new string name;
    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite frontSpritel;
    [SerializeField] Sprite backSprite;

    [SerializeField] EnemyType type;

    [SerializeField] int maxHP;
    [SerializeField] int attack;
    [SerializeField] int defense;

    public int Attack{ get => attack; }
    public int Defense{ get => defense; }
}

[Serializable]
public class LearnableMove
{
    [SerializeField] MoveBase Base;
    [SerializeField] int level;

    public MoveBase Base1 { get => Base; set => Base = value; }
    public int Level { get => level; set => level = value; }
}

public enum EnemyType
{
    None,
    Fire,
    Water,
    Electric,
}