using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Core.UI
{
    [DefaultExecutionOrder(-1)]
    public class SceneUI : MonoBehaviour
    {
        public Canvas Canvas { get; private set; }
        public Camera UICamera { get; private set; }

        [Header("Starter Page")]
        [SerializeField]
        private UIEnum starterPage;
        [SerializeField]
        private List<UIPage> _stackedPages;

        private Dictionary<UIEnum, UIPage> _cachedPages;

        private void Awake()
        {
            Canvas = GetComponent<Canvas>();
            UICamera = Canvas.worldCamera;

            _cachedPages = new Dictionary<UIEnum, UIPage>();
            _stackedPages = new List<UIPage>();

            PreparePages();
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.5f);
            
            if(starterPage != null)
                PushPage(starterPage);
        }

        public void PushPage(UIEnum pageId)
        {
            PushPage(pageId, null);
        }

        public void PushPage(UIEnum pageId, PageData data)
        {
            if (GetPageFromStack(pageId) != null)
            {
                Debug.LogWarning(string.Format("Screen {0} already exists in the stack. Ignoring push request.", pageId.name));
                return;
            }

            if (_cachedPages.TryGetValue(pageId, out UIPage pushedPage) == false)
            {
                // Or maybe spawn the page?
                Debug.Log($"Page {pageId} doesn't exist");
                return;
            }

            // Set current top page condition
            UIPage topPage = GetTopPage();
            if (topPage != null && pushedPage.disablePreviousPage)
            {
                topPage.Close();
            }

            // Change top page to new page
            _stackedPages.Insert(0, pushedPage);
            pushedPage.OnPush(data);
            pushedPage.Open();
        }
        
        public void PopToFirstPage()
        {
            if(starterPage != null)
                PopToPage(_stackedPages[_stackedPages.Count - 1].PageID);
        }

        public void PopToPage(UIEnum pageId)
        {
            if (pageId == null)
            {
                Debug.Log($"Page ID is Null");
                return;
            }

            UIPage targetPage = null;
            List<UIPage> toBePoppedPage = new List<UIPage>();

            for (int i = 0; i < _stackedPages.Count; i++)
            {
                UIPage page = _stackedPages[i];

                if (page.PageID.IsEqual(pageId) == false)
                {
                    toBePoppedPage.Add(page);
                }
                else
                {
                    targetPage = page;
                    break;
                }
            }

            for (int i = 0; i < toBePoppedPage.Count; i++)
            {
                UIPage poppedPage = toBePoppedPage[i];
                poppedPage.Close();
                _stackedPages.Remove(poppedPage);
            }

            if (targetPage == null)
                Debug.LogWarning($"Can't found Page {pageId.name}. Stack Empty");
            else
                targetPage.Open();
        }

        public void PopPage()
        {
            UIPage pageToPop = GetTopPage();
            if (pageToPop != null)
            {
                _stackedPages.RemoveAt(0);
                pageToPop.Close();
            }

            UIPage newTopPage = GetTopPage();
            if (newTopPage != null)
            {
                newTopPage.Open();
            }
        }

        private UIPage GetTopPage()
        {
            if (_stackedPages.Count > 0)
                return _stackedPages[0];

            return null;
        }

        private UIPage GetCachedPage(UIEnum pageId)
        {
            if (_cachedPages.ContainsKey(pageId))
                return _cachedPages[pageId];

            return null;
        }

        private void PreparePages()
        {
            var pages = GetComponentsInChildren<UIPage>(true);

            foreach (var page in pages)
            {
                if (page.PageID == null)
                {
                    Debug.LogError($"Page id of Page {page.name} is Null. Can't add to dictionary.");
                    continue;
                }

                page.SetupPage(this);
                page.Close();

                _cachedPages.TryAdd(page.PageID, page);
            }
        }
    
        private UIPage GetPageFromStack(UIEnum pageId)
        {
            UIPage result = null;
            foreach (var page in _stackedPages)
            {
                if (page.PageID.IsEqual(pageId))
                {
                    result = page;
                    break;
                }
            }

            return result;
        }
    }    
}