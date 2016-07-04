using UnityEngine;
using System.Collections;

public class baseEmenyAI : MonoBehaviour {

    [SerializeField]
    public GameObject m_characte;
    [SerializeField]
    public Animator m_characterAnim;
    [SerializeField]
    public GameObject m_shot;
    [SerializeField]
    public Transform m_lancher;

    [SerializeField]
    public battleMain m_Main;

    public float m_speed = 0.0f;

    public bool m_attackflag = false;
    public bool m_battleflag = false;

    public bool m_canMove = true;

    private bool m_skillflag = false;

    // stand block
    public int enemyPos;
    public int enemyPerPos;

    public float m_thinktime = 0.0f;
    private float m_thinkCountDown = 0.0f;

    public float m_movetime = 0.0f;
    private float m_moveCountDown = 0.0f;

    monsterInfo m_baseInfo;

    public enum CharacterState
    {
        idle,
        run,
        attack,
        skill,
        stageon,
    }

    private CharacterState m_characterNow;
    public CharacterState m_CharacterNow
    {
        set { m_characterNow = value; }
        get { return m_CharacterNow; }
    }


    // Use this for initialization
    void Start()
    {
        m_thinkCountDown = m_thinktime;
        m_canMove = m_baseInfo.canMove;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_thinkCountDown <= 0 && m_battleflag)
        {
            stateControl(CharacterState.attack);
            m_thinkCountDown = m_thinktime;
            m_moveCountDown = 0.15f;
        }
        else if (m_moveCountDown <= 0)
        {
            baseMove();
            m_moveCountDown = m_movetime;
        }
        else
        {
            m_thinkCountDown -= Time.deltaTime;
            m_moveCountDown -= Time.deltaTime;

            m_characterAnim.SetBool("shot", m_attackflag);
        }

    }

    void moveTo(int line)
    {
        baseEmenyAI temp = this;
        switch (line)
        {
            case 1:
                for (int i = 9; i < 18; i += 3)
                {
                    if (!m_Main.mapinfos[i].ManInBlock && m_Main.mapinfos[i].m_MapType == mapInfo.mapType.enemy)
                    {
                        m_Main.EmenyMoveTo(temp, i);
                        break;
                    }
                }
                break;
            case 2:
                for (int i = 10; i < 18; i += 3)
                {
                    if (!m_Main.mapinfos[i].ManInBlock && m_Main.mapinfos[i].m_MapType == mapInfo.mapType.enemy)
                    {
                        m_Main.EmenyMoveTo(temp, i);
                        break;
                    }
                }
                break;
            case 3:
                for (int i = 11; i < 18; i += 3)
                {
                    if (!m_Main.mapinfos[i].ManInBlock && m_Main.mapinfos[i].m_MapType == mapInfo.mapType.enemy)
                    {
                        m_Main.EmenyMoveTo(temp, i);
                        break;
                    }
                }
                break;
        }
    }

    public void shotStart()
    {
        GameObject shotClone = (GameObject)Instantiate(m_shot, m_lancher.position, m_lancher.rotation);
        if (gameObject.tag == "enemy")
        {
            shotClone.transform.localScale =  new Vector3(-1 , 1 , 1);
        }
        shotClone.SetActive(true); 
    }

    public void stateControl(CharacterState m_cState)
    {
        switch (m_cState)
        {

            case CharacterState.idle:
                m_characterAnim.SetBool("shot", false);
                m_characterAnim.SetBool("walk", false);
                break;
            case CharacterState.attack:
                m_characterAnim.SetBool("shot", true);
                shotStart();
                break;
            case CharacterState.run:
                m_characterAnim.SetBool("walk", true);
                break;

            default:
                m_characterAnim.SetBool("shot", false);
                m_characterAnim.SetBool("walk", false);
                break;
        }
    }

    // move to same line attack , if block have people will move to second
    private void baseMove()
    {
        if (m_canMove)
        { 
            int tarPos = m_Main.m_character.m_mypos;
            int tempEnemyH = (enemyPos + 1) % 3;
            int tempH = (tarPos + 1) % 3;

            // move by same line
            if (tempEnemyH != tempH)
            {
                switch (tempH)
                {
                    // 11,14,17
                    case 0:
                        moveTo(3);
                        break;
                    // 9,12,15
                    case 1:
                        moveTo(1);
                        break;
                    // 10,13,16
                    case 2:
                        moveTo(2);
                        break;
                }
            }
        }
    }

    public void Die()
    {
        m_Main.mapinfos[enemyPos].ManInBlock = false;
        GameObject.Destroy(this.gameObject);
    }

}
