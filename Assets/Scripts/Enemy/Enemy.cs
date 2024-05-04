using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour , IDamageable
{
    public Camera camera;
    public int speed,life, idx;
    public TextMeshPro myHealtTXT;
    public GameManager gameManager;
    public EnemyPathManager pathManager;
    public Transform myTransform;
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
        camera = FindObjectOfType<Camera>();
        life = Random.Range(1, 5);
        myHealtTXT.text=life.ToString();
        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(camera.transform.position);
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
        myHealtTXT.text=life.ToString();
    }
}
