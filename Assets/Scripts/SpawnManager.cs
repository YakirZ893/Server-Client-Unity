﻿using UnityEngine;
using Mirror;


public class SpawnManager : NetworkBehaviour
{
    [SerializeField] private ServerData serverData;
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private float spawnOffset;
    private GameObject obstacleToSpawn;
    private int _randomSpawnIndex = 0;
    private GameObject CarRef;


    public void SpawnObstacle()
    {
        CarRef = serverData.GetPlayer();
        obstacleToSpawn = obstacles[_randomSpawnIndex];
        _randomSpawnIndex++;
        if (_randomSpawnIndex == obstacles.Length)
        {
            _randomSpawnIndex = 0;
        }
        NetworkClient.RegisterPrefab(obstacleToSpawn);
        obstacleToSpawn = Instantiate(obstacleToSpawn, CarRef.transform.position + (CarRef.transform.forward * spawnOffset), obstacleToSpawn.transform.rotation);

    }

    [ClientRpc]
    public void RpcSpawnObstacleOnClient()
    {
        SpawnObstacle();
    }

}