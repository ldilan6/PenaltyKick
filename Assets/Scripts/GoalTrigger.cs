using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PenaltyKick
{
    public class GoalTrigger : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Ball" && SoccerBall.Instance.CanScoreGoal)
            {
                GameManager.Instance.OnGoal();
            }
        }

    }
}
