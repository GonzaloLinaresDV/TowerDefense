using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering;

public class FireTower : MonoBehaviour, ITowers
{
    GameManager gameManager;
    public int FOV;
    public int level;
    public GameObject bullet, nextLevel;
    public Transform spawnPoint,end;
    public int RPS;
    public FocusType focusType;
    public List<Enemy> allEnemies;
    
    public enum FocusType
    {
        first,
        last,
        moreHealth,
        LessHealth,

    }
    private void Start()
    {

        gameManager = GameManager.Instance;

        allEnemies = gameManager.allEnemy;
        StartCoroutine(ShootingCorrutine());
    }
    public void InstantiateBullet(Transform target)
    {
        var go=Instantiate(bullet, spawnPoint.transform.position,Quaternion.identity);
        go.GetComponent<Bullet>().SetBullet(target,2);
        Debug.Log("ATAQUE A ");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Upgrade();
        }
    }
    public void Upgrade()
    {
        if (level + 1 != 4)
        {
            level++;
            ChangeTowerAsset();
        }
    }
    void ChangeTowerAsset()
    {
        if (nextLevel != null)
        {
            Destroy(gameObject);
            Instantiate(nextLevel, transform.position, Quaternion.identity);
        }
    }
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
    public IEnumerable<Enemy> CanAttackEnemies() {       
        var attacableEnemies= allEnemies.Where(x => x.type == Enemy.Type.ice);
        Debug.Log(attacableEnemies);
        return attacableEnemies;
    }

    public bool IsOnFOV(IEnumerable <Enemy> enemyAttacable)
    {
        foreach (Enemy enemy in enemyAttacable)
        {
            return Vector2.Distance(transform.position, enemy.transform.position) < FOV;
        }
        return false;

    }
    IEnumerator ShootingCorrutine()
    {
        while (true)
        {
            if (IsOnFOV(CanAttackEnemies())&& focusType== FocusType.first)
            {
                InstantiateBullet(CanAttackEnemies().First().transform);
                yield return new WaitForSeconds(RPS);
            }
            else if (IsOnFOV(CanAttackEnemies()) && focusType== FocusType.last)
            {
                InstantiateBullet(CanAttackEnemies().Last().transform);
                yield return new WaitForSeconds(RPS);
            }
            else if (IsOnFOV(CanAttackEnemies()) && focusType == FocusType.moreHealth)
            {
                var orderByHeatlh = CanAttackEnemies().OrderByDescending(x => x.life);
                InstantiateBullet(orderByHeatlh.First().transform);
                yield return new WaitForSeconds(RPS);
            }
            else if (IsOnFOV(CanAttackEnemies()) && focusType == FocusType.LessHealth)
            {
                var orderByHeatlh=CanAttackEnemies().OrderBy(x=>x.life);
                InstantiateBullet(orderByHeatlh.First().transform);
                yield return new WaitForSeconds(RPS);
            }

            yield return null;
        }
    }
}
