using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class battleMain : MonoBehaviour {

    public List<mapInfo> mapinfos = new List<mapInfo>();

    //player
    public characterControl m_character;

    //enemy
    public Transform m_emenys;
    private List<GameObject> m_emenyList = new List<GameObject>();

    public CmdControl m_cmdPanel;

    public GameObject m_blockCollider;

    public GameObject m_menuBtn;

    public UISlider slider;

    //
    private waveInfo m_waveInfo;

    //move
    private int m_nowBlock;
    private int m_preBlock;

    //now time
    protected float m_TimecurValue;
    // temp 5
    protected int m_TimemaxValue = 5;

    bool countFlag = false;

    // Use this for initialization
    void Start () {
        // init base param
        initBattleField();

        // base obj create
        // palyerCraete();
        
        emenyCraete();

        // logic start
        battleStart();
    }

    // Update is called once per frame
    void Update () {

        if (countFlag)
        {
            battleTimeCount();
        }
  
    }

    void initBattleField()
    {
        for(int i = 0; i<mapinfos.Count; i++)
        {
            if (i <= 8)
            {
                mapinfos[i].SetMapType(mapInfo.mapType.palyer);
            }
            else
            {
                mapinfos[i].SetMapType(mapInfo.mapType.enemy);
            }

            mapinfos[i].m_Block = i; 
        }
        m_menuBtn.SetActive(false);
    }

    void battleStart()
    {
        for (int i = 0; i < m_emenyList.Count; i++)
        {
            m_emenyList[i].GetComponent<baseEmenyAI>().m_battleflag = true ;
        }
        countFlag = true;
    }

    void battleEnd()
    {
        countFlag = false;
    }

    void battleTimeCount()
    {
        m_TimecurValue += Time.deltaTime;
        slider.value = (m_TimecurValue / m_TimemaxValue);
        if(m_TimecurValue > m_TimemaxValue)
        {
            m_menuBtn.SetActive(true);
        }
    }

    void emenyCraete()
    {
        battleWave Wave =  new GameObject(typeof(battleWave).Name).AddComponent<battleWave>();
        Wave.m_main = this;

        Wave.initWave();

    }

    public void createEmenyByRecourse(string prefabsname , int pos , bool isBoss = false)
    {
        GameObject temp = Instantiate(Resources.Load("Assets\\enemy\\" +prefabsname, typeof(GameObject))) as GameObject;
        temp.name = m_emenyList.Count.ToString() + temp.name;
        temp.transform.localScale = new Vector3(-1, 1, 1);
        temp.transform.parent = mapinfos[pos].transform;

        //pos set
        temp.GetComponent<baseEmenyAI>().enemyPos = pos;
        EmenyMoveTo(temp.GetComponent<baseEmenyAI>(), pos);

        temp.GetComponent<baseEmenyAI>().m_Main = this;

        //
        m_emenyList.Add(temp);

    }

    public void moveTo(int blockName)
    {
        if (m_nowBlock != blockName)
        {
            m_character.transform.parent = mapinfos[blockName].transform;

            m_character.transform.position = Vector3.zero;
            int pos_z = (blockName + 1) % 3;
            switch (pos_z)
            {
                case 0:
                    pos_z = -4 * 400;
                    break;
                case 1:
                    pos_z = -2 * 400; ;
                    break;
                case 2:
                    pos_z = -3 * 400; ;
                    break;
            }
            m_character.transform.localPosition = new Vector3(0, 30, pos_z);
            m_character.motionContronl(characterControl.characterState.walk);

            mapinfos[blockName].ManInBlock = true;
            if (m_nowBlock != m_preBlock)
            {
                m_preBlock = m_nowBlock;
                mapinfos[m_preBlock].ManInBlock = false;
            }
            m_nowBlock = blockName;
            m_character.m_mypos = m_nowBlock;
        }
    }

    public void EmenyMoveTo(baseEmenyAI em, int blockName)
    {
        if (em.enemyPerPos != 0)
        { 
            mapinfos[em.enemyPerPos].ManInBlock = false;
        }
        em.transform.parent = mapinfos[blockName].transform;

        int pos_z = (blockName + 1) % 3;
        switch (pos_z)
        {
            case 0:
                pos_z = -1;
                break;
            case 1:
                pos_z = 1;
                break;
            case 2:
                pos_z = 0;
                break;
        }

        em.transform.position = Vector3.zero;
        em.transform.localPosition = new Vector3(255, 30, 0);
        em.stateControl(baseEmenyAI.CharacterState.run);
        em.transform.parent = m_emenys;
        em.transform.localPosition = new Vector3(em.transform.localPosition.x, em.transform.localPosition.y, pos_z);

        em.enemyPerPos = em.enemyPos;
        em.enemyPos = blockName;
        mapinfos[blockName].ManInBlock = true;
    }

    public void OnClickOpenCMD()
    {
        m_cmdPanel.gameObject.SetActive(!m_cmdPanel.gameObject.activeInHierarchy);
        m_blockCollider.SetActive(!m_blockCollider.activeInHierarchy);
        if (m_cmdPanel.gameObject.activeInHierarchy)
        {
            m_cmdPanel.Init();
        }

        for (int i = 0; i < m_cmdPanel.m_useCard.Length; i++)
        {
            // clear use chip
            if (m_character.useChips.Count > i)
            {
                m_character.useChips.Remove(m_character.useChips[i]);
            }
        }
    }

    public void OnClickOpenCMD_btnOK()
    {
        for (int i = 0; i < m_cmdPanel.m_useCard.Length; i++)
        {
            if (m_cmdPanel.m_useCard[i] !=null)
            { 
                m_character.useChips.Add(m_cmdPanel.m_useCard[i]);
            } 
        }
        m_cmdPanel.gameObject.SetActive(false);
        m_blockCollider.SetActive(!m_blockCollider.activeInHierarchy);
        if (m_character.useChips.Count == 0)
            return;
        m_menuBtn.SetActive(false);
        m_TimecurValue = 0f;
    }

    public void OnClickAttack()
    {
        if(m_character.useChips.Count != 0)
        {
            m_character.useChips.Remove(m_character.useChips[0]);
            m_character.motionContronl(characterControl.characterState.shot);
        }
    }

}
