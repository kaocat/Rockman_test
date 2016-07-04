using UnityEngine;
using System.Collections;

public class DataChip : MonoBehaviour {

    //card id
    public int m_id ;

    public string m_name ;

    public int m_effect ;

    public Texture m_texture;

    // Use this for initialization
    void Start () {
        // defult
        m_id = 0;
        m_name = "skill_0";
        m_effect = 0;
        m_texture = this.gameObject.GetComponent<UITexture>().mainTexture;

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
