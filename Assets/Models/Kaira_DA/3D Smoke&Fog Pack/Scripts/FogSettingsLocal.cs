using UnityEngine;
namespace KairaDigitalArts
{
    public class FogSettingsLocal : MonoBehaviour
    {
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
    }
}
