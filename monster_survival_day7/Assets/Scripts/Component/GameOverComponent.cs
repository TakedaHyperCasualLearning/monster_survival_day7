using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverComponent : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    private bool isGameOver;

    public GameObject GameOverUI { get => gameOverUI; set => gameOverUI = value; }
    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }
}
