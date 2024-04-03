using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterControllerScript : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Terrain terrain;

    
    // Start is called before the first frame update
    private void Start()
    {
        RandomLocation();
    }
    
    private void RandomLocation()
    {
        var terrainData = terrain.terrainData;
        
        var terrainWidth = terrainData.size.x;
        var terrainLength = terrainData.size.z;
        
        var rx = Random.Range(0, terrainWidth);
        var rz = Random.Range(0, terrainLength);
        var targetPosition = new Vector3(rx, transform.position.y, rz);
        agent.SetDestination(targetPosition);
    }

    // Update is called once per frame
    private void Update()
    {
        if (agent.pathPending || agent.remainingDistance < 0.5)
        {
            RandomLocation();
        }
    }
}
