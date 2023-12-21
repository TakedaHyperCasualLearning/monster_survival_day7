using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem
{
    private GameEvent gameEvent;
    private ObjectPool objectPool;
    private GameObject bulletRoot;
    private List<PlayerAttackComponent> playerAttackComponentList = new List<PlayerAttackComponent>();
    private List<CharacterBaseComponent> characterBaseComponentList = new List<CharacterBaseComponent>();
    private List<InputComponent> inputComponentList = new List<InputComponent>();

    public PlayerAttackSystem(GameEvent gameEvent, ObjectPool objectPool, GameObject bulletRoot)
    {
        this.gameEvent = gameEvent;
        this.objectPool = objectPool;
        this.bulletRoot = bulletRoot;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    private void Initialize(PlayerAttackComponent playerAttackComponent)
    {
        GameObject effect = GameObject.Instantiate(playerAttackComponent.ShotEffectPrefab, playerAttackComponent.transform);
        // effect.transform.SetParent(playerAttackComponent.transform);
        playerAttackComponent.ShotEffect = effect.GetComponent<ParticleSystem>();
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
        for (int i = 0; i < playerAttackComponent.Split; i++)
        {
            GameObject gameObject = objectPool.GetGameObject(playerAttackComponent.BulletPrefab);
            gameObject.transform.SetParent(this.bulletRoot.transform);
            gameObject.transform.position = playerAttackComponent.transform.position;
            Vector3 direction = new Vector3(0, 180 / (playerAttackComponent.Split + 1) * (i + 1) - 90, 0);
            Quaternion angleQuaternion = Quaternion.Euler(direction);
            gameObject.GetComponent<BulletMoveComponent>().Direction = angleQuaternion * playerAttackComponent.transform.forward;
            gameObject.GetComponent<BulletBaseComponent>().AttackPoint = playerAttackComponent.GetComponent<CharacterBaseComponent>().AttackPoint;
            if (!objectPool.IsNewGenerate) continue;
            gameEvent.AddComponentList?.Invoke(gameObject);
            objectPool.IsNewGenerate = false;
        }
        playerAttackComponent.ShotEffect.Play();
        playerAttackComponent.ShotEffect.transform.rotation = playerAttackComponent.transform.rotation;
        playerAttackComponent.ShotEffect.transform.position = playerAttackComponent.transform.position + new Vector3(0, 5, 0);
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

        Initialize(playerAttackComponent);
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
