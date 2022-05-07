using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootManager : MonoBehaviour
{
    public GameObject collisionParticle, shootParticle;
    public int damage = 0;
    public float range = 50f;
    public float fireRate = 4f;
    float fireTime = 0f;
    ParticleSystem shootParticle0;

    // Start is called before the first frame update
    void Start()
    {
        shootParticle0 = shootParticle.GetComponent<ParticleSystem>();
    }

    // Update is not used
    void Update()
    {
        
    }
    public void PlayerShoot(){
        
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range) && Time.time >= fireTime)
        {
            shootParticle0.Play();
            fireTime = Time.time + 1f / fireRate;
            EnemyCollisionHandeler ech = hit.transform.GetComponent<EnemyCollisionHandeler>();
            if (ech != null)
            {
                ech.TakeDamage(damage);
            }
            GameObject ptle = Instantiate(collisionParticle, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(ptle, 0.5f);
        }
    }
}
