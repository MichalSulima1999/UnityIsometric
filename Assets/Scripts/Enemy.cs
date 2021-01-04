using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 4f;
    public float maxDist = 10f;
    public float minDist = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);

        if(Vector3.Distance(transform.position, player.position) >= minDist) {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;

            
        } else if (Vector3.Distance(transform.position, player.position) <= maxDist) {

        }
    }
}
