using Core;
using Core.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("Page Reference")]
    public EnumId descriptionPageId;

    private GameManager _gameManager;

    private UIPage _uiPage;

    private void Awake()
    {
        _uiPage = GetComponent<UIPage>();
        _gameManager = SceneServiceProvider.GetService<GameManager>();
    }

    public void OpenDescription()
    {
        var pageData = new PageData();
        pageData.Add("description", "This is popup");

        _uiPage.OpenPage(descriptionPageId, pageData);
    }
}
