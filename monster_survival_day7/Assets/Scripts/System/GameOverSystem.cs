using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSystem
{
    private GameEvent gameEvent;
    private List<GameOverComponent> gameOverComponentList = new List<GameOverComponent>();

    public GameOverSystem(GameEvent gameEvent)
    {
        this.gameEvent = gameEvent;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
        gameEvent.GameOver += OnGameOver;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < gameOverComponentList.Count; i++)
        {
            GameOverComponent gameOverComponent = gameOverComponentList[i];

            if (!gameOverComponent.IsGameOver) continue;

            gameOverComponent.gameObject.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
            }
        }
    }

    private void AddComponentList(GameObject gameObject)
    {
        GameOverComponent gameOverComponent = gameObject.GetComponent<GameOverComponent>();

        if (gameOverComponent == null) return;

        gameOverComponentList.Add(gameOverComponent);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        GameOverComponent gameOverComponent = gameObject.GetComponent<GameOverComponent>();

        if (gameOverComponent == null) return;

        gameOverComponentList.Remove(gameOverComponent);
    }

    public void OnGameOver()
    {
        for (int i = 0; i < gameOverComponentList.Count; i++)
        {
            GameOverComponent gameOverComponent = gameOverComponentList[i];

            gameOverComponent.IsGameOver = true;
        }
    }
}
