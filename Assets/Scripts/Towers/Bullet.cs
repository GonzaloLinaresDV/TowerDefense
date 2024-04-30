using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform target;
    Vector3 myTargetPos;
    bool enterCorrutine;
    int dmg;
    public enum Type
    {
        fire,
        ice,
        water,
    }
    public Type type;
    public void SetBullet(Transform target,int dmg)
    {
        this.target= target;
        this.dmg=dmg;
    }
    private void Start()
    {
        var color = gameObject.GetComponent<MeshRenderer>();
        switch (type)
        {
            case Type.fire:
                color.material.color = Color.red;
                break;
            case Type.ice:
                color.material.color = Color.blue;
                break;
            case Type.water:
                color.material.color = Color.yellow;
                break;
        }
    }

    private void Update()
    {
        if (target! != null)
        {
            myTargetPos= target.position;
            transform.position = Vector3.MoveTowards(transform.position, target.position, 5f * Time.deltaTime);
        }
        else
        {
            if (!enterCorrutine)
            {
                StartCoroutine(DestroyBullet());
            }
        }
    }

    IEnumerator DestroyBullet()
    {
        enterCorrutine = true;
        yield return new WaitForSeconds(.1f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDamageable>() != null) {
            
            other.GetComponent<Enemy>().TakeDamage(dmg);
            Destroy(gameObject);
        }
    }

}
