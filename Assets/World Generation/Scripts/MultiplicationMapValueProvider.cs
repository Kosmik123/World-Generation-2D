using UnityEngine;

namespace WorldGeneration2D
{
    [CreateAssetMenu]
    public class MultiplicationMapValueProvider : MapValueProvider
    {
        [SerializeField]
        private MapValueProvider[] mapValueProviders;

        public override float GetValue(float x, float y)
        {
            float value = 1;
            foreach(var mapValueProvider in mapValueProviders)
                value *= mapValueProvider.GetValue(x, y);

            return value;
        }
    }
}
