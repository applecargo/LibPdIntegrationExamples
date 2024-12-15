using UnityEngine;

namespace KairaDigitalArts
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 5f;     // Y�r�y�� h�z�
        public float mouseSensitivity = 2f; // Fare hassasiyeti

        private float verticalRotation = 0f;  // Y ekseni i�in bak�� a��s� s�n�r�
        public float lookUpLimit = 90f;  // Yukar� bakma s�n�r�
        public float lookDownLimit = -90f; // A�a�� bakma s�n�r�

        private CharacterController characterController;

        void Start()
        {
            // CharacterController bile�enini al
            characterController = GetComponent<CharacterController>();

            // Fare imlecini gizle ve ekrana kilitle
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            LookAround();   // Bak�nma fonksiyonunu �a��r
            Move();         // Hareket fonksiyonunu �a��r
        }

        void LookAround()
        {
            // Fare hareketini al
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            // Karakteri yatay eksende d�nd�r (yani sa�a/sola bakma)
            transform.Rotate(Vector3.up * mouseX);

            // Yukar�/a�a�� bak��� s�n�rlamak i�in dikey d�n��� manuel ayarla
            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, lookDownLimit, lookUpLimit);

            // Kamera yukar�/a�a�� bakacak �ekilde d�nd�r
            Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }

        void Move()
        {
            // �leri/geri ve sa�a/sola hareket girdilerini al
            float moveX = Input.GetAxis("Horizontal");  // A/D veya Sol/sa� ok tu�lar�
            float moveZ = Input.GetAxis("Vertical");    // W/S veya Yukar�/a�a�� ok tu�lar�

            // Hareket y�n�n� hesapla
            Vector3 move = transform.right * moveX + transform.forward * moveZ;

            // Karakteri hareket ettir
            characterController.Move(move * moveSpeed * Time.deltaTime);
        }
    }
}
