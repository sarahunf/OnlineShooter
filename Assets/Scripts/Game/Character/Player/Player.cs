using Photon.Pun;
using ScriptableObjects;
using UnityEngine;

namespace Game.Character.Player
{
    public class Player : MonoBehaviour, ICharacter, IHealth
    {
        [SerializeField] private CharacterData characterData;
        [SerializeField] private float speed;
        [SerializeField] private byte health;
        [SerializeField] private Animator anim;
        [SerializeField] private HealthBar healthBar;
        private readonly int _isRunning = Animator.StringToHash("isRunning");
        private readonly int _isShooting = Animator.StringToHash("isShooting");
        private PhotonView _view;
        private Weapon.Weapon _weapon;

        private const string TakeDamageRPCMethod = "TakeDamageRPC";

        public float Speed
        {
            get => speed;
            set => speed = value;
        }

        public byte Health
        {
            get => health;
            set => health = value;
        }
        
        private void Start()
        {
            GetData();
            _view = GetComponent<PhotonView>();
        }

        public void Update()
        {
            if (!_view.IsMine) return;

            var moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (Input.GetButtonDown("Fire1"))
            {
                anim.SetTrigger(_isShooting);
            }
            Move(moveInput);
        }

        private void GetData()
        {
            speed = characterData.Speed;
            health = characterData.Health;
        }

        public void Move(Vector2 moveInput)
        {
            var moveAmount = moveInput.normalized * Speed * Time.deltaTime;
            transform.position += (Vector3) moveAmount;
            anim.SetBool(_isRunning, moveInput != Vector2.zero);
        }

        public void TakeDamage(byte amount = 0)
        {
            if (!_view.IsMine) return;
            _view.RPC(TakeDamageRPCMethod, RpcTarget.All);
        }

        [PunRPC]
        private void TakeDamageRPC() //reference: TakeDamageRPCReference
        {
            health--;
            healthBar.UpdateView(health);
        }

        public bool IsDead()
        {
            return Health <= 0;
        }

    }
}