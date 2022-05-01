using Photon.Pun;
using ScriptableObjects;
using UnityEngine;

namespace Game.Character.Weapon
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponData _weaponData;
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject bulletPrefab;
        private PhotonView _view;
        private byte _bulletMax;
        private byte _bulletCount;
        private const string RPC_ShootMethod = "RPC_Shoot";
        private void Start()
        {
            _view = GetComponent<PhotonView>();
            GetData();
            SetData();
        }

        private void Update()
        {
            if (!_view.IsMine) return;
            if (Input.GetButtonDown("Fire1") && _bulletCount < _bulletMax)
            {
                Shoot();
            }
        }
        
        private void GetData()
        {
            _bulletMax = _weaponData.maxAmmunition;
        }

        private void SetData()
        {
            Ammunition ammun = bulletPrefab.GetComponent<Ammunition>();
            ammun.ammunitionData = _weaponData.AmmunitionData;
        }

        protected virtual void Shoot()
        {
            _bulletCount++;
            _view.RPC(RPC_ShootMethod, RpcTarget.AllViaServer);
        }

        [PunRPC]
        private void RPC_Shoot() //reference: RPC_ShootMethod
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