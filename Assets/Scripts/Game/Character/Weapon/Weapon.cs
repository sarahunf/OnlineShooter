using Game.Character.Player;
using Game.Spawner;
using Photon.Pun;
using ScriptableObjects;
using UnityEngine;

namespace Game.Character.Weapon
{
    public class Weapon : MonoBehaviourPun
    {
        [SerializeField] private WeaponData weaponData;
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Player.Player myPlayer;
        [SerializeField] private ViewBar ammoBar;
        private byte _bulletMax;
        private byte _bulletCount;
        private static int _lootLayerMask;
        private float _positionOffset = 0.86f;

        private void Start()
        {
            _lootLayerMask = LayerMask.NameToLayer("LootBox");
            GetData();
            SetData();
        }

        private void Update()
        {
            if (!photonView.IsMine) return;
            if (Input.GetButtonDown("Fire1") && CanShoot())
            {
                Shoot(photonView.Owner);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.layer != _lootLayerMask) return;

            LootBox loot = col.GetComponent<LootBox>();
            loot.TakeDamage();
            if (!loot.IsDead()) return;

            loot.collectible.player = photonView.Owner;
            photonView.RPC(nameof(RPCChangeWeapon), RpcTarget.All);
        }

        private void GetData()
        {
            _bulletMax = weaponData.maxAmmunition;
            _bulletCount = _bulletMax;
        }

        private void SetData()
        {
            Ammunition ammo = bulletPrefab.GetComponent<Ammunition>();
            ammo.ammunitionData = weaponData.AmmunitionData;
            ammoBar.startSize = _bulletMax;
        }

        protected virtual void Shoot(Photon.Realtime.Player player)
        {
            _bulletCount--;
            photonView.RPC(nameof(RPCShoot), RpcTarget.AllViaServer);
        }

        [PunRPC]
        private void RPCShoot()
        {
            if (!CanShoot()) return;
            ammoBar.UpdateView(_bulletCount);
            Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = worldMousePos - (Vector2) transform.position;
            direction.Normalize();
            if (!photonView.IsMine) return; //double checking ownership seems to fix double spawning bullets

            GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Ammunition>().Move(direction, photonView.Owner);
        }

        [PunRPC]
        private void RPCChangeWeapon()
        {
            WeaponData[] allWeapons = Resources.LoadAll<WeaponData>("Data/Weapons");
            WeaponData randomWeapon = allWeapons[Random.Range(0, allWeapons.Length)];
            weaponData = randomWeapon;
            GetData();
            photonView.RPC(nameof(RPCReloadWeapon), RpcTarget.All);
            SetData();
            transform.position = new Vector3(myPlayer.transform.position.x,
                myPlayer.transform.position.y + _positionOffset, myPlayer.transform.position.z);
        }

        [PunRPC]
        private void RPCReloadWeapon()
        {
            _bulletCount = _bulletMax;
            ammoBar.UpdateView(_bulletCount);
        }

        private bool CanShoot()
        {
            return _bulletCount > 0;
        }
    }
}