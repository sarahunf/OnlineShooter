using Photon.Pun;
using UnityEngine;

namespace Game.Character.Player
{
    public class Player : MonoBehaviour, ICharacter, IHealth
    {
        [SerializeField] private float speed;
        [SerializeField] private byte health;
        [SerializeField] private Animator anim;
        [SerializeField] private HealthBar healthBar;
        private readonly int _isRunning = Animator.StringToHash("isRunning");
        private PhotonView _view;

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
            _view = GetComponent<PhotonView>();
        }

        public void Update()
        {
            if (!_view.IsMine) return;

            var moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Move(moveInput);
        }

        public void Move(Vector2 moveInput)
        {
            var moveAmount = moveInput.normalized * Speed * Time.deltaTime;
            transform.position += (Vector3) moveAmount;
            anim.SetBool(_isRunning, moveInput != Vector2.zero);
        }

        public void Shoot()
        {
            throw new System.NotImplementedException();
        }

        
        public void TakeDamage()
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