using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnerSystem
{
    private GameEvent gameEvent;
    private ObjectPool objectPool;
    private GameObject playerObject;
    private List<EnemySpawnerComponent> enemySpawnerComponentList = new List<EnemySpawnerComponent>();

    public EnemySpawnerSystem(GameEvent gameEvent, GameObject player, ObjectPool objectPool)
    {
        this.gameEvent = gameEvent;
        this.playerObject = player;
        this.objectPool = objectPool;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    private void Initialize(EnemySpawnerComponent enemySpawnerComponent)
    {
        enemySpawnerComponent.ScreenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10.0f));
    }

    public void OnUpdate()
    {
        for (int i = 0; i < enemySpawnerComponentList.Count; i++)
        {
            EnemySpawnerComponent enemySpawnerComponent = enemySpawnerComponentList[i];
            if (!enemySpawnerComponent.gameObject.activeSelf) continue;

            if (enemySpawnerComponent.SpawnTimer < enemySpawnerComponent.SpawnInterval)
            {
                enemySpawnerComponent.SpawnTimer += Time.deltaTime;
                continue;
            }
            Spawn(enemySpawnerComponent);
            enemySpawnerComponent.SpawnTimer = 0.0f;
        }

        List<GameObject> tempEnemyList = objectPool.GetGameObjectList(enemySpawnerComponentList[0].EnemyPrefab);
        if (tempEnemyList == null) return;
        for (int i = 0; i < tempEnemyList.Count; i++)
        {
            if (!tempEnemyList[i].activeSelf) continue;
            tempEnemyList[i].GetComponent<CharacterMoveComponent>().TargetPosition = playerObject.transform.position;
        }
    }

    private void Spawn(EnemySpawnerComponent enemySpawnerComponent)
    {
        GameObject tempObject = objectPool.GetGameObject(enemySpawnerComponent.EnemyPrefab);
        Vector3 tempPos = new Vector3(Random.RandomRange(enemySpawnerComponent.ScreenSize.x, enemySpawnerComponent.ScreenSize.x + enemySpawnerComponent.PositionOffset.x), 0.0f, Random.RandomRange(enemySpawnerComponent.ScreenSize.y, enemySpawnerComponent.ScreenSize.y + enemySpawnerComponent.PositionOffset.z));
        tempPos *= Random.RandomRange(0.0f, 1.0f) > 0.5f ? 1 : -1;
        tempObject.transform.position = playerObject.transform.position + tempPos;
        tempObject.GetComponent<CharacterMoveComponent>().Direction = playerObject.transform.position - tempObject.transform.position;
        tempObject.GetComponent<CharacterBaseComponent>().HitPoint = tempObject.GetComponent<CharacterBaseComponent>().MaxHitPoint;
        if (!objectPool.IsNewGenerate) return;
        gameEvent.AddComponentList?.Invoke(tempObject);
        objectPool.IsNewGenerate = false;
    }

    private void AddComponentList(GameObject gameObject)
    {
        EnemySpawnerComponent enemySpawnerComponent = gameObject.GetComponent<EnemySpawnerComponent>();

        if (enemySpawnerComponent == null) return;

        enemySpawnerComponentList.Add(enemySpawnerComponent);

        Initialize(enemySpawnerComponent);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        EnemySpawnerComponent enemySpawnerComponent = gameObject.GetComponent<EnemySpawnerComponent>();

        if (enemySpawnerComponent == null) return;

        enemySpawnerComponentList.Remove(enemySpawnerComponent);
    }
}
