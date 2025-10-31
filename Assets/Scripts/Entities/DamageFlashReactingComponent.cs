using UnityEngine;

namespace Entities
{
    public class DamageFlashReactingComponent : MonoBehaviour
    {
        [SerializeField] private Renderer[] targets;
        [SerializeField] private Color flashColor = Color.red;
        [SerializeField] private float flashDuration = 0.15f;

        private MaterialPropertyBlock _mpb;
        private float _timer;
        private static readonly int ColorID = Shader.PropertyToID("_BaseColor");
        
        private void Awake()
        {
            _mpb = new MaterialPropertyBlock();
            
            if (TryGetComponent(out IDamageable damageFlashReacting))
                damageFlashReacting.OnTakeDamage += Flash;
            else
                Debug.LogError($"Could not find IDamageable in Component {gameObject.name}");
        }

        private void Update()
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
                var t = _timer / flashDuration;

                foreach (var targetRenderer in targets)
                {
                    targetRenderer.GetPropertyBlock(_mpb);
                    _mpb.SetColor(ColorID, Color.Lerp(Color.gray, flashColor, t));
                    targetRenderer.SetPropertyBlock(_mpb);
                }
            }
        }

        private void Flash(float damage)
        {
            _timer = flashDuration;
        }

        private void OnDestroy()
        {
            if (TryGetComponent(out IDamageable damageFlashReacting))
                damageFlashReacting.OnTakeDamage -= Flash;
        }
    }
}