using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem
{
    private GameEvent gameEvent;
    private List<PlayerAttackComponent> playerAttackComponentList = new List<PlayerAttackComponent>();
    private List<CharacterBaseComponent> characterBaseComponentList = new List<CharacterBaseComponent>();
    private List<InputComponent> inputComponentList = new List<InputComponent>();

    public PlayerAttackSystem(GameEvent gameEvent)
    {
        this.gameEvent = gameEvent;
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
            ShotAction();
            playerAttackComponent.IntervalTImer = 0.0f;
        }
    }

    private void ShotAction()
    {
        Debug.Log("ShotAction");
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