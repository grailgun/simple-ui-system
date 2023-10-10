using Lean.Gui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Core.UI
{
    public class UIPopup : UIPage
    {
        [System.Serializable]
        public class ButtonAction
        {
            public LeanButton button;
            public UnityEvent buttonAction;

            public void ConnectButton()
            {
                button.OnClick.AddListener(() => buttonAction?.Invoke());
            }
        }

        public override bool DisablePreviousPage => false;

        [Header("Buttons")]
        [SerializeField]
        private LeanButton closeButton;
        [SerializeField]
        private ButtonAction[] buttonActions;

        private void Awake()
        {
            closeButton.OnClick.AddListener(ClosePopup);

            foreach (var buttonAction in buttonActions)
                buttonAction.ConnectButton();            
        }

        public void OpenPopup()
        {
            OpenPage(PageID);
        }

        public void ClosePopup()
        {
            Return();
        }
    }
}