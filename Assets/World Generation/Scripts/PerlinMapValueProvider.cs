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

    [CustomEditor(typeof(PerlinMapValueProvider))]
    public class GenerationSettingsEditor : Editor
    {
        private const int resolution = 100;

        private Texture2D _previewTexture;
        public Texture2D PreviewTexture
        {
            get
            {
                if (_previewTexture == null)
                    _previewTexture = CreateTexture();
                return _previewTexture;
            }
        }

        private static Texture2D CreateTexture()
        {
            return new Texture2D(resolution, resolution, TextureFormat.RGBA32, false, true)
            {
                wrapMode = TextureWrapMode.Clamp
            };
        }

        private readonly Color32[] colors = new Color32[resolution * resolution];

        public override bool HasPreviewGUI() => true;

        private void OnEnable()
        {
            PopulatePreviewTexture();
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            base.OnInspectorGUI();

            if (EditorGUI.EndChangeCheck() || _previewTexture == null)
            {
                PopulatePreviewTexture();
            }
        }

        private void PopulatePreviewTexture()
        {
            var generationSettings = target as MapValueProvider;
            float halfResolution = resolution / 2;
            for (int j = 0; j < resolution; j++)
            {
                for (int i = 0; i < resolution; i++)
                {
                    // one pixel in preview = one unit in world
                    var pixelValue = generationSettings.GetValue(i - halfResolution, j - halfResolution);
                    colors[j * resolution + i] = Color.Lerp(Color.black, Color.white, pixelValue);
                }
            }
            PreviewTexture.SetPixels32(colors);
            PreviewTexture.Apply();
        }

        public override void OnPreviewGUI(Rect r, GUIStyle background)
        {
            base.OnPreviewGUI(r, background);
            var textureRect = r;
            EditorGUI.DrawPreviewTexture(textureRect, PreviewTexture);
        }
    }
}
