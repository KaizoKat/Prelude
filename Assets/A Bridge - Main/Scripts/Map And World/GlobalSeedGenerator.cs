using UnityEngine;

public class GlobalSeedGenerator : MonoBehaviour
{
    int GlobalSeed;

    [SerializeField] DungeonGenerator[] generators;

    private void Start()
    {
        GlobalSeed = Random.Range(-int.MaxValue, int.MaxValue);

        foreach (var generator in generators)
            generator.seed = GlobalSeed;
    }
}
