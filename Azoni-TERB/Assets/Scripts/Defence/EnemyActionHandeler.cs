using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyActionHandeler : MonoBehaviour
{
    [SerializeField] EnemyManager em;
    [SerializeField] Transform grabPoint;
    int rayDist = 2;
    NavMeshAgent nm;
    Vector3 currentDes;
    Vector3 startPos;
    [HideInInspector] public GameObject currentitem;
    ItemTimerHandeler ith;
    bool grab = false;
    private void Awake() {
        em = FindObjectOfType<EnemyManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        nm = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(currentitem != null && !grab){
            transform.LookAt(new Vector3(currentitem.transform.position.x, 1.8f, currentitem.transform.position.z));
            nm.SetDestination(new Vector3(currentitem.transform.position.x, 1.8f, currentitem.transform.position.z));
        }
        if(grab && currentitem != null){
                currentitem.transform.position = grabPoint.position;
        }
        
        if(currentitem!= null && nm.destination != startPos && transform.position != em.enemySpawnStartPos.position){
            if(Vector3.Distance(transform.position, new Vector3(currentitem.transform.position.x, 1.8f, currentitem.transform.position.z))<= nm.stoppingDistance + 0.5f){
                if(currentitem.GetComponent<TrashManager>().currentState != currentitem.GetComponent<TrashManager>().states[1] && !grab){
                    nm.SetDestination(startPos);
                    currentitem = null;
                    em.isMoved = false;
                    return;
                }

                if(currentitem.GetComponent<TrashManager>().currentState == currentitem.GetComponent<TrashManager>().states[1]){
                    currentitem.transform.position = grabPoint.position;
                    currentitem.GetComponent<TrashManager>().currentState = currentitem.GetComponent<TrashManager>().states[0];
                    nm.SetDestination(startPos);
                    grab = true;
                    ith.RestItem(currentitem);
                    em.isMoved = false;
                }
                
            }
        }
    }
    public void MoveEnemy(Vector3 startPos, GameObject item, ItemTimerHandeler ith){
        Vector3 desPos = item.transform.position;
        nm.SetDestination(desPos);
        currentDes = new Vector3(desPos.x,1.8f,desPos.z);
        this.startPos = startPos;
        currentitem = item;
        this.ith = ith;
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawRay(transform.position,transform.TransformDirection(Vector3.forward)*rayDist);
    }
}
