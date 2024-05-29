using UnityEngine;
public class PipeGeneratorController : MonoBehaviour
{
    [SerializeField] private GameObject pipePrefab;
    [SerializeField] private GameObject upperPipePrefab;
    [SerializeField] private float distanceBetweenPipes;
    [SerializeField] private float gapSize;
    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;
    [SerializeField] private float spawnRate;
    private float timer;
    void Start()
    {
        GeneratePipe();
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            GeneratePipe();
            timer = 0;
        }
    }
    void GeneratePipe()
    {
        float centerPositionY = Random.Range(minHeight, maxHeight);
        GameObject upperPipe = Instantiate(upperPipePrefab, new Vector3(0, centerPositionY + gapSize / 2, 0), upperPipePrefab.transform.rotation);
        GameObject lowerPipe = Instantiate(pipePrefab, new Vector3(0, centerPositionY - gapSize / 2, 0), Quaternion.identity);
        Vector3 positionOffset = new Vector3(0, 0, Random.Range(-0.5f, 0.5f));
        upperPipe.transform.position = upperPipe.transform.position + positionOffset;
        lowerPipe.transform.position = lowerPipe.transform.position + positionOffset;
    }
}
