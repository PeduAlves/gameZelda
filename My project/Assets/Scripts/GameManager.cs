using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyState{

    IDLE, ALERT, EXPLORE, PATROL, FOLLOW, FURY

}

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    public Transform Player;

    [Header("Slime IA")]
    public Transform[] SlimeWayPoints;
    public float SlimeIdleWaitTime = 3f;
    public float SlimeDistanceToAttack = 2.3f;

}
