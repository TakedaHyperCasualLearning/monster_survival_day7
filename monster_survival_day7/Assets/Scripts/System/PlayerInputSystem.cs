using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputSystem
{
    private GameEvent gameEvent;
    private List<InputComponent> inputComponentList = new List<InputComponent>();
    private List<CharacterMoveComponent> characterMoveComponentList = new List<CharacterMoveComponent>();

    public PlayerInputSystem(GameEvent gameEvent)
    {
        this.gameEvent = gameEvent;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < inputComponentList.Count; i++)
        {
            InputComponent inputComponent = inputComponentList[i];
            CharacterMoveComponent characterMoveComponent = characterMoveComponentList[i];
            if (!inputComponent.gameObject.activeSelf) continue;

            ClickInput(inputComponent);
            MoveInput(characterMoveComponent);
            TargetInput(characterMoveComponent);
        }
    }

    private void MoveInput(CharacterMoveComponent characterMoveComponent)
    {
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) direction += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) direction += Vector3.back;
        if (Input.GetKey(KeyCode.A)) direction += Vector3.left;
        if (Input.GetKey(KeyCode.D)) direction += Vector3.right;

        characterMoveComponent.Direction = direction.normalized;
    }

    private void TargetInput(CharacterMoveComponent characterMoveComponent)
    {
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(characterMoveComponent.gameObject.transform.position);
        Vector3 targetPosition = (Input.mousePosition - playerPosition).normalized;
        targetPosition.z = 0.0f;
        targetPosition = Camera.main.ScreenToWorldPoint(playerPosition + targetPosition);
        characterMoveComponent.TargetPosition = targetPosition;
    }

    private void ClickInput(InputComponent inputComponent)
    {
        if (Input.GetMouseButton(0))
        {
            inputComponent.IsClick = true;
            return;
        }

        inputComponent.IsClick = false;
    }

    private void AddComponentList(GameObject gameObject)
    {
        InputComponent inputComponent = gameObject.GetComponent<InputComponent>();
        CharacterMoveComponent characterMoveComponent = gameObject.GetComponent<CharacterMoveComponent>();

        if (inputComponent == null || characterMoveComponent == null) return;

        inputComponentList.Add(inputComponent);
        characterMoveComponentList.Add(characterMoveComponent);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        InputComponent inputComponent = gameObject.GetComponent<InputComponent>();
        CharacterMoveComponent characterMoveComponent = gameObject.GetComponent<CharacterMoveComponent>();

        if (inputComponent == null || characterMoveComponent == null) return;

        inputComponentList.Remove(inputComponent);
        characterMoveComponentList.Remove(characterMoveComponent);
    }

}
