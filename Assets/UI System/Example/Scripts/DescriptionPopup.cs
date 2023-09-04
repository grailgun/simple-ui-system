using Core.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DescriptionPopup : MonoBehaviour
{
    [Header("Description Properties")]
    public TextMeshProUGUI descriptionText;

    private UIPage _uiPage;

    private void Awake()
    {
        _uiPage = GetComponent<UIPage>();
        _uiPage.OnPushed.AddListener(OnPopupPushed);
    }

    private void OnPopupPushed(PageData data)
    {
        Debug.Log(data == null);
        descriptionText.SetText(data.Get<string>("description"));
    }
}
