using UnityEngine;
using WorldGeneration2D;

[RequireComponent(typeof(SpriteRenderer))]
public class MapVisualize : MonoBehaviour
{
    [SerializeField]
    private GenerationSettings generationSettings;

    private SpriteRenderer _spriteRenderer;
    public SpriteRenderer SpriteRenderer
    {
        get
        {
            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();
            return _spriteRenderer;
        }
    }

    private Texture2D visualizationTexture;

    private const int resolution = 100;

    private void Start()
    {
        CreateVisualization();
        SpriteRenderer.sprite = Sprite.Create(visualizationTexture, new Rect(0,0, resolution, resolution), new Vector2(0.5f, 0.5f));
    }

    private void CreateVisualization()
    {
        transform.localScale = 10 * Vector3.one;

        var position = transform.position;
        visualizationTexture = new Texture2D(resolution, resolution);
        var pixels = new Color32[resolution * resolution];
        for (int j = 0; j < resolution; j++)
        {
            for (int i = 0; i < resolution; i++)
            {
                var x = position.x + i / 10f;
                var y = position.y + j / 10f;
                float value = generationSettings.GetValue(x, y);
                pixels[j * resolution + i] = Color.Lerp(Color.black, Color.white, value);
            }
        }
        visualizationTexture.SetPixels32(pixels);
        visualizationTexture.Apply();
    }
}
