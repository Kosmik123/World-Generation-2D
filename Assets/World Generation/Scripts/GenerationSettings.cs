using UnityEngine;

namespace WorldGeneration2D
{
    [CreateAssetMenu]
    public class GenerationSettings : ScriptableObject
    {
        [SerializeField]
        private Octave[] octaves;

        [SerializeField]
        private Vector2 noiseScale;

        private void Reset()
        {
            noiseScale = Vector2.one;
        }

        public float GetValue(float x, float y)
        {
            float noiseValue = 0;
            Vector2 positionScale = noiseScale;
            float valueScale = 1;

            int octavesCount = octaves.Length;
            for (int i = 0; i < octavesCount; i++)
            {
                float xReal = x / positionScale.x + octaves[i].Offset.x;
                float yReal = y / positionScale.y + octaves[i].Offset.y;
                noiseValue += valueScale * Mathf.PerlinNoise(xReal, yReal);

                valueScale /= 2;
                positionScale /= 2;
            }

            float maxPossibleValue = 2 - 1f / (1 << (octavesCount - 1));
            return noiseValue / maxPossibleValue;
        }
    }

    [System.Serializable]
    public struct Octave
    {
        [SerializeField]
        private Vector2 offset;
        public readonly Vector2 Offset => offset;
    }

}
