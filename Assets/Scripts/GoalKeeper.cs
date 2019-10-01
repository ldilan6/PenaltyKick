using Gamelogic.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PenaltyKick
{
    public class GoalKeeper : Singleton<GoalKeeper>
    {
        public float BlockRange = 1.5f;
        public float MoveRange = 3f;

        GoalKeeperAICharacterControl ctrl;
        Vector3 StartPosition;
        Quaternion StartRotation;
        bool resetPosition;
        bool attemptedBlock;
        bool isGoal;

        void Awake()
        {
            ctrl = GetComponent<GoalKeeperAICharacterControl>();
            StartPosition = transform.position;
            StartRotation = transform.rotation;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (resetPosition) // once we see this set, move the GK back to original position and rotation
            {
                attemptedBlock = false;
                resetPosition = false;
                isGoal = false;
                transform.SetPositionAndRotation(StartPosition, StartRotation);
                return;
            }

            if (!isGoal) // Once a goal has happened, don't try to kick or block
            {
                if (!attemptedBlock && IsBallWithinBlockRange()) // Only block or catch once per kick
                {
                    attemptedBlock = true;
                    transform.LookAt(SoccerBall.Instance.transform);
                    ctrl.BlockBall();
                }
            }
        }
        // Called by GM when it knows a player kicked the ball
        public void OnShotTaken()
        {
            ctrl.SetTarget(SoccerBall.Instance.transform);
        }

        // Called by the GM when its time for the next kicker
        public void OnNextKicker()
        {
            resetPosition = true;
            ctrl.SetTarget(null);
            ctrl.Warp(StartPosition); // teleports player to original position
        }

        // Check distance to the ball compared to MoveRange
        public bool IsBallWithinMoveRange()
        {
            return (Vector3.Distance(transform.position, SoccerBall.Instance.transform.position) <= MoveRange);
        }

        // check distance to the ball compared to BlockRange
        public bool IsBallWithinBlockRange()
        {
            return (Vector3.Distance(transform.position, SoccerBall.Instance.transform.position) <= BlockRange);
        }

        // called by GM when a goal happened
        public void OnGoal()
        {
            isGoal = true;
        }
    }
}
