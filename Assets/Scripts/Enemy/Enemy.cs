using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour , IDamageable
{
    public int speed,life;
    public GameManager gameManager;
    public EnemyPathManager pathManager;
    int idx;
    public enum Type
    {
        fire,
        ice,
        earth
    }
    public Type type;

    void Start()
    {
        pathManager=FindObjectOfType<EnemyPathManager>();
        gameManager= FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        GoTo();
    }

    void GoTo()
    {
        transform.position = Vector3.MoveTowards(transform.position, pathManager.path[idx].position, speed * Time.deltaTime);
        var distance= Vector3.Distance(transform.position, pathManager.path[idx].position);
        transform.LookAt(pathManager.path[idx].position);
        if(distance <= 0.1f) {
            idx++;
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "End")
        {
            gameManager.allEnemy.Remove(this);
            Destroy(gameObject);
        }
        else if (other.tag == "Bullet")
        {
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(int dmg)
    {
        life -= dmg;
        if (life <= 0)
        {
            gameManager.allEnemy.Remove(this);
            Destroy(this.gameObject);
        }
    }
}
