using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TabsUIHorizontal : MonoBehaviour
{
    [SerializeField]
    private List<Button> tabBtns;

    [SerializeField]
    private List<GameObject> tabContent;

    private Color tabColorActive;

    private Color tabColorInactive;

    private void Start()
    {
        for (int i = 0; i < tabBtns.Count; i++)
        {
            var index = i;
            tabBtns[i].onClick.AddListener(() => OnBtnClicked(index));
        }
        tabColorActive = tabBtns[0].GetComponent<Image>().color;
        tabColorInactive = tabBtns[1].GetComponent<Image>().color;
        CloseAllContent();
        tabContent[0].SetActive(true);
    }
    public void OnBtnClicked(int itemIndex)
    {
        CloseAllContent();
        tabContent[itemIndex].SetActive(true);
        ResetAllColor();
        tabBtns[itemIndex].GetComponent<Image>().color = tabColorActive;

    }


    private void CloseAllContent()
    {
        for (int i = 0; i < tabContent.Count; i++)
        {
            tabContent[i].SetActive(false);
        }
    }
    private void ResetAllColor()
    {
        for (int i = 0; i < tabBtns.Count; i++)
        {
            tabBtns[i].GetComponent<Image>().color = tabColorInactive;
        }
    }
}
