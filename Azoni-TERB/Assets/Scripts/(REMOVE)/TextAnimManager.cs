using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAnimManager : MonoBehaviour
{
    public bool isExit = false;
    int counter = 0;
    [SerializeField]int maxCount;

    [SerializeField] Animator anim;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextAnimation(){
        if(counter <= maxCount){
            anim.SetTrigger("Exit"+counter);
            counter++;
        }else {
            anim.SetTrigger("ExitColle");
            isExit = true;
        }
    }



}
