using Core.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("Page Reference")]
    public UIEnum descriptionPageId;
   
    private UIPage _uiPage;

    private void Awake()
    {
        _uiPage = GetComponent<UIPage>();
    }

    public void OpenDescription()
    {
        var pageData = new PageData();
        pageData.Add("description", "");

        _uiPage.OpenPage(descriptionPageId, pageData);
    }
}
