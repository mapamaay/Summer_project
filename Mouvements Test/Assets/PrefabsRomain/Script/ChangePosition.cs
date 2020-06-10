using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePosition : MonoBehaviour
{
    private GameObject beam;
    public string object_name;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        beam = GameObject.Find(object_name);
        transform.position = beam.transform.position;
    }
}
