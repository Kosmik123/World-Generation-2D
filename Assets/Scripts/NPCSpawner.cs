using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] npcsPrototypes;

    [SerializeField, Range(0, 1)]
    private float spawnProbability = 0.02f;

    private void Start()
    {
        if (Random.value < spawnProbability)
        {
            int npcsCount = Random.Range(1, 4);
            for (int i = 0; i < npcsCount; i++)
            {
                SpawnNPC();
            }
        }
    }

    private void SpawnNPC()
    {
        var npcPrototype = npcsPrototypes[Random.Range(0, npcsPrototypes.Length)];
        var npc = Instantiate(npcPrototype, transform);
    }
}
