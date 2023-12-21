using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerComponent : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyRoot;
    [SerializeField] private float spawnInterval;
    private float spawnTimer;
    private Vector3 screenSize;
    [SerializeField] private Vector3 positionOffset;

    public GameObject EnemyPrefab { get => enemyPrefab; set => enemyPrefab = value; }
    public GameObject EnemyRoot { get => enemyRoot; set => enemyRoot = value; }
    public float SpawnInterval { get => spawnInterval; set => spawnInterval = value; }
    public float SpawnTimer { get => spawnTimer; set => spawnTimer = value; }
    public Vector3 ScreenSize { get => screenSize; set => screenSize = value; }
    public Vector3 PositionOffset { get => positionOffset; set => positionOffset = value; }
}
