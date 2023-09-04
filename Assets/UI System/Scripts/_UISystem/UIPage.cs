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

        [Header("UI ID")]
        [SerializeField] private UIEnum pageId;

        [Header("Page Setting")]
        public bool disablePreviousPage = false;

        [Header("Events Hook")]
        public UnityEvent<PageData> OnPushed;

        private SceneUI _sceneUI;
        private PageData _pageData;

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
            //need more functional things
            TurnOn();
        }

        internal void Close()
        {
            //need more functional things
            TurnOff();
        }

        public void Back()
        {
            SceneUI.PopPage();
        }
    }
}