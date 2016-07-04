using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class characterControl : MonoBehaviour {

    public List<DataChip> useChips = new List<DataChip>();

    public GameObject m_useChip;

    public int m_mypos;

    Animator m_charAnimator;

    public enum characterState
    {
        idle = 1,
        walk = 2,
        attack = 3,
        shot = 4,
        throwPose = 5,
        die = 6
    }

    private static characterState m_nowState;
    public characterState m_NowState
    {
        set { m_nowState = value; }
        get { return m_nowState; }
    }

    // Use this for initialization
    void Start () {
        m_charAnimator = this.GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        updateUseChip();
    }

    void updateUseChip()
    {
        if (useChips.Count != 0)
        {
            for (int i = 0; i < useChips.Count; i++)
            {
                if (useChips[0])
                {
                    m_useChip.GetComponent<UITexture>().mainTexture = useChips[i].m_texture;
                    break;
                }
            }
        }
        else
        {
            m_useChip.GetComponent<UITexture>().mainTexture = null;
        }
    }

    public void motionContronl(characterState tempMotion)
    {
        switch(tempMotion)
        {
            case characterState.walk:
                m_charAnimator.SetBool("walk", true);
                break;
            case characterState.shot:
                m_charAnimator.SetBool("shot", true);
                break;
        }
    }

    public void  stateToOver(string aniname)
    {
        m_charAnimator.SetBool(aniname, false); 
    }

    
}
