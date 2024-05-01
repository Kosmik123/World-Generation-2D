using UnityEditor;
using UnityEngine;

namespace WorldGeneration2D
{
    [CustomEditor(typeof(BiomesTerrainGenerator))]
    public class ChunkTerrainGeneratorEditor : Editor
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

        private readonly Color32[] colors = new Color32[resolution * resolution];

        private void OnEnable()
        {
            PopulatePreviewTexture();
        }

        private static Texture2D CreateTexture()
        {
            return new Texture2D(resolution, resolution, TextureFormat.RGBA32, false, true)
            {
                wrapMode = TextureWrapMode.Clamp
            };
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
            var biomeGenerator = target as BiomesTerrainGenerator;
            float halfResolution = resolution / 2;
            for (int j = 0; j < resolution; j++)
            {
                for (int i = 0; i < resolution; i++)
                {
                    // one pixel in preview = one unit in world
                    var climate = biomeGenerator.GetClimateData(i - halfResolution, j - halfResolution);
                    colors[j * resolution + i] = new Color(climate.temperature, (climate.humidity + climate.temperature) / 2, climate.humidity);
                }
            }
            PreviewTexture.SetPixels32(colors);
            PreviewTexture.Apply();
        }

        public override bool HasPreviewGUI() => true;

        public override void OnPreviewGUI(Rect r, GUIStyle background)
        {
            base.OnPreviewGUI(r, background);
            var textureRect = r;
            EditorGUI.DrawPreviewTexture(textureRect, PreviewTexture);
        }
    }
}
