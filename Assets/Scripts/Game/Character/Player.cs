using UnityEngine;
using Photon.Pun;

namespace Game.Character
{
    public class Player : MonoBehaviour, ICharacter, IHealth
    {
        [SerializeField] private float speed;
        [SerializeField] private byte health;
        [SerializeField] private Animator anim;
        private readonly int isRunning = Animator.StringToHash("isRunning");
        private PhotonView view;
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
            view = GetComponent<PhotonView>();
        }

        public void Update()
        {
            if (!view.IsMine) return;
            
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Move(moveInput);
        }

        public void Move(Vector2 moveInput)
        {
            Vector2 moveAmount = moveInput.normalized * Speed * Time.deltaTime;
            transform.position += (Vector3) moveAmount;
            anim.SetBool(isRunning, moveInput != Vector2.zero);
        }
        
        public void Shoot()
        {
            throw new System.NotImplementedException();
        }
        

        public bool TakeDamage()
        {
            if (health <= 0) return false;
                health--;
            return health <= 0;
        }

        public void DoDamage()
        {
            throw new System.NotImplementedException();
        }
    }
}