using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class EnemyAiConfig : ScriptableObject
{
    public float maxSightDistance = 5.0f;

    public float maxTime = 1.0f;
    public float minDistance = 1.0f;
}
