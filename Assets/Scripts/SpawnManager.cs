using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private Vector3 spawnPos = new Vector3(30, 0, 0);
    private PlayerController playerController;
    private float startDelay = 2;
    private float repeatRate = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Spawn obstacale at spawnPos
    private void SpawnObstacle()
    {
        // Stop obstacle spawning on gameOver
        if(!playerController.gameOver)
        {
            Instantiate(obstaclePrefab, spawnPos, gameObject.transform.rotation);
        }
    }
}
