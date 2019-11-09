using Gamelogic.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PenaltyKick
{
    public class PlayerAI : Singleton<PlayerAI>
    {
        public float KickRange = 1f;

        PlayerAICharacterControl ctrl;
        Vector3 StartPosition;
        Quaternion StartRotation;
        bool resetPosition;
        bool attemptedKick;
        bool isGoal;

        void Awake()
        {
            ctrl = GetComponent<PlayerAICharacterControl>();
            StartPosition = transform.position;
            StartRotation = transform.rotation;
        }
        // Start is called before the first frame update
        void Start()
        {
            ctrl.SetTarget(SoccerBall.Instance.transform);
        }

        // Update is called once per frame
        void Update()
        {
            if (resetPosition)
            {
                attemptedKick = false;
                resetPosition = false;
                isGoal = false;
                transform.SetPositionAndRotation(StartPosition, StartRotation);
                return;
            }

            if (!attemptedKick && IsBallWithinKickRange())
            {
                attemptedKick = true;
                ctrl.KickBall();
                ctrl.SetTarget(null);
            }
        }
        public void OnShotTaken()
        {

        }

        public void OnNextKicker()
        {
            resetPosition = true;
            ctrl.SetTarget(null);
        }

        public bool IsBallWithinKickRange()
        {
            return (Vector3.Distance(transform.position, SoccerBall.Instance.transform.position) <= KickRange);
        }

        public void OnGoal()
        {
            isGoal = true;
        }
    }
}
