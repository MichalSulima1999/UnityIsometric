using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject item;
    private Transform player;
    private float randx;
    private float randz;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SpawnDroppedItem() {
        randx = Random.Range(-2f, 2f);
        randz = Random.Range(-2f, 2f);
        if(randx < 0.5f && randx > -0.5f) {
            randx += 1;
        }
        if (randz < 0.5f && randz > -0.5f) {
            randz += 1;
        }
        Vector3 playerPos = new Vector3(player.position.x + randx, player.position.y, player.position.z + randz);
        Instantiate(item, playerPos, item.gameObject.transform.rotation);
    }
}
