using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitSystem
{
    private GameEvent gameEvent;
    private ObjectPool objectPool;
    private GameObject enemyPrefab;
    private List<ColliderComponent> colliderComponentList = new List<ColliderComponent>();
    private List<BulletBaseComponent> bulletBaseComponentList = new List<BulletBaseComponent>();

    public BulletHitSystem(GameEvent gameEvent, ObjectPool objectPool, GameObject enemyPrefab)
    {
        this.gameEvent = gameEvent;
        this.objectPool = objectPool;
        this.enemyPrefab = enemyPrefab;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < colliderComponentList.Count; i++)
        {
            ColliderComponent colliderComponent = colliderComponentList[i];
            BulletBaseComponent bulletBaseComponent = bulletBaseComponentList[i];
            if (!colliderComponent.gameObject.activeSelf) continue;

            List<GameObject> gameObjectList = objectPool.GetGameObjectList(enemyPrefab);
            if (gameObjectList == null) continue;
            for (int j = 0; j < gameObjectList.Count; j++)
            {
                ColliderComponent tempEnemy = gameObjectList[j].GetComponent<ColliderComponent>();
                if (!tempEnemy.gameObject.activeSelf) continue;

                if (Vector3.Distance(colliderComponent.transform.position, tempEnemy.transform.position) > colliderComponent.Radius + tempEnemy.Radius) continue;
                DamageComponent enemyDamageComponent = tempEnemy.gameObject.GetComponent<DamageComponent>();
                enemyDamageComponent.Damage = bulletBaseComponent.AttackPoint;
                enemyDamageComponent.IsDamage = true;
                gameEvent.ReleaseObject?.Invoke(bulletBaseComponent.gameObject);
            }
        }
    }

    private void AddComponentList(GameObject gameObject)
    {
        ColliderComponent colliderComponent = gameObject.GetComponent<ColliderComponent>();
        BulletBaseComponent bulletBaseComponent = gameObject.GetComponent<BulletBaseComponent>();

        if (colliderComponent == null || bulletBaseComponent == null) return;

        colliderComponentList.Add(colliderComponent);
        bulletBaseComponentList.Add(bulletBaseComponent);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        ColliderComponent colliderComponent = gameObject.GetComponent<ColliderComponent>();
        BulletBaseComponent bulletBaseComponent = gameObject.GetComponent<BulletBaseComponent>();

        if (colliderComponent == null || bulletBaseComponent == null) return;

        colliderComponentList.Remove(colliderComponent);
        bulletBaseComponentList.Remove(bulletBaseComponent);
    }
}
