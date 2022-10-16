using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] ItemTimerHandeler ith;
    [SerializeField] ItemsManager im;
    [SerializeField] public Transform enemySpawnStartPos;
    [SerializeField] GameObject enemyDefencePrefab;
    [SerializeField] Transform enemyHolderManager;
    [SerializeField] Vector3 [] spawnPos;
    [SerializeField] Vector3 [] spawnScale;
    [SerializeField] int numEnemies;
    [SerializeField] int roundTime;
    List<GameObject> enemies = new List<GameObject>();
    float [] timeSpawn;
    float timer;
    //[HideInInspector]
     public bool isMoved = false;
    
    // Start is called before the first frame update
    void Awake() {
        SetEnemies();
    }
    void Start()
    {
        InvokeRepeating("TimeMoveEnemy", 0f,1f);
    }

    // Update is called once per frame
    void Update()
    {
        roundTime= Mathf.RoundToInt(im.timeRemaining);      
    }
    void TimeMoveEnemy(){
        for (int g = 0; g < timeSpawn.Length; g++)
        {
            if(roundTime == Mathf.RoundToInt(timeSpawn[g]) && !isMoved){
                isMoved = true;
                MoveEnemy();
            }   
        }
    }
    void SetEnemies(){
        timer = im.timeRemaining;
        timeSpawn = new float[numEnemies];
        for (int l = 0; l < numEnemies; l++)
        {
            float num2 = UnityEngine.Random.Range(timer*0.15f, timer*0.9f);
            timeSpawn[l] = num2;
            GameObject obj = Instantiate(enemyDefencePrefab, enemySpawnStartPos.position, Quaternion.identity, enemyHolderManager);
            enemies.Add(obj);
        }
    }
    void MoveEnemy(){
        int num = UnityEngine.Random.Range(0, enemies.Count);
        int numb = UnityEngine.Random.Range(0, spawnPos.Length);
        int num1 = UnityEngine.Random.Range(0, ith.items.Count);
        Vector3 newPos = spawnPos[numb] + new Vector3(UnityEngine.Random.Range(-spawnScale[numb].x/2,spawnScale[numb].x/2),0f,UnityEngine.Random.Range(-spawnScale[numb].z/2,spawnScale[numb].z/2));
        enemies[num].transform.position = newPos;
        enemies[num].GetComponent<EnemyActionHandeler>().MoveEnemy(newPos, ith.items[num1], ith);        
        for (int g = 0; g < enemies.Count; g++)
        {
            if(enemies[g] == enemies[num]){
                enemies.RemoveAt(g);
                break;
            }       
        }
        
        
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        for (int o = 0; o < spawnPos.Length; o++)
        {
            Gizmos.DrawCube(spawnPos[o], spawnScale[o]);
        }
    }
}
