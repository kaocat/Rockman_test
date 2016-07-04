using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;

public class CmdControl : MonoBehaviour {

    public GameObject m_cardunit;

    public UIGrid m_grid;

    // use card list pos
    public List<Transform> m_useCardPos = new List<Transform>();
    // use card list
    public DataChip[] m_useCard = new DataChip[5];

    protected const int cardMax = 10;

    private List<GameObject> m_unitList = new List<GameObject>();

    // record unit ui original pos
    private List<Vector3> m_unitInitPosList = new List<Vector3>();

    // Use this for initialization
    void Start () {

    }

    public void Init()
    {

        createdObj();

        m_grid.sorting = UIGrid.Sorting.Alphabetic;
        m_grid.onReposition = OnGridRepostion;
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnDisable ()
    {
        ResetChipPos();
    }

    void ResetChipPos()
    {
        if (m_unitList.Count == 0) return;
        {
            for (int i = 0; i < cardMax; i++)
            {
                m_unitList[i].transform.parent = m_grid.transform;
                //TmpObj.transform.localPosition = Vector3.zero;
                m_unitList[i].transform.localPosition = m_unitInitPosList[i];  // let btn easy click
                m_unitList[i].GetComponent<UIButton>().enabled = true;
                m_useCard = new DataChip[5];
            }
        }
    }

    void createdObj()
    {
        if(m_unitList.Count != 0) return;
        {
            for (int i = 0; i < cardMax; i++)
            {
                GameObject TmpObj = Instantiate(m_cardunit) as GameObject;
                TmpObj.name = i.ToString() + "_" + TmpObj.name;
                TmpObj.transform.parent = m_grid.transform;
                //TmpObj.transform.localPosition = Vector3.zero;
                TmpObj.transform.localPosition = new Vector3(0, 0, -50);  // let btn easy click
                TmpObj.transform.localScale = Vector3.one;
                TmpObj.SetActive(true);
                m_unitList.Add(TmpObj);
            }
            m_cardunit.gameObject.transform.localPosition = new Vector3(10000, 0, 0);
        }
    }

    void OnGridRepostion()
    {
        // get the init pos
        if (m_unitInitPosList.Count == 0 )
        {
            for (int i = 0; i < m_unitList.Count; i++)
            {
                m_unitInitPosList.Add(m_unitList[i].transform.localPosition);
            }
        }
    }

    public void OnClickChips(GameObject btn)
    {
        for (int i = 0; i < m_useCard.Length; i++)
        {
            if (m_useCard[i] == null )
            {

                m_useCard[i] = btn.GetComponent<DataChip>();
                btn.transform.parent = m_useCardPos[i].transform;
                btn.transform.localPosition = Vector3.zero;
                btn.GetComponent<UIButton>().enabled = false;
                break;
            }
        }
    }

    public void OnClickUseCard(GameObject btn)
    {
        string[] name = btn.gameObject.name.Split('_');
        int temp = Convert.ToInt32(name[0]);
        if (m_useCard[temp - 1] != null)
        {
            string[] name_ori = m_useCard[temp - 1].name.Split('_');
            int temp_ori = Convert.ToInt32(name_ori[0]);
            m_useCard[temp - 1].transform.parent = m_grid.transform;
            m_useCard[temp - 1].transform.localPosition = m_unitInitPosList[temp_ori];
            m_useCard[temp - 1].GetComponent<UIButton>().enabled = true;
            m_useCard[temp - 1] = null;
        }
    }
}
