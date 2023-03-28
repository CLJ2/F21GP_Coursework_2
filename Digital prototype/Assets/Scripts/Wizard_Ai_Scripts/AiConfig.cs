using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AiConfig : ScriptableObject
{
    public float followStateDistance = 5.0f;
    public float attackEnemyStateDistance = 5.0f;
}
