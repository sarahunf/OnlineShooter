using UnityEngine;

namespace Game.Character.Player
{
    public class HealthBar : MonoBehaviour
    {
        private Vector3 _localScale;
        private float ySize = 0.2f;

        private void Start()
        {
            _localScale = transform.localScale;
        }

        public void UpdateView(byte amount)
        {
            _localScale.x = amount;
            _localScale.Normalize();
            _localScale.y = ySize;
            transform.localScale = _localScale;
        }
    }
}