using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireTower : MonoBehaviour, ITowers
{
    GameManager gameManager;
    public int FOV;
    public int level;
    public GameObject bullet;
    public Transform spawnPoint,end;
    public List<Mesh> towerAssets;
    MeshFilter myMesh;
    Material myMaterial;
    public List<Enemy> allEnemies;

    private void Start()
    {
        myMesh= GetComponent<MeshFilter>();
        myMaterial= GetComponent<MeshRenderer>().materials[0];
        myMaterial.color= Color.red;
        gameManager = GameManager.Instance;

        allEnemies = gameManager.allEnemy;
        
    }
    public void Attack()
    {
        InstantiateBullet();        
    }
    public void InstantiateBullet()
    {
        var go=Instantiate(bullet, spawnPoint.transform.position,Quaternion.identity);
        go.GetComponent<Bullet>().SetBullet(CanAttackEnemies().First().transform,2);
        Debug.Log("ATAQUE A ");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Upgrade();
        }
        if (IsOnFOV(CanAttackEnemies()))
        {
            Attack();
        }
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
}
