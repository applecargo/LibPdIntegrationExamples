using UnityEngine;

namespace PlayerGridBuilder
{
    /// <summary>
    /// A basic first-person player controller.
    /// This allows the player to move around while building.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        public CharacterController characterController;
        public float speed = 3;

        public Transform cameraHolder;
        public float mouseSensitivity = 2f;
        public float upLimit = -50;
        public float downLimit = 50;

        private readonly float gravity = 9.87f;
        private float verticalSpeed = 0;

        void Update()
        {
            Move();
            Rotate();
        }

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Rotate()
        {
            float horizontalRotation = Input.GetAxis("Mouse X");
            float verticalRotation = Input.GetAxis("Mouse Y");

            transform.Rotate(0, horizontalRotation * mouseSensitivity, 0);
            cameraHolder.Rotate(-verticalRotation * mouseSensitivity, 0, 0);

            Vector3 currentRotation = cameraHolder.localEulerAngles;
            if (currentRotation.x > 180) currentRotation.x -= 360;
            currentRotation.x = Mathf.Clamp(currentRotation.x, upLimit, downLimit);
            cameraHolder.localRotation = Quaternion.Euler(currentRotation);
        }

        private void Move()
        {
            float horizontalMove = Input.GetAxis("Horizontal");
            float verticalMove = Input.GetAxis("Vertical");

            if (characterController.isGrounded) verticalSpeed = 0;
            else verticalSpeed -= gravity * Time.deltaTime;
            Vector3 gravityMove = new Vector3(0, verticalSpeed, 0);

            Vector3 move = transform.forward * verticalMove + transform.right * horizontalMove;
            characterController.Move(speed * Time.deltaTime * move + gravityMove * Time.deltaTime);
        }
    }
}