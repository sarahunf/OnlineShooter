using UnityEngine;

namespace Game.Character.Player
{
    public class HealthBar : MonoBehaviour
    {
        private Vector3 _localScale;

        private void Start()
        {
            _localScale = transform.localScale;
        }

        public void UpdateView(byte amount)
        {
            _localScale.x = amount * 0.1f;
            transform.localScale = _localScale;
        }
    }
}