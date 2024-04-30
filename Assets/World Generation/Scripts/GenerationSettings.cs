using UnityEngine;

namespace WorldGeneration2D
{
    [CreateAssetMenu]
    public class GenerationSettings : ScriptableObject
    {
        [SerializeField]
        private Vector2 perlinOffset;
        [SerializeField]
        private Vector2 perlinScale;

        private void Reset()
        {
            perlinScale = Vector2.one;
        }

        public float GetValue(float x, float y)
        {
            float xReal = x / perlinScale.x + perlinOffset.x;
            float yReal = y / perlinScale.y + perlinOffset.y;

            return Mathf.PerlinNoise(xReal, yReal);
        }
    }
}
