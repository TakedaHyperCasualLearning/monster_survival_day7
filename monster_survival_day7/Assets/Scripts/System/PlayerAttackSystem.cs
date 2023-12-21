using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem
{
    private GameEvent gameEvent;
    private ObjectPool objectPool;
    private List<PlayerAttackComponent> playerAttackComponentList = new List<PlayerAttackComponent>();
    private List<CharacterBaseComponent> characterBaseComponentList = new List<CharacterBaseComponent>();
    private List<InputComponent> inputComponentList = new List<InputComponent>();

    public PlayerAttackSystem(GameEvent gameEvent, ObjectPool objectPool)
    {
        this.gameEvent = gameEvent;
        this.objectPool = objectPool;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < playerAttackComponentList.Count; i++)
        {
            PlayerAttackComponent playerAttackComponent = playerAttackComponentList[i];
            if (!playerAttackComponent.gameObject.activeSelf) continue;

            if (playerAttackComponent.IntervalTImer < playerAttackComponent.AttackInterval)
            {
                playerAttackComponent.IntervalTImer += Time.deltaTime;
                continue;
            }

            if (!inputComponentList[i].IsClick) continue;
            ShotAction(playerAttackComponent);
            playerAttackComponent.IntervalTImer = 0.0f;
        }
    }

    private void ShotAction(PlayerAttackComponent playerAttackComponent)
    {
        GameObject gameObject = objectPool.GetGameObject(playerAttackComponent.BulletPrefab);
        gameObject.transform.position = playerAttackComponent.transform.position;
        gameObject.GetComponent<BulletMoveComponent>().Direction = playerAttackComponent.transform.forward;
        gameObject.GetComponent<BulletBaseComponent>().AttackPoint = playerAttackComponent.GetComponent<CharacterBaseComponent>().AttackPoint;
        if (!objectPool.IsNewGenerate) return;
        gameEvent.AddComponentList?.Invoke(gameObject);
        objectPool.IsNewGenerate = false;
    }

    private void AddComponentList(GameObject gameObject)
    {
        PlayerAttackComponent playerAttackComponent = gameObject.GetComponent<PlayerAttackComponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();
        InputComponent inputComponent = gameObject.GetComponent<InputComponent>();

        if (playerAttackComponent == null || characterBaseComponent == null || inputComponent == null) return;

        playerAttackComponentList.Add(playerAttackComponent);
        characterBaseComponentList.Add(characterBaseComponent);
        inputComponentList.Add(inputComponent);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        PlayerAttackComponent playerAttackComponent = gameObject.GetComponent<PlayerAttackComponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();
        InputComponent inputComponent = gameObject.GetComponent<InputComponent>();

        if (playerAttackComponent == null || characterBaseComponent == null || inputComponent == null) return;

        playerAttackComponentList.Remove(playerAttackComponent);
        characterBaseComponentList.Remove(characterBaseComponent);
        inputComponentList.Remove(inputComponent);
    }
}
