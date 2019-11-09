﻿using Gamelogic.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PenaltyKick
{
	public class SoccerBall : Singleton<SoccerBall>
	{
		[System.NonSerialized]
		public bool IsKicked;

		[System.NonSerialized]
		public bool CanScoreGoal;

		Vector3 StartPosition;
        float timeSinceKick;

		private void Awake()
		{
			StartPosition = transform.position;
		}

		void Update()
		{
            if (IsKicked)
            {
                timeSinceKick += Time.deltaTime;
                if (timeSinceKick >= GameManager.Instance.CheckForGoalSecs)
                {
                    timeSinceKick = 0;
                    IsKicked = false;
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    transform.position = StartPosition;
                }
            }			
		}

		public void OnNextKicker()
		{
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
			transform.position = StartPosition;

			CanScoreGoal = true;
		}
	}
}