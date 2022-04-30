using System.Collections.Generic;
using Game.BehaviorTree;

namespace Game.Character.EnemyAI
{
    public class EnemyBT : Tree
    {
        public UnityEngine.Transform[] waypoints;

        public static float speed = 3f;
        public static float fovRange = 6f;
        public static float attackRange = 2f;
        
        internal static readonly string Attacking ="isAttacking";
        internal static readonly string Running = "isRunning";

        //order is crucial. List priorities for enemy actions.
        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new CheckPlayerInAttackRange(transform),
                    new TaskAttack(transform),
                }),
                new Sequence(new List<Node>
                {
                    new CheckPlayerIFOVRange(transform),
                    new TaskGoToTarget(transform)
                }),
                new TaskPatrol(transform, waypoints)
            });
            return root;
        }
    }
}