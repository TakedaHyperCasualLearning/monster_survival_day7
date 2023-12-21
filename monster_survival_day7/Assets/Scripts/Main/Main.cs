using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemySpawner;
    [SerializeField] private GameObject UIRoot;

    private GameEvent gameEvent;
    private ObjectPool objectPool;
    private CharacterMoveSystem characterMoveSystem;

    private PlayerInputSystem playerInputSystem;
    private PlayerAttackSystem playerAttackSystem;

    private EnemySpawnerSystem enemySpawnerSystem;
    private EnemyAttackSystem enemyAttackSystem;

    private BulletMoveSystem bulletMoveSystem;
    private BulletHitSystem bulletHitSystem;

    private DamageSystem damageSystem;
    private HitPointUISystem hitPointUISystem;

    void Start()
    {
        gameEvent = new GameEvent();
        objectPool = new ObjectPool(gameEvent);

        GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        characterMoveSystem = new CharacterMoveSystem(gameEvent);

        playerInputSystem = new PlayerInputSystem(gameEvent);
        playerAttackSystem = new PlayerAttackSystem(gameEvent, objectPool);

        enemySpawnerSystem = new EnemySpawnerSystem(gameEvent, player, objectPool);
        enemyAttackSystem = new EnemyAttackSystem(gameEvent, player);

        bulletMoveSystem = new BulletMoveSystem(gameEvent, player);
        bulletHitSystem = new BulletHitSystem(gameEvent, objectPool, enemyPrefab);

        damageSystem = new DamageSystem(gameEvent);
        hitPointUISystem = new HitPointUISystem(gameEvent, UIRoot);

        gameEvent.AddComponentList?.Invoke(player);
        gameEvent.AddComponentList?.Invoke(enemySpawner);
    }

    void Update()
    {
        playerInputSystem.OnUpdate();
        playerAttackSystem.OnUpdate();
        characterMoveSystem.OnUpdate();
        enemySpawnerSystem.OnUpdate();
        enemyAttackSystem.OnUpdate();
        bulletMoveSystem.OnUpdate();
        bulletHitSystem.OnUpdate();
        damageSystem.OnUpdate();
        hitPointUISystem.OnUpdate();
    }
}
