using Gamelogic.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerBall : Singleton<SoccerBall>
{
    [System.NonSerialized]
    public bool IsKicked;

    [System.NonSerialized]
    public bool CanScoreGoal;

    Vector3 StartPosition;

    private void Awake()
    {
        StartPosition = transform.position;
    }

    public void OnNextKicker()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        transform.position = StartPosition;

        CanScoreGoal = true;
    }
}
