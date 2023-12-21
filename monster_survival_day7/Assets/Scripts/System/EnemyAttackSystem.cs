using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSystem : MonoBehaviour
{

    private GameEvent gameEvent;
    private GameObject playerObject;

    private List<DamageComponent> damageComponentList = new List<DamageComponent>();
    private List<CharacterBaseComponent> characterBaseComponentList = new List<CharacterBaseComponent>();
    private List<ColliderComponent> colliderComponentList = new List<ColliderComponent>();

    public EnemyAttackSystem(GameEvent gameEvent, GameObject playerObject)
    {
        this.gameEvent = gameEvent;
        this.playerObject = playerObject;

        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < damageComponentList.Count; i++)
        {
            DamageComponent damageComponent = damageComponentList[i];
            CharacterBaseComponent characterBaseComponent = characterBaseComponentList[i];
            ColliderComponent colliderComponent = colliderComponentList[i];
            if (!damageComponent.gameObject.activeSelf) continue;

            if (damageComponent.gameObject == playerObject) continue;
            DamageComponent playerDamageComponent = playerObject.GetComponent<DamageComponent>();
            CharacterBaseComponent playerCharacterBaseComponent = playerObject.GetComponent<CharacterBaseComponent>();
            ColliderComponent playerColliderComponent = playerObject.GetComponent<ColliderComponent>();

            if (Vector3.Distance(colliderComponent.transform.position, playerObject.transform.position) > colliderComponent.Radius + playerColliderComponent.Radius) continue;

            playerDamageComponent.Damage = characterBaseComponent.AttackPoint;
            Debug.Log(playerDamageComponent.Damage);
            playerDamageComponent.IsDamage = true;
        }
    }

    private void AddComponentList(GameObject gameObject)
    {
        DamageComponent damageComponent = gameObject.GetComponent<DamageComponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();
        ColliderComponent colliderComponent = gameObject.GetComponent<ColliderComponent>();

        if (damageComponent == null || characterBaseComponent == null || colliderComponent == null) return;

        damageComponentList.Add(damageComponent);
        characterBaseComponentList.Add(characterBaseComponent);
        colliderComponentList.Add(colliderComponent);
    }


    private void RemoveComponentList(GameObject gameObject)
    {
        DamageComponent damageComponent = gameObject.GetComponent<DamageComponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();
        ColliderComponent colliderComponent = gameObject.GetComponent<ColliderComponent>();

        if (damageComponent == null || characterBaseComponent == null || colliderComponent == null) return;

        damageComponentList.Remove(damageComponent);
        characterBaseComponentList.Remove(characterBaseComponent);
        colliderComponentList.Remove(colliderComponent);
    }
}
