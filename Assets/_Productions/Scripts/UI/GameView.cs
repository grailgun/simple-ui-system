using Core;
using Core.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    private UIPage _uiPage;
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = SceneServiceProvider.GetService<GameManager>();
        _gameManager.OnScoreChange.AddListener(UpdateScore);
        scoreText.SetText($"0");

        _uiPage = GetComponent<UIPage>();
        _uiPage.OnOn.AddListener(OnPageShow);
    }

    private void OnPageShow()
    {
        
    }

    private void UpdateScore(int value)
    {
        scoreText.SetText($"{value}");
    }
}
