using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveSystem
{
    private GameEvent gameEvent;
    private List<CharacterMoveComponent> characterMoveComponentList = new List<CharacterMoveComponent>();

    public CharacterMoveSystem(GameEvent gameEvent)
    {
        this.gameEvent = gameEvent;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < characterMoveComponentList.Count; i++)
        {
            CharacterMoveComponent characterMoveComponent = characterMoveComponentList[i];
            if (!characterMoveComponent.gameObject.activeSelf) continue;

            if (characterMoveComponent.IsChase)
            {
                characterMoveComponent.Direction = Vector3.forward;
            }

            if (characterMoveComponent.IsLokAt)
            {
                characterMoveComponent.transform.LookAt(characterMoveComponent.TargetPosition);
            }

            characterMoveComponent.transform.Translate(characterMoveComponent.Direction * characterMoveComponent.MoveSpeed * Time.deltaTime, Space.Self);
        }
    }

    private void AddComponentList(GameObject gameObject)
    {
        CharacterMoveComponent characterMoveComponent = gameObject.GetComponent<CharacterMoveComponent>();

        if (characterMoveComponent == null) return;

        characterMoveComponentList.Add(characterMoveComponent);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        CharacterMoveComponent characterMoveComponent = gameObject.GetComponent<CharacterMoveComponent>();

        if (characterMoveComponent == null) return;

        characterMoveComponentList.Remove(characterMoveComponent);
    }
}
