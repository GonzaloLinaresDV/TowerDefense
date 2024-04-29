using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ElectricTower : MonoBehaviour
{
    public int FOV;
    public int level;
    public GameObject bullet;
    public Transform spawnPoint, end;
    public List<Mesh> towerAssets;
    MeshFilter myMesh;
    Material myMaterial;
    public List<Enemy> allEnemies;

    private void Start()
    {
        myMesh = GetComponent<MeshFilter>();
        myMaterial = GetComponent<MeshRenderer>().materials[0];
        myMaterial.color = Color.yellow;
    }
    public void Attack()
    {
        if (IsOnFOV(CanAttackEnemies()))
        {
            InstantiateBullet();
        }
    }
    public void InstantiateBullet()
    {
        var go = Instantiate(bullet, spawnPoint.transform.position, Quaternion.identity);
        go.GetComponent<Bullet>().SetBullet(CanAttackEnemies().First().transform,2);
        Debug.Log("ATAQUE A ");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Upgrade();
        }
        CanAttackEnemies();
        Attack();
    }
    public void Upgrade()
    {
        if (level + 1 != 4)
        {
            level++;
            ChangeTowerAsset(level);
        }
    }
    void ChangeTowerAsset(int lvl)
    {
        myMesh.mesh = towerAssets[lvl];
    }
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
    public IEnumerable<Enemy> CanAttackEnemies()
    {
        var attacableEnemies = allEnemies.Where(x => x.type != Enemy.Type.fire);
        Debug.Log(attacableEnemies);
        return attacableEnemies;
    }
    public List<Enemy> GetCloserToExit()
    {
        var closer = allEnemies.OrderBy(x => Vector3.Distance(x.transform.position, end.position));
        return closer.ToList();
    }






    public bool IsOnFOV(IEnumerable<Enemy> enemyAttacable)
    {
        foreach (Enemy enemy in enemyAttacable)
        {
            return Vector2.Distance(transform.position, enemy.transform.position) < FOV;
        }
        return false;

    }
}
