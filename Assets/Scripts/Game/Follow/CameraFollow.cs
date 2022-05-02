using UnityEngine;

namespace Game.Follow
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float smoothFactor;
        [SerializeField] private Vector3 minValues;
        [SerializeField] private Vector3 maxValues;
        private Transform _playerTransform;

        void FixedUpdate()
        {
            if (!_playerTransform) return;
            Follow();
        }
 
        public void setTarget(Transform target)
        {
            _playerTransform = target;
        }

        private void Follow()
        {
            Vector3 targetPosition = _playerTransform.position;
            float x = Mathf.Clamp(targetPosition.x, minValues.x, maxValues.x);
            float y = Mathf.Clamp(targetPosition.y, minValues.y, maxValues.y);
            float z = Mathf.Clamp(targetPosition.z, minValues.z, maxValues.z);

            Vector3 smoothPosition =
                Vector3.Lerp(transform.position, new Vector3(x, y, z), smoothFactor * Time.fixedDeltaTime);
            transform.position = smoothPosition;

        }
    }

}