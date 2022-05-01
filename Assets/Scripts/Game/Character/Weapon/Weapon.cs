using Photon.Pun;
using UnityEngine;

namespace Game.Character.Weapon
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform centerTransform;
        private PhotonView _view;
        private const byte BulletMax = 100;
        private byte _bulletCount;
        private const string RPC_ShootMethod = "RPC_Shoot";
        private void Start()
        {
            _view = GetComponent<PhotonView>();
        }
        private void Update()
        {
            Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 lookAt = mouseScreenPosition;
            float AngleRad = Mathf.Atan2(lookAt.y - this.transform.position.y, lookAt.x - this.transform.position.x);
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            Debug.Log(AngleDeg);
            if (transform.rotation.z != AngleDeg)
            {
                transform.RotateAround(centerTransform.position, Vector3.forward, AngleDeg * Time.deltaTime);
            }
            
            if (!_view.IsMine) return;
            if (Input.GetButtonDown("Fire1") && _bulletCount < BulletMax)
            {
                Shoot();
            }
        }
        
        protected virtual void Shoot()
        {
            _bulletCount++;
            _view.RPC(RPC_ShootMethod, RpcTarget.AllViaServer);
        }

        [PunRPC]
        public void RPC_Shoot() //reference: RPC_ShootMethod
        {
            Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = worldMousePos - (Vector2)transform.position;
            direction.Normalize();
            if (!_view.IsMine) return;//double checking ownership seems to fix double spawning bullets
            GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Ammunition>().Move(direction);
        }
    }
}