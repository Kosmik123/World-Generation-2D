using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace WorldGeneration2D
{
    [CreateAssetMenu]
    public class PerlinMapValueProvider : MapValueProvider
    {
        public static AnimationCurve LinearCurve => new AnimationCurve(new Keyframe(0, 0, 0.5f, 0.5f), new Keyframe(1, 1, 0.5f, 0.5f));

        [SerializeField]
        private Octave[] octaves;
        [SerializeField]
        private Vector2 noiseScale;
        [SerializeField]
        private AnimationCurve curve = LinearCurve;

        private void Reset()
        {
            noiseScale = Vector2.one;
        }

        public override float GetValue(float x, float y) => GetValue(x, y, noiseScale, octaves, curve);

        public static float GetValue(float x, float y, Vector2 noiseScale, IReadOnlyList<Octave> octaves, AnimationCurve curve)
        {
            float noiseValue = 0;
            Vector2 positionScale = noiseScale;
            float valueScale = 1;

            int octavesCount = octaves.Count;
            for (int i = 0; i < octavesCount; i++)
            {
                var octave = octaves[i];
                float xReal = x / positionScale.x + octave.Offset.x;
                float yReal = y / positionScale.y + octave.Offset.y;
                noiseValue += valueScale * Mathf.PerlinNoise(xReal, yReal);

                valueScale /= 2;
                positionScale /= 2;
            }

            float maxPossibleValue = 2 - 1f / (1 << (octavesCount - 1));
            return curve.Evaluate(noiseValue / maxPossibleValue);
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
