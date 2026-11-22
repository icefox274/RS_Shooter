using UnityEngine;

public class LevelGen : MonoBehaviour
{
    public static LevelGen Instance;
    [SerializeField] private GameObject[] LevelObjects;
    [SerializeField] private GameObject Level;
    [SerializeField] private Transform[] ObjSpawn;
    [SerializeField] private Transform LevelGenerator;
    [SerializeField] private float SPawnInterval;

    private float timer;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {

        timer += Time.deltaTime;

        if(timer>SPawnInterval)
        {
            int N = Random.Range(0,LevelObjects.Length);
            int M = Random.Range(0, ObjSpawn.Length);

            Instantiate(LevelObjects[N], ObjSpawn[M]);

            timer = 0f;
        }
    }

    public void SpawnStrcture()
    {
        Instantiate(Level,LevelGenerator);
    }
}
