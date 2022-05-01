using Game.BehaviorTree;
using UnityEngine;

namespace Game.Character.EnemyAI
{
    public class TaskAttack : Node
    {
        private Transform _lastTarget;
        private Player.Player _playerManager;
        
        private float _attackTime = 1f;
        private float _attackCounter = 0f;

        private Animator _animator;

        public TaskAttack(Transform transform)
        {
            _animator = transform.GetComponent<Animator>();
        }

        public override NodeState Evaluate()
        {
            Transform target = (Transform)GetData("target");
            if (target != _lastTarget)
            {
                _playerManager = target.GetComponent<Player.Player>();
                _lastTarget = target;
            }
            
            _attackCounter += Time.deltaTime;
            if (_attackCounter >= _attackTime)
            {
                if (_playerManager.IsDead())
                {
                    ClearData("target");
                    _animator.SetBool(EnemyBT.Attacking, false);
                    _animator.SetBool(EnemyBT.Running, true);
                }
                else
                {
                    _playerManager.TakeDamage();
                    _attackCounter = 0f;
                }
            }

            state = NodeState.RUNNING;
            return state;
        }
    }
}