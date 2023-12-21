using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemySpawner;

    private GameEvent gameEvent;
    private ObjectPool objectPool;
    private CharacterMoveSystem characterMoveSystem;

    private PlayerInputSystem playerInputSystem;
    private PlayerAttackSystem playerAttackSystem;

    private EnemySpawnerSystem enemySpawnerSystem;

    void Start()
    {
        gameEvent = new GameEvent();
        objectPool = new ObjectPool(gameEvent);

        GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        characterMoveSystem = new CharacterMoveSystem(gameEvent);

        playerInputSystem = new PlayerInputSystem(gameEvent);
        playerAttackSystem = new PlayerAttackSystem(gameEvent);

        enemySpawnerSystem = new EnemySpawnerSystem(gameEvent, player, objectPool);

        gameEvent.AddComponentList?.Invoke(player);
        gameEvent.AddComponentList?.Invoke(enemySpawner);
    }

    void Update()
    {
        playerInputSystem.OnUpdate();
        playerAttackSystem.OnUpdate();
        characterMoveSystem.OnUpdate();
        enemySpawnerSystem.OnUpdate();
    }
}
