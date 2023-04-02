using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AiConfig : ScriptableObject
{
    public float followStateDistance = 5.0f;
    public float followStateStopDistance = 5.0f;
    public float attackEnemyStateDistance = 5.0f;
    public float attackRange = 5.0f;

    public float maxHealth = 50.0f;

    public float Timer = 1.0f;
}
