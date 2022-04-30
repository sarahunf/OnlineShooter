using Game.BehaviorTree;
using UnityEngine;

namespace Game.Character.EnemyAI
{
    public class CheckPlayerIFOVRange : Node
    {
        private Transform _transform;
        private Animator _animator;
        private static int _playerLayerMask = 1 << LayerMask.NameToLayer("Player");

        public CheckPlayerIFOVRange(Transform transform)
        {
            _transform = transform;
            _animator = transform.GetComponent<Animator>();
        }

        public override NodeState Evaluate()
        {
            var t = GetData("target");
            if (t == null)
            {
                Collider2D[] colliders =
                {
                    Physics2D.OverlapCircle(_transform.position, EnemyBT.fovRange, _playerLayerMask)
                };

                if (colliders.Length > 0 && colliders[0])
                {
                    parent.parent.SetData("target", colliders[0].transform);
                    _animator.SetBool(EnemyBT.Running, true);
                    state = NodeState.SUCCESS;
                    return state;
                }

                state = NodeState.FAILURE;
                return state;
            }

            state = NodeState.SUCCESS;
            return state;
        }
    }
}