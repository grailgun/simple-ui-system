using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class UIFocusModeEditor : MonoBehaviour 
{
    private Transform container;

    [Header("ENABLE ALL CONTAINERS")]

    public bool OnSelectNothing = false;
    public bool OnSelectOnAnotherRoot = false;
    public bool OnSelectItself = false;
    public bool OnSelectOwnRoot = false;
    public bool OnSelectOnSibling = false;

    private void Awake()
    {
        if (transform == transform.root)
        {
            Debug.LogError("Dont work in root objects !");
            return;
        }

        container = transform;
        Selection.selectionChanged += OnSelectionChange;
    }

    private void OnDestroy()
    {
        if (container == null) return;

        ToggleAll(true);
        Selection.selectionChanged -= OnSelectionChange;
    }

    private void OnSelectionChange()
    {
        Transform selectedTransform = Selection.activeTransform;
        if (ShouldReturn(selectedTransform)) return;

        int stepsFromContainer = CalculateStepsFromContainer(selectedTransform);
        ActivateSelectedContainer(stepsFromContainer, selectedTransform);
    }

    private bool ShouldReturn(Transform t)
    {
        if (t == null)
        {
            if (OnSelectNothing)
                ToggleAll(true);

            return true;
        }

        if (t.root != container.root)
        {
            if (OnSelectOnAnotherRoot)
                ToggleAll(true);

            return true;
        }

        if (t == container)
        {
            if (OnSelectItself)
                ToggleAll(true);

            return true;
        }

        if (t == container.root)
        {
            if (OnSelectOwnRoot)
                ToggleAll(true);

            return true;
        }

        if (!IsInsideOfContainer(t))
        {
            if (OnSelectOnSibling)
                ToggleAll(true);

            return true;
        }

        return false;
    }

    private bool IsInsideOfContainer(Transform t)
    {
        bool isInside = false;

        while (t.parent != null)
        {
            if (t == container)
            {
                isInside = true;
                break;
            }
            t = t.parent;
        }

        return isInside;
    }

    private int CalculateStepsFromContainer(Transform t)
    {
        int _stepsFromContainer = 0;
        while (t != container)
        {
            _stepsFromContainer++;
            t = t.parent;

            //Lets say that Im little paranoic about 'while' loops
            if (_stepsFromContainer > 100)
                break;
        }

        return _stepsFromContainer;
    }

    private void ToggleAll(bool option)
    {
        for (int i = 0; i < container.childCount; i++)
        {
            container.GetChild(i).gameObject.SetActive(option);
        }
    }

    private void ActivateSelectedContainer(int levels, Transform t)
    {
        Transform selectedContainer = t;
        for (int i = 0; i < levels - 1; i++)
        {
            selectedContainer = selectedContainer.parent;
        }

        ToggleAll(false);
        selectedContainer.gameObject.SetActive(true);
    }
}