using Game.BehaviorTree;
using UnityEngine;

namespace Game.Character.EnemyAI
{
    public class TaskAttack : Node
    {
        private Transform _lastTarget;
        private Player _playerManager;
        
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
                _playerManager = target.GetComponent<Player>();
                _lastTarget = target;
            }
            
            _attackCounter += Time.deltaTime;
            if (_attackCounter >= _attackTime)
            {
                bool enemyIsDead = _playerManager.TakeDamage();
                if (enemyIsDead)
                {
                    ClearData("target");
                    _animator.SetBool(EnemyBT.Attacking, false);
                    _animator.SetBool(EnemyBT.Running, true);
                    target.gameObject.SetActive(false);
                }
                else
                {
                    _attackCounter = 0f;
                }
            }

            state = NodeState.RUNNING;
            return state;
        }
    }
}