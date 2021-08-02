using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lobby : MonoBehaviour
{
    public ScrollRect scroll;
    int tapCount = 2;
    int nowNum = 0;
    int distNum = 0;

    float interval = 1f;
    bool move = false;

    int next = -1;

    public GameObject[] taps;
    public Button[] buttons;

    void Start()
    {
        nowNum = distNum = 0;
        for (int i = 0; i < 4; i++)
        {
            int num = i;
            buttons[i].onClick.AddListener(()=>tapClick(num));
        }
    }

    float val;

    void Update()
    {
        if (next > -1)
        {
            tapClick(next, true);
        }

        if (distNum != nowNum)
        {        
            scroll.verticalScrollbar.value = Mathf.Lerp(scroll.verticalScrollbar.value, val, 0.2f); 
            
            if (Mathf.Abs(scroll.verticalScrollbar.value - val) < 0.01f)
            {
                scroll.content.sizeDelta = new Vector2(0f, 331f);
                taps[nowNum].SetActive(false);
                nowNum = distNum;
                move = false;
            }
        }        
    }

    void tapClick(int i, bool isNext = false)
    {
        if (move)
        {
            next = i;
            return;
        }

        if (nowNum == i)
            return;

        scroll.content.sizeDelta = new Vector2(0f, 662f);
        move = true;

        distNum = i;
        taps[distNum].SetActive(true);

        if (distNum < nowNum)
        {
            scroll.verticalScrollbar.value = 0f;
            val = 1f;
        }
        else if (nowNum < distNum)
        {
            scroll.verticalScrollbar.value = 1f;
            val = 0f;
        }

        if (isNext)
            next = -1;
    }
}
