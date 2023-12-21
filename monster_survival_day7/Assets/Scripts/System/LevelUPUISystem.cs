using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUPUISystem
{
    private GameEvent gameEvent;
    private GameObject playerObject;
    private List<LevelUPUIComponent> levelUPUIComponentList = new List<LevelUPUIComponent>();

    public LevelUPUISystem(GameEvent gameEvent, GameObject playerObject)
    {
        this.gameEvent = gameEvent;
        this.playerObject = playerObject;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < levelUPUIComponentList.Count; i++)
        {
            LevelUPUIComponent levelUPUIComponent = levelUPUIComponentList[i];
            LevelUPComponent levelUPComponent = playerObject.GetComponent<LevelUPComponent>();

            if (levelUPComponent.IsLevelUp && !levelUPUIComponent.gameObject.activeSelf)
            {
                levelUPUIComponent.gameObject.SetActive(true);
                ButtonShuffle(levelUPUIComponent);
            }
            else if (!levelUPComponent.IsLevelUp && levelUPUIComponent.gameObject.activeSelf)
            {
                levelUPUIComponent.gameObject.SetActive(false);
            }
        }
    }

    private void ButtonShuffle(LevelUPUIComponent levelUPUIComponent)
    {
        List<int> indexList = new List<int>();
        for (int i = 0; i < levelUPUIComponent.LevelUpKindCount; i++)
        {
            int temp = Random.Range(0, levelUPUIComponent.LevelUpKindCount);
            if (indexList.Contains(temp))
            {
                i--;
                continue;
            }
            indexList.Add(temp);
        }

        for (int i = 0; i < 3; i++)
        {
            levelUPUIComponent.LevelUPButtonList[i].onClick.RemoveAllListeners();

            switch (indexList[i])
            {
                case 0:
                    levelUPUIComponent.LevelUPButtonList[i].onClick.AddListener(OnClickAttackUPButton);
                    levelUPUIComponent.LevelUPButtonList[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Attack UP";
                    break;
                case 1:
                    levelUPUIComponent.LevelUPButtonList[i].onClick.AddListener(OnClickHitPointUPButton);
                    levelUPUIComponent.LevelUPButtonList[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "HitPoint UP";
                    break;
                case 2:
                    levelUPUIComponent.LevelUPButtonList[i].onClick.AddListener(OnClickAttackSpeedUPButton);
                    levelUPUIComponent.LevelUPButtonList[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "AttackSpeed UP";
                    break;
                case 3:
                    levelUPUIComponent.LevelUPButtonList[i].onClick.AddListener(OnSplitUPButton);
                    levelUPUIComponent.LevelUPButtonList[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Split UP";
                    break;
            }
        }
    }


    private void OnClickAttackUPButton()
    {
        LevelUPComponent levelUPComponent = playerObject.GetComponent<LevelUPComponent>();
        if (levelUPComponent == null) return;

        levelUPComponent.AttackLevel++;
        gameEvent.LevelUP?.Invoke();
        levelUPComponent.IsLevelUp = false;
    }

    private void OnClickHitPointUPButton()
    {
        LevelUPComponent levelUPComponent = playerObject.GetComponent<LevelUPComponent>();
        if (levelUPComponent == null) return;

        levelUPComponent.HitPointLevel++;
        gameEvent.LevelUP?.Invoke();
        levelUPComponent.IsLevelUp = false;
    }

    private void OnClickAttackSpeedUPButton()
    {
        LevelUPComponent levelUPComponent = playerObject.GetComponent<LevelUPComponent>();
        if (levelUPComponent == null) return;

        levelUPComponent.AttackSpeedLevel++;
        gameEvent.LevelUP?.Invoke();
        levelUPComponent.IsLevelUp = false;
    }


    private void OnSplitUPButton()
    {
        LevelUPComponent levelUPComponent = playerObject.GetComponent<LevelUPComponent>();
        if (levelUPComponent == null) return;

        levelUPComponent.SplitLevel++;
        gameEvent.LevelUP?.Invoke();
        levelUPComponent.IsLevelUp = false;
    }


    private void AddComponentList(GameObject gameObject)
    {
        LevelUPUIComponent levelUPUIComponent = gameObject.GetComponent<LevelUPUIComponent>();

        if (levelUPUIComponent == null) return;

        levelUPUIComponentList.Add(levelUPUIComponent);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        LevelUPUIComponent levelUPUIComponent = gameObject.GetComponent<LevelUPUIComponent>();

        if (levelUPUIComponent == null) return;

        levelUPUIComponentList.Remove(levelUPUIComponent);
    }




}
