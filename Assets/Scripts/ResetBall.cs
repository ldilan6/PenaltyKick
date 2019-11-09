using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PenaltyKick
{
    public class ResetBall : MonoBehaviour
    {
        public bool IsKicked;

        Vector3 StartPosition;
        float timeSinceKick;

        private void Awake()
        {
            StartPosition = transform.position;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
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
    }
}
