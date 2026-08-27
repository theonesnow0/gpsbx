using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class TabGroup : MonoBehaviour {
    [SerializeField] int m_currentIndex = 0;

    [SerializeField] ToggleGroup m_toggleGroup;
    [SerializeField] List<Toggle> m_tabs = new();
    [SerializeField] List<CanvasGroup> m_pages = new();

    CanvasGroup m_previousPage, m_currentPage;

    public UnityEvent<int> OnPageIndexChanged;

    void SwitchPage(int index) {
        m_pages[index];
        
        OnPageIndexChanged?.Invoke(index);
    }
}
