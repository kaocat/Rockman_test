using UnityEngine;
using System.Collections;

public class characterAniEvent : MonoBehaviour {

    public characterControl m_characterControl;



    public void aniEndCallBack(string aniname)
    {
        if (gameObject.tag != "enemy")
        {
            m_characterControl.stateToOver(aniname);
        }
        else
        {
            GetComponent<Animator>().SetBool(aniname, false);
        }
    }
}
