using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveSystem
{
    private GameEvent gameEvent;
    private GameObject player;
    private List<BulletMoveComponent> bulletMoveComponentList = new List<BulletMoveComponent>();


    public BulletMoveSystem(GameEvent gameEvent, GameObject player)
    {
        this.gameEvent = gameEvent;
        this.player = player;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < bulletMoveComponentList.Count; i++)
        {
            BulletMoveComponent bulletMoveComponent = bulletMoveComponentList[i];
            if (!bulletMoveComponent.gameObject.activeSelf) continue;

            bulletMoveComponent.transform.Translate(bulletMoveComponent.Direction * bulletMoveComponent.Speed * Time.deltaTime, Space.Self);

            if (Vector3.Distance(bulletMoveComponent.transform.position, player.transform.position) < bulletMoveComponent.ReleaseRange) continue;
            gameEvent.ReleaseObject(bulletMoveComponent.gameObject);
        }
    }

    private void AddComponentList(GameObject gameObject)
    {
        BulletMoveComponent bulletMoveComponent = gameObject.GetComponent<BulletMoveComponent>();

        if (bulletMoveComponent == null) return;

        bulletMoveComponentList.Add(bulletMoveComponent);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        BulletMoveComponent bulletMoveComponent = gameObject.GetComponent<BulletMoveComponent>();

        if (bulletMoveComponent == null) return;

        bulletMoveComponentList.Remove(bulletMoveComponent);
    }
}
