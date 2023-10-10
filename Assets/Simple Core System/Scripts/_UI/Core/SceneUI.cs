using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    [DefaultExecutionOrder(-1)]
    public class SceneUI : MonoBehaviour
    {
        [Header("Starter Page")]
        [SerializeField]
        private EnumId starterPage;
        [SerializeField]
        private List<UIPage> stackedPages;

        [Header("Pop Up")]
        [SerializeField]
        private List<UIPage> popUps;

        private Dictionary<EnumId, UIPage> _cachedPages;
        private Dictionary<EnumId, UIPage> _cachedPopups;

        private void Awake()
        {
            _cachedPages = new Dictionary<EnumId, UIPage>();
            _cachedPopups = new Dictionary<EnumId, UIPage>();
            stackedPages = new List<UIPage>();

            PreparePages();
            PreparePopups();
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.2f);
            
            if(starterPage != null)
                PushPage(starterPage);
        }

        public void PushPage(EnumId pageId)
        {
            PushPage(pageId, null);
        }

        public void PushPage(EnumId pageId, PageData data)
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
            if (topPage != null && pushedPage.DisablePreviousPage)
            {
                topPage.Close();
            }

            // Change top page to new page
            stackedPages.Insert(0, pushedPage);
            pushedPage.OnPush(data);
            pushedPage.Open();
        }
        
        public void PopToFirstPage()
        {
            if(starterPage != null)
                PopToPage(stackedPages[stackedPages.Count - 1].PageID);
        }

        public void PopToPage(EnumId pageId)
        {
            if (pageId == null)
            {
                Debug.Log($"Page ID is Null");
                return;
            }

            UIPage targetPage = null;
            List<UIPage> toBePoppedPage = new List<UIPage>();

            for (int i = 0; i < stackedPages.Count; i++)
            {
                UIPage page = stackedPages[i];

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
                stackedPages.Remove(poppedPage);
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
                stackedPages.RemoveAt(0);
                pageToPop.Close();
            }

            UIPage newTopPage = GetTopPage();
            if (newTopPage != null)
            {
                newTopPage.Open();
            }
        }

        public UIPage GetPage(EnumId pageId)
        {
            if (_cachedPages.ContainsKey(pageId))
                return _cachedPages[pageId];

            return null;
        }

        public UIPopup GetPopup(EnumId popupId)
        {
            if (_cachedPopups.ContainsKey(popupId))
                return _cachedPopups[popupId] as UIPopup;

            return null;
        }

        private UIPage GetTopPage()
        {
            if (stackedPages.Count > 0)
                return stackedPages[0];

            return null;
        }

        private void PreparePages()
        {
            var pages = GetComponentsInChildren<UIPage>(true);

            foreach (var page in pages)
            {
                page.gameObject.SetActive(true);
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

        private void PreparePopups()
        {
            var popups = GetComponentsInChildren<UIPopup>(true);

            foreach (var popup in popups)
            {
                popup.gameObject.SetActive(true);
                if (popup.PageID == null)
                {
                    Debug.LogError($"Page id of Page {popup.name} is Null. Can't add to dictionary.");
                    continue;
                }

                popup.SetupPage(this);
                popup.Close();

                _cachedPopups.TryAdd(popup.PageID, popup);
            }
        }

        private UIPage GetPageFromStack(EnumId pageId)
        {
            UIPage result = null;
            foreach (var page in stackedPages)
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