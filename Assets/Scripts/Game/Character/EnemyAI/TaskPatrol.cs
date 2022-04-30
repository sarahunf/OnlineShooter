using Game.BehaviorTree;
using Unity.Mathematics;
using UnityEngine;

namespace Game.Character.EnemyAI
{
    public class TaskPatrol : Node
    {
        private Transform _transform;
        private Transform[] _waypoints;
        private Animator _animator;

        private int _currentWaypointIndex = 0;

        private float _waitTime = 1f;
        private float _waitCounter = 0f;
        private bool _waiting = false;

        public TaskPatrol(Transform transform, Transform[] waypoints)
        {
            _transform = transform;
            _waypoints = waypoints;
            _animator = transform.GetComponent<Animator>();
        }

        public override NodeState Evaluate()
        {
            if (_waiting)
            {
                _waitCounter += Time.deltaTime;
                if (_waitCounter >= _waitTime)
                {
                    _waiting = false;
                    _animator.SetBool(EnemyBT.Running, true);
                }
            }
            else
            {
                Transform wp = _waypoints[_currentWaypointIndex];
                if (Vector2.Distance(_transform.position, wp.position) < 0.01f)
                {
                    _transform.position = wp.position;
                    _waitCounter = 0f;
                    _waiting = true;

                    _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
                    _animator.SetBool(EnemyBT.Running, false);
                }
                else
                {
                    _transform.position = Vector2.MoveTowards(_transform.position,
                        wp.position,
                        EnemyBT.speed * Time.deltaTime);
                    _animator.SetBool(EnemyBT.Running, true);
                }
            }

            state = NodeState.RUNNING;
            return state;
        }
    }
}