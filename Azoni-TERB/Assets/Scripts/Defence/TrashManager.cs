using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashManager : MonoBehaviour
{
    //este se explica solo :v
    [SerializeField] public string type;

    public string [] states = {"Grabbed","Dropped","Process"};

    public string currentState;  
    // Start is called before the first frame update
    void Start()
    {
        currentState = states[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
