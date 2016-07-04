using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class waveInfo : MonoBehaviour {

    private static List<monsterInfo> m_enemys = new List<monsterInfo>();
    public List<monsterInfo> m_Enemys 
    {
        get  { return m_enemys ;  }
        set { m_enemys = value; }
    }

    public int m_nowWave;


    //base state
    // hp , attack , blablablablablablablabla
    //

    public const int m_waveCount = 3;
	// Use this for initialization

	
    public void getWave(int waveN)
    {
        if (waveN > m_nowWave)
        {
            waveN = m_nowWave;
        }
        switch(waveN)
        {
            case 1 :
                for (int i = 0; i < 3; i++)
                {
                    monsterInfo temp = new monsterInfo();
                    temp.m_name = i.ToString() + "enemy_1";
                    temp.m_pos = (i + 1) * 2 + 10;
                    if (i == 0)
                    {
                        temp.canMove = true;
                    }
                    temp.isBoss = false;

                    m_Enemys.Add(temp);
                }
                break;
            case 2:
                for (int i = 0; i < 3; i++)
                {
                    monsterInfo temp = new monsterInfo();
                    temp.m_name = i.ToString() + "enemy_1";
                    temp.m_pos = 16;

                    temp.isBoss = false;
                    temp.canMove = false;

                    m_Enemys.Add(temp);
                }
                break;
        }
    }

	// Update is called once per frame
	void Update () {
	    
	}

    public void initStart()
    {
        //fake data
        for (int i = 0; i < 3; i++)
        {
            monsterInfo temp = new monsterInfo();
            temp.m_name = i.ToString() + "enemy_1";
            temp.m_pos = (i + 1) * 2 + 10;
            if (i == 0)
            {
                temp.canMove = true;
            }
            temp.isBoss = false;

            m_Enemys.Add(temp);
        }
    }
}
