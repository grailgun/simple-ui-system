using Lean.Gui;
using UnityEngine;
using UnityEngine.Events;

namespace Core.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIPage : LeanWindow
    {
        public UIEnum PageID => pageId;
        public PageData PageData => _pageData;
        public SceneUI SceneUI => _sceneUI;
        public virtual bool DisablePreviousPage => disablePreviousPage;

        [Header("UI ID")]
        [SerializeField] private UIEnum pageId;

        [Header("Page Setting")]
        [SerializeField] private bool disablePreviousPage = false;

        [Header("Events Hook")]
        public UnityEvent<PageData> OnPushed;

        private SceneUI _sceneUI;
        private PageData _pageData;

        #region INTERNAL CLASS
        internal void SetupPage(SceneUI sceneUI)
        {
            _sceneUI = sceneUI;
            TurnOffNow();
        }

        internal void OnPush(PageData data)
        {
            _pageData = data;
            OnPushed?.Invoke(_pageData);
        }

        internal void Open()
        {
            //need more functional things?
            TurnOn();
        }

        internal void Close()
        {
            //need more functional things?
            TurnOff();
        }
        #endregion

        public void OpenPage(UIEnum pageId)
        {
            SceneUI.PushPage(pageId);
        }

        public void OpenPage(UIEnum pageId, PageData data = null)
        {
            SceneUI.PushPage(pageId, data);
        }

        public void ReturnToPage(UIEnum pageId)
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
    }
}