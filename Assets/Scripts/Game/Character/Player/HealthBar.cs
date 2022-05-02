using UnityEngine;
using UnityEngine.UI;

namespace Game.Character.Player
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image fillBar;
        internal float startSize;

        public void UpdateView(byte amount)
        {
            fillBar.fillAmount = amount/startSize;
        }
    }
}