using Lean.Gui;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Core.UI
{
    public class UIPage : MonoBehaviour
    {
        public enum ClosePageType
        {
            CanvasGraphic = 0,
            GameObject = 1,
        }

        public EnumId PageID => pageId;
        public PageData PageData => _pageData;
        public SceneUI SceneUI => _sceneUI;
        public virtual bool DisablePreviousPage => disablePreviousPage;

        [Header("UI ID")]
        [SerializeField] private EnumId pageId;

        [Header("Page Setting")]
        [SerializeField] private bool disablePreviousPage = false;
        [SerializeField] private ClosePageType offType;

        [Header("Events Hook")]
        public UnityEvent<PageData> OnPushed;
        public UnityEvent OnOpen;
        public UnityEvent OnClose;

        private SceneUI _sceneUI;
        private PageData _pageData;
        private Canvas _canvas;
        private GraphicRaycaster _graphicRaycaster;

        #region INTERNAL CLASS
        internal void SetupPage(SceneUI sceneUI)
        {
            _sceneUI = sceneUI;

            _canvas = GetComponent<Canvas>();
            _graphicRaycaster = GetComponent<GraphicRaycaster>();
        }

        internal void OnPush(PageData data)
        {
            _pageData = data;
            OnPushed?.Invoke(_pageData);
        }

        internal void Open()
        {
            SetPage(true);

            OnOpen?.Invoke();
        }

        internal void Close()
        {
            SetPage(false);

            OnClose?.Invoke();
        }
        #endregion

        public void OpenPage(EnumId pageId)
        {
            SceneUI.PushPage(pageId);
        }

        public void OpenPage(EnumId pageId, PageData data = null)
        {
            SceneUI.PushPage(pageId, data);
        }

        public void ReturnToPage(EnumId pageId)
        {
            SceneUI.PopToPage(pageId);
        }

        public void ReturnToFirstPage()
        {
            SceneUI.PopToFirstPage();
        }

        public void Return()
        {
            SceneUI.PopPage();
        }

        private void SetPage(bool condition)
        {
            if (offType == ClosePageType.CanvasGraphic)
            {
                if (_canvas != null)
                    _canvas.enabled = condition;
                else
                    gameObject.SetActive(condition);

                if (_graphicRaycaster != null)
                    _graphicRaycaster.enabled = condition;

            }
            else if(offType == ClosePageType.GameObject)
            {
                gameObject.SetActive(condition);
            }
        }
    }
}