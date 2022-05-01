using System;
using System.Collections.Generic;
using Game.BehaviorTree;
using UnityEngine;
using Tree = Game.BehaviorTree.Tree;

namespace Game.Character.EnemyAI
{
    public class EnemyBT : Tree
    {
        public Transform[] waypoints;

        public static float speed = 3f;
        public static float fovRange = 9f;
        public static float attackRange = 2f;
        
        internal static readonly string Attacking ="isAttacking";
        internal static readonly string Running = "isRunning";
        

        //order is crucial. Prioritizes hierarchy of enemy actions
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