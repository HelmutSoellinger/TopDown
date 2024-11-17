using UnityEngine;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM 
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class NPC_Controller : MonoBehaviour
    {
        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float Gravity = -15.0f;

        [Header("Player Grounded")]
        [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        public bool Grounded = true;

        [Tooltip("Useful for rough ground")]
        public float GroundedOffset = -0.14f;

        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius = 0.28f;

        [Tooltip("What layers the character uses as ground")]
        public LayerMask GroundLayers;

        private CharacterController _controller;
        private float _verticalVelocity;
        private Animator _animator;
        private bool _hasAnimator;

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
            _hasAnimator = TryGetComponent(out _animator);

            AssignAnimationIDs();
        }

        private void Update()
        {
            _hasAnimator = TryGetComponent(out _animator);
            JumpAndGravity();
            // Apply gravity and move the NPC based on the vertical velocity
            Vector3 move = new Vector3(0, _verticalVelocity * Time.deltaTime, 0);
            _controller.Move(move);
            GroundedCheck();

        }

        private void AssignAnimationIDs()
        {
            // Placeholder for animation IDs if needed
        }

        private void GroundedCheck()
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);

            // Reset vertical velocity if grounded
            if (Grounded && _verticalVelocity < 0)
            {
                _verticalVelocity = 0f;
            }
        }

        private void JumpAndGravity()
        {
            // Apply gravity over time if under terminal velocity
            if (_verticalVelocity > Gravity)
            {
                _verticalVelocity += Gravity * Time.deltaTime;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            Gizmos.color = Grounded ? transparentGreen : transparentRed;
            Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
        }
    }
}
