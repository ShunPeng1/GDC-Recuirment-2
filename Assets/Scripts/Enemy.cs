using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{
    [SerializeField] private Transform player;
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody rb;
    private Vector3 movement;

    // Start is called before the first frame update
    void Start(){
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update(){
        Vector3 direction = player.position - transform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion angle = Quaternion.LookRotation(direction);
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
    }
    private void FixedUpdate() {
        moveCharacter(movement);
    }
    void moveCharacter(Vector3 direction){
        rb.MovePosition((Vector3)transform.position + (direction * (moveSpeed * Time.deltaTime)));
    }
}