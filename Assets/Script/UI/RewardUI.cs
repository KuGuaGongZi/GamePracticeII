using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RewardUI : SingletonWindow<RewardUI>
{
    private Text timeText;
    public RewardUI()
    {
        m_sort_order = 5;
    }
    public override string windowName
    {
        get
        {
            return "RewardUI";
        }
    }
    public void SetText(string s)
    {
        timeText.text = s;
    }
    public void SetTime(int time)
    {
        timeText.text = time.ToString();
    }
    protected override void Initialize()
    {
        base.Initialize();
        timeText = Find<Text>("TimeText");

    }
}
