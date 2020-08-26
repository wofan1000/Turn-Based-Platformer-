using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public static CheckpointController instance;

    private CheckPoint[] checkPoints;

    public Vector3 spawnPoints;
    // Start is called before the first frame update

    public void Awake()
    {
        instance = this;
    }
    void Start()
    {
        checkPoints = FindObjectsOfType<CheckPoint>();
        spawnPoints = PlayerController.instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeactivateCheckpoints()
    {
        for(int i = 0; i < checkPoints.Length; i++)
        {
            checkPoints[i].ResetCheckPoint();
        }
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoints = newSpawnPoint;
    }
}
