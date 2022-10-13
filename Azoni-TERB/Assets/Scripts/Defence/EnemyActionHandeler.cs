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
    Transform currentDes;
    Vector3 startPos;
    GameObject currentitem;

    bool state = false;
    
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
        if(nm.destination!= null && nm.destination != startPos && transform.position != em.enemySpawnStartPos.position){
            if(currentDes != null)
            {
                if(Vector3.Distance(transform.position, currentDes.position)<= rayDist){
                    if(currentitem.GetComponent<TrashManager>().currentState == currentitem.GetComponent<TrashManager>().states[1]){
                        currentitem.transform.position = grabPoint.position;
                        state = true;
                        nm.SetDestination(startPos);
                        em.isMoved = false;
                    }

                }
            }
            if (state && currentitem != null){

                currentitem.transform.position = grabPoint.position;
            }
        }
    }
    public void MoveEnemy(Vector3 startPos, GameObject item){
        Transform desPos = item.transform;
        nm.SetDestination(desPos.position);
        currentDes = desPos;
        this.startPos = startPos;
        currentitem = item;
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawRay(transform.position,transform.TransformDirection(Vector3.forward)*rayDist);
    }
}
