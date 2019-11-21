using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Wrapper
{
    public Transform currentPos;
    public Transform desiredPos;
}

public class TransformMatcher : MonoBehaviour
{
    
    public Wrapper[] transformTable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach( Wrapper w in transformTable)
        {
            Transform currentPos = w.currentPos;
            Transform desiredPos = w.desiredPos;

            currentPos.position = desiredPos.position;
        }
    }
}
