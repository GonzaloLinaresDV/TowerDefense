using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnTurret : MonoBehaviour
{
    public List<GameObject> Turrets = new List<GameObject>();
    BoxCollider myBox;


    private void Awake()
    {
        myBox = GetComponent<BoxCollider>();
    }
    private void OnMouseDown()
    {
        if(Input.GetMouseButtonDown(0)) {
            SpawnTorreta();
        }
    }
    void SpawnTorreta()
    {
        if (myBox.enabled)
        {
            Instantiate(Turrets[1], transform.position, Quaternion.identity);
            myBox.enabled = false;
        }
    }
}
