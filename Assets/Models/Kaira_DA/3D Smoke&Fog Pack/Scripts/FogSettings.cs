using UnityEngine;
namespace KairaDigitalArts {
    public class FogSettings : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private Color fogColor;
        [SerializeField] private float windX;
        [SerializeField] private float windY;
        [SerializeField] private float windZ;

        private void Start()
        {
            ParticleSystem ps = GetComponent<ParticleSystem>();

            // Partik�l sisteminin ana mod�l�ne eri�im ve rengin ayarlanmas�
            ParticleSystem.MainModule mainModule = ps.main;
            mainModule.startColor = new ParticleSystem.MinMaxGradient(fogColor);

            // Velocity Over Lifetime mod�l�ne eri�im ve r�zgar ayarlar�n�n yap�lmas�
            ParticleSystem.VelocityOverLifetimeModule velocityModule = ps.velocityOverLifetime;

            // Wind direction settings
            velocityModule.x = new ParticleSystem.MinMaxCurve(windX);
            velocityModule.y = new ParticleSystem.MinMaxCurve(windY);
            velocityModule.z = new ParticleSystem.MinMaxCurve(windZ);
        }
        private void Update()
        {
            if (player == null)
            {
                player = GameObject.FindWithTag("Player");
                Debug.Log("Player object not assigned. Object with 'Player' tag automaticly assigned.");
            }
            transform.parent = player.transform;
        }
    }
}