using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyState{

    IDLE, ALERT, PATROL, FOLLOW, FURY

}

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    public Transform Player;
    public float PlayerGetHitDelay = 1.5f;

    [Header("Slime IA")]
    public Transform[] SlimeWayPoints;
    public float SlimeIdleWaitTime = 3f;
    public float SlimeDistanceToAttack = 2.3f;
    public float SlimeAlertTime = 1f;
    public float SlimeAttackDelay = 1f;
    public float SlimeLookAtSpeed = 1f;

    [Header("Slime Config")]
    public int SlimeHitDamage = 5;

}
