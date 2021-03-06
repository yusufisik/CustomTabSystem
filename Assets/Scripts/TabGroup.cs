﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TabTransitionTypes { ColorTint, SpriteSwap }

[AddComponentMenu("UI/TabGroup")]
public class TabGroup : MonoBehaviour
{
    private List<TabButton> tabButtons;

    public TabTransitionTypes transition = TabTransitionTypes.ColorTint;
    //Colors
    public Color normalColor = Color.white;
    public Color highlightedColor = new Color(0.8f, 0.8f, 0.8f);
    public Color selectedColor = new Color(1f, 0.5f, 0f);
    public Color disabledColor = Color.grey;
    //Sprites
    public Sprite normalSprite;
    public Sprite highlightedSprite;
    public Sprite selectedSprite;
    public Sprite disabledSprite;

    private TabButton selectedTab;
    private void Start()
    {
        SelectFirstAvaibleTab();
    }

    public void Subscribe(TabButton tabButton)
    {
        if (tabButtons == null)
            tabButtons = new List<TabButton>();

        tabButtons.Add(tabButton);
    }

    public void OnTabEnter(TabButton tabButton)
    {
        ResetTabs();
        if (tabButton.interactible && (selectedTab == null || tabButton != selectedTab))
        {
            if(transition == TabTransitionTypes.ColorTint)
                tabButton.backgroundImage.color = highlightedColor;
            else if (transition == TabTransitionTypes.SpriteSwap)
            {
                if(highlightedSprite != null)
                    tabButton.backgroundImage.sprite = highlightedSprite;
            }
        }
    }
    public void OnTabExit(TabButton tabButton)
    {
        ResetTabs();
    }
    public void OnTabSelected(TabButton tabButton)
    {
        if(tabButton.interactible)
            SelectTab(tabButton);
    }
    private void SelectTab(TabButton tabButton)
    {
        if (selectedTab != null)
            selectedTab.DeSelect();

        selectedTab = tabButton;
        selectedTab.Select();

        ResetTabs();
        if (transition == TabTransitionTypes.ColorTint)
            tabButton.backgroundImage.color = selectedColor;
        else if (transition == TabTransitionTypes.SpriteSwap)
        {
            if (selectedSprite != null)
                tabButton.backgroundImage.sprite = selectedSprite;
        }

        foreach (TabButton tButton in tabButtons)
        {
            if(tButton.panelToOpen != null)
            {
                if (tButton != tabButton)
                    tButton.panelToOpen.SetActive(false);
                else
                    tButton.panelToOpen.SetActive(true);
            }
        }
    }
    private void ResetTabs()
    {
        foreach(TabButton tabButton in tabButtons)
        {
            if (selectedTab != null && tabButton == selectedTab)
                continue;
            //reset background images or colors
            if (tabButton.interactible)
            {
                if (transition == TabTransitionTypes.ColorTint)
                    tabButton.backgroundImage.color = normalColor;
                else if (transition == TabTransitionTypes.SpriteSwap)
                {
                    if(normalSprite != null)
                        tabButton.backgroundImage.sprite = normalSprite;
                }
            }
            else
            {
                if (transition == TabTransitionTypes.ColorTint)
                    tabButton.backgroundImage.color = disabledColor;
                else if (transition == TabTransitionTypes.SpriteSwap)
                {
                    if(disabledSprite != null)
                        tabButton.backgroundImage.sprite = disabledSprite;
                }
            }
        }
    }

    private void SelectFirstAvaibleTab()
    {
        if (tabButtons.Count > 0)
        {
            ResetTabs();

            List<TabButton> interactibleTabs = new List<TabButton>();
            for (int i = 0; i < tabButtons.Count; i++)
            {
                if (tabButtons[i].interactible)
                    interactibleTabs.Add(tabButtons[i]);
            }

            if(interactibleTabs.Count > 0)
            {
                int smallestSiblingIndex = int.MaxValue;
                int indexx = -1;

                for (int i = 0; i < interactibleTabs.Count; i++)
                {
                    int siblingIndex = interactibleTabs[i].transform.GetSiblingIndex();
                    if (siblingIndex < smallestSiblingIndex)
                    {
                        smallestSiblingIndex = siblingIndex;
                        indexx = i;
                    }
                }
                SelectTab(interactibleTabs[indexx]);
            }
            else
            {
                foreach (TabButton tButton in tabButtons)
                {
                    if (tButton.panelToOpen != null)
                        tButton.panelToOpen.SetActive(false);
                }
            }
        }
    }
    public void SelectNextTab()
    {
        if (tabButtons.Count > 0)
        {
            ResetTabs();

            if(selectedTab != null)
            {
                List<TabButton> interactibleTabs = new List<TabButton>();
                for (int i = 0; i < tabButtons.Count; i++)
                {
                    if (tabButtons[i].interactible)
                        interactibleTabs.Add(tabButtons[i]);
                }

                if (interactibleTabs.Count > 0)
                {
                    int indexx = -1;
                    for (int i = 0; i < interactibleTabs.Count; i++)
                    {
                        if (selectedTab == interactibleTabs[i])
                        {
                            indexx = (i + 1) % interactibleTabs.Count;
                            break;
                        }
                    }
                    SelectTab(interactibleTabs[indexx]);
                }
                else
                {
                    foreach (TabButton tButton in tabButtons)
                    {
                        if (tButton.panelToOpen != null)
                            tButton.panelToOpen.SetActive(false);
                    }
                }
            }
            else
            {
                SelectFirstAvaibleTab();
            }
        }
    }
    public void SelectPreviousTab()
    {
        if (tabButtons.Count > 0)
        {
            ResetTabs();

            if (selectedTab != null)
            {
                List<TabButton> interactibleTabs = new List<TabButton>();
                for (int i = 0; i < tabButtons.Count; i++)
                {
                    if (tabButtons[i].interactible)
                        interactibleTabs.Add(tabButtons[i]);
                }

                if (interactibleTabs.Count > 0)
                {
                    int indexx = -1;
                    for (int i = 0; i < interactibleTabs.Count; i++)
                    {
                        if (selectedTab == interactibleTabs[i])
                        {
                            indexx = (i - 1) % interactibleTabs.Count;
                            break;
                        }
                    }
                    SelectTab(interactibleTabs[indexx]);
                }
                else
                {
                    foreach (TabButton tButton in tabButtons)
                    {
                        if (tButton.panelToOpen != null)
                            tButton.panelToOpen.SetActive(false);
                    }
                }
            }
            else
            {
                SelectFirstAvaibleTab();
            }
        }
    }
}


