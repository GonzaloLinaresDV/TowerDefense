using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int speed,life;
    public Transform start,end;
    public GameManager gameManager;
    public enum Type
    {
        fire,
        ice,
        earth
    }
    public Type type;

    void Start()
    {
        gameManager= FindObjectOfType<GameManager>();
        var color = gameObject.GetComponent<MeshRenderer>();
        switch (type)
        {
            case Type.fire:
                color.material.color = Color.red;
                break; 
            case Type.ice:
                color.material.color= Color.blue;
                break; 
            case Type.earth:
                color.material.color = Color.yellow;
                break;
        }
        start = gameManager.start;
        end = gameManager.end;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position=Vector3.MoveTowards(transform.position,end.position,speed*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "End")
        {
            Destroy(gameObject);
        }
        else if (other.tag == "Bullet")
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
