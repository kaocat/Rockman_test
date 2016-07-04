using UnityEngine;
using System.Collections;

public class mapInfo : MonoBehaviour {

    public battleMain m_control;

    //people stand on
    public bool ManInBlock;

    public enum mapType
    {
        palyer = 1,
        enemy  = 2
    }

    //maptype is emeny cant move on ;
    private mapType m_mapType;
    public mapType m_MapType
    {
        set { m_mapType = value ;  }
        get { return m_mapType ; }
    }

    //now block count ;
    private int m_block;
    public int m_Block
    {
        set { m_block = value; }
        get { return m_block; }
    }

    void Start () {
	
	}

	void Update () {
	
	}

    public void SetMapType(mapType tmptype)
    {
        if (m_MapType != tmptype)
        {
            if (tmptype == mapType.enemy)
            {
                m_MapType = tmptype;
                this.gameObject.GetComponent<UISprite>().color = Color.blue;
                this.gameObject.GetComponent<UIButton>().defaultColor = Color.blue;
            }
            else if (tmptype == mapType.palyer)
            {
                m_MapType = tmptype;
                this.gameObject.GetComponent<UISprite>().color = Color.red;
            }
        }
    }

    public bool CantMove()
    {
        if (m_MapType == mapType.palyer && !ManInBlock) return true;

        return false;
    }

    public void OnclickMove()
    {
        if (CantMove())
        {
            m_control.moveTo(m_Block);
        }
    }

}
