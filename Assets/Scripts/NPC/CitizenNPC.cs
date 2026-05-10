using UnityEngine;
using DragonSeal.Data;

namespace DragonSeal.NPC
{
    public class CitizenNPC : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] protected float moveSpeed = 2f;
        protected Vector3 _targetPosition;
        protected bool _isMoving = false;
        protected Animator _animator;

        public CitizenSO CitizenData { get; private set; }

        public static Vector3 EntryPoint = new Vector3(-8f, 0f, 0f);
        public static Vector3 WindowPoint = new Vector3(0f, 0f, 0f);
        public static Vector3 ExitPoint = new Vector3(8f, 0f, 0f);

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public virtual void Initialize(CitizenSO data)
        {
            CitizenData = data;
            transform.position = EntryPoint;
            WalkToWindow();
        }

        // movement
        public void WalkToWindow()
        {
            _targetPosition = WindowPoint;
            _isMoving = true;
            SetWalkAnimation(true);
        }

        public void WalkOut()
        {
            _targetPosition = ExitPoint;
            _isMoving = true;
            SetWalkAnimation(true);
        }

        protected virtual void Update()
        {
            if (!_isMoving) return;

            // move to target
            transform.position = Vector3.MoveTowards(
                transform.position,
                _targetPosition,
                moveSpeed * Time.deltaTime
            );

            // see if target reached
            if (Vector3.Distance(transform.position, _targetPosition) < 0.05f)
            {
                transform.position = _targetPosition;
                _isMoving = false;
                SetWalkAnimation(false);
                OnReachedTarget();
            }
        }

        // when  reaches the target? 
        protected virtual void OnReachedTarget()
        {
            // If reached window, tell inspection manager 
            if (_targetPosition == WindowPoint)
                OnArrivedAtWindow();

            // If reached exit, destroy
            if (_targetPosition == ExitPoint)
                Destroy(gameObject);
        }

        protected virtual void OnArrivedAtWindow()
        {
            Debug.Log($"{CitizenData.citizenName} has arrived at the window.");
        }

        // animation
        protected virtual void SetWalkAnimation(bool isWalking)
        {
            if (_animator == null) return;
            _animator.SetBool("IsWalking", isWalking);
        }

        public virtual void PlaySurprisedAnimation()
        {
            if (_animator == null) return;
            _animator.SetTrigger("Surprised");
        }

        public virtual void PlayPleadAnimation()
        {
            if (_animator == null) return;
            _animator.SetTrigger("Plead");
        }

    }
}