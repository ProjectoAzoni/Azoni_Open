using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class Collidable : MonoBehaviour
{
    public ContactFilter2D filter;
    public BoxCollider2D boxCollider1;
    private Collider2D[] hits = new Collider2D[10];
    public float tiempo;
       public string Scene;

    protected virtual void Start()
    {
        boxCollider1 = GetComponent<BoxCollider2D>();
    }

    
    protected virtual void Update()
    {
        //collision system
        boxCollider1.OverlapCollider(filter, hits);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;

            }
            OnCollide(hits[i]);

            hits[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D coll)
        
    {
        Debug.Log(coll.name);
        StartCoroutine(WaitFor(tiempo));
    }
    IEnumerator WaitFor(float time)
    {
        yield return new WaitForSeconds(time);
        SceneController.GoToScene(Scene);
    }
}
