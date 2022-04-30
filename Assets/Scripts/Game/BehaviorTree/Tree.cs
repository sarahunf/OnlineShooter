using UnityEngine;

namespace Game.BehaviorTree
{
    public abstract class Tree : MonoBehaviour
    {
        private Node _root = null;

        protected void Start()
        {
            _root = SetupTree();
        }
        
        private void Update()
        {
            _root?.Evaluate();
        }
        protected abstract Node SetupTree();
    }
}