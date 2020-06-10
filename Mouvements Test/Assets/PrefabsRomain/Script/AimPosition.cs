using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimPosition : MonoBehaviour
{
  	private GameObject beamPosition;
    public string object_name;
    Vector3 m_NewPosition;
	public float m_YPosition;

    // Start is called before the first frame update
    void Start()
    {
        m_NewPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        beamPosition = GameObject.Find(object_name);
        m_NewPosition = beamPosition.transform.position;
        m_NewPosition.y = m_YPosition;
        transform.position = m_NewPosition;
    }
}