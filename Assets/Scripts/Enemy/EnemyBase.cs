using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyBase : ScriptableObject
{
    [SerializeField] EnemyType type;
    //[SerializeField] EnemyMoveBase move;

    public enum EnemyType
    {
        Pig,
        Dog,
    }
}
