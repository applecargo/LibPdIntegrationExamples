using UnityEngine;

namespace KairaDigitalArts
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 5f;     // Yürüyüþ hýzý
        public float mouseSensitivity = 2f; // Fare hassasiyeti

        private float verticalRotation = 0f;  // Y ekseni için bakýþ açýsý sýnýrý
        public float lookUpLimit = 90f;  // Yukarý bakma sýnýrý
        public float lookDownLimit = -90f; // Aþaðý bakma sýnýrý

        private CharacterController characterController;

        void Start()
        {
            // CharacterController bileþenini al
            characterController = GetComponent<CharacterController>();

            // Fare imlecini gizle ve ekrana kilitle
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            LookAround();   // Bakýnma fonksiyonunu çaðýr
            Move();         // Hareket fonksiyonunu çaðýr
        }

        void LookAround()
        {
            // Fare hareketini al
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            // Karakteri yatay eksende döndür (yani saða/sola bakma)
            transform.Rotate(Vector3.up * mouseX);

            // Yukarý/aþaðý bakýþý sýnýrlamak için dikey dönüþü manuel ayarla
            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, lookDownLimit, lookUpLimit);

            // Kamera yukarý/aþaðý bakacak þekilde döndür
            Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }

        void Move()
        {
            // Ýleri/geri ve saða/sola hareket girdilerini al
            float moveX = Input.GetAxis("Horizontal");  // A/D veya Sol/sað ok tuþlarý
            float moveZ = Input.GetAxis("Vertical");    // W/S veya Yukarý/aþaðý ok tuþlarý

            // Hareket yönünü hesapla
            Vector3 move = transform.right * moveX + transform.forward * moveZ;

            // Karakteri hareket ettir
            characterController.Move(move * moveSpeed * Time.deltaTime);
        }
    }
}
