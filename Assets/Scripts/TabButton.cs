using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEditor;

[AddComponentMenu("UI/TabButton")]
[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public bool interactible = true;
    [Space(10)]
    public TabGroup tabGroup;
    public Image backgroundImage;
    [Header("Panel")]
    public GameObject panelToOpen;
    [Space(10)]
    public UnityEvent onTabSelected;
    public UnityEvent onTabDeselected;

    private void Reset()
    {
        backgroundImage = GetComponent<Image>();
    }

    private void Awake()
    {
        if (tabGroup != null)
            tabGroup.Subscribe(this);
        if(backgroundImage == null)
            backgroundImage = GetComponent<Image>();
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tabGroup != null)
            tabGroup.OnTabEnter(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (tabGroup != null)
            tabGroup.OnTabSelected(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tabGroup != null)
            tabGroup.OnTabExit(this);
    }

    public void Select()
    {
        if (onTabSelected != null)
            onTabSelected.Invoke();
    }

    public void DeSelect()
    {
        if (onTabDeselected != null)
            onTabDeselected.Invoke();
    }

    [MenuItem("GameObject/UI/TabButton", false)]
    static void CreateTabButtonInScene(MenuCommand menuCommand)
    {
        // Create a custom game object
        GameObject go = new GameObject("TabButton");
        // Ensure it gets reparented if this was a context click (otherwise does nothing)
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        go.AddComponent<TabButton>();
        // Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
    }
}
