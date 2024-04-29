using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Enemy> enemyList;
    public Transform start, end;
    public List<FireTower> fireTowers;
    public List<Enemy> allEnemy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InstanseEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator InstanseEnemy()
    {
        while (true)
        {
            var go=Instantiate(enemyList[Random.RandomRange(0,enemyList.Count)],start.transform.position,Quaternion.identity);
            foreach(FireTower x in fireTowers) {
                allEnemy.Add(go);
            }
            yield return new WaitForSeconds(2f);
            
        }
    }
}
