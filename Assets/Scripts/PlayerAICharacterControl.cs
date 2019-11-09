using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PenaltyKick
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof(SoccerPlayerCharacter))]
    public class PlayerAICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public SoccerPlayerCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for
        public Transform CatchHeightTarget;

        private void Awake()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<SoccerPlayerCharacter>();
        }
        // Start is called before the first frame update
        private void Start()
        {
            agent.updateRotation = false;
            agent.updatePosition = true;
        }

        // Update is called once per frame
        private void Update()
        {
            if (target != null)
            {
                agent.isStopped = false;
                agent.updateRotation = false;
                agent.updatePosition = true;
                agent.SetDestination(target.position);
            }
            else
            {
                agent.updateRotation = false;
                agent.updatePosition = false;
                agent.isStopped = true;
            }

            if (agent.remainingDistance > agent.stoppingDistance)
                character.Move(agent.desiredVelocity, false, false, false, false, false);
            else
                character.Move(Vector3.zero, false, false, false, false, false);
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
            if (target == null)
            {
                this.target = null;
            }
        }

        public void KickBall()
        {
            character.Move(agent.desiredVelocity, false, false, true, false, false);
        }
    }
}
