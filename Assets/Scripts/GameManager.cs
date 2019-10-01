using Gamelogic.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PenaltyKick
{
    public class GameManager : Singleton<GameManager>
    {
        public Transform PlayerStartPosition;
        public Transform AIStartPosition;
        public bool UseJoystick;
        public FixedJoystick Joystick;

        bool IsShotTaken = false;

        public float KickPower = 11;
        public float KickPowerUp = 4;
        public float KickRange = 1;

        float timeSinceShot;

        // Start is called before the first frame update
        void Start()
        {
            if (UseJoystick)
                Joystick.gameObject.SetActive(true);
            else
                Joystick.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnShotTaken()
        {
            IsShotTaken = true;
            timeSinceShot = 0;
            //TeamDatas[CurrentTeam - 1].TotalShots++;

            GoalKeeper.Instance.OnShotTaken();
        }
    }
}
