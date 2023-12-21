using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    private GameEvent gameEvent;
    private CharacterMoveSystem characterMoveSystem;

    private PlayerInputSystem playerInputSystem;
    private PlayerAttackSystem playerAttackSystem;

    void Start()
    {
        gameEvent = new GameEvent();

        GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        characterMoveSystem = new CharacterMoveSystem(gameEvent);

        playerInputSystem = new PlayerInputSystem(gameEvent);
        playerAttackSystem = new PlayerAttackSystem(gameEvent);

        gameEvent.AddComponentList?.Invoke(player);
    }

    void Update()
    {
        playerInputSystem.OnUpdate();
        playerAttackSystem.OnUpdate();
        characterMoveSystem.OnUpdate();
    }
}
