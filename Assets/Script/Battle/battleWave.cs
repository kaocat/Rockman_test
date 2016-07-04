using UnityEngine;
using System.Collections;

public class battleWave : MonoBehaviour {

    public battleMain m_main;

    waveInfo m_info;

    int nowWave = 1;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        int changeWave = 0;

        for (int i = 0; i < m_info.m_Enemys.Count; i++)
        {
            if(m_info.m_Enemys[i].isDie)
            {
                changeWave += 1;
            }
        }

        if (changeWave == m_info.m_Enemys.Count)
        {
            if (nowWave < waveInfo.m_waveCount)
            {
                nowWave++;
            }
            m_info.getWave(nowWave);
        }
    }

    void OnGUI()
    {
        //debug
        if(GUI.Button(new Rect(0,0,100,30),"next wave"))
        {
            for (int i = 0; i < m_info.m_Enemys.Count; i++)
            {
                
                
            }
            m_info.m_Enemys.Clear();
            m_info.getWave(nowWave++);
        }
    }

    public void initWave()
    {

        m_info =  new GameObject(typeof(waveInfo).Name).AddComponent<waveInfo>();
        m_info.initStart();

        m_info.getWave(nowWave);
        for (int i = 0; i < m_info.m_Enemys.Count; i++)
        {
            m_main.createEmenyByRecourse("enemy_1", m_info.m_Enemys[i].m_pos , m_info.m_Enemys[i].canMove);
        }
        
    }
    
}
