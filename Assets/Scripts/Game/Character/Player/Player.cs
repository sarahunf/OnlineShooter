using Game.Follow;
using Game.View;
using Photon.Pun;
using Photon.Scripts.Managers;
using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Game.Character.Player
{
    public class Player : MonoBehaviourPun, ICharacter, IHealth
    {
        [SerializeField] private float speed;
        [SerializeField] private byte health;
        [SerializeField] private CharacterData characterData;
        [SerializeField] private Animator anim;
        [SerializeField] private TextMeshProUGUI nicknameText;
        [SerializeField] private ViewBar healthBar;
        private readonly int _isRunning = Animator.StringToHash("isRunning");
        private readonly int _isShooting = Animator.StringToHash("isShooting");
        private GameOverView _gameOverView;

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
            SetData();
            SetComponents();
            nicknameText.text = photonView.IsMine ? ConnectPhotonManager.ME.GetNickName() : ConnectPhotonManager.ME.GetOtherNickName(photonView);
        }


        public void Update()
        {
            if(!photonView.IsMine || IsDead()) return;
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

        private void SetData()
        {
            healthBar.startSize = health;
        }

        private void SetComponents()
        {
            if (!photonView.IsMine) return;
            _gameOverView = FindObjectOfType<GameOverView>();
            Camera.main.GetComponent<CameraFollow>().setTarget(gameObject.transform);
        }
        
        public void Move(Vector2 moveInput)
        {
            var moveAmount = moveInput.normalized * Speed * Time.deltaTime;
            transform.position += (Vector3) moveAmount;
            anim.SetBool(_isRunning, moveInput != Vector2.zero);
        }

        public void TakeDamage(byte amount = 0)
        {
            if (!photonView.IsMine) return;
            if (Health <= 0) return;
            photonView.RPC(nameof(RPCTakeDamage), RpcTarget.All);
        }

        [PunRPC]
        private void RPCTakeDamage()
        {
            health--;
            healthBar.UpdateView(health);
        }

        public bool IsDead()
        {
            if (Health > 0) return false;

            if (!photonView.IsMine) return true;
            gameObject.SetActive(false);
            _gameOverView.Activate(photonView.Owner);
            return true;
        }
    }
}