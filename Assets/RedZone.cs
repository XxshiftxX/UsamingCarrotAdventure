using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedZone : MonoBehaviour {

    public Transform player;
    public Collider2D collider;
    public float height = -2;
    public float speed = 0.2f;
    public float level = 2;
    public float levelIncrease = 1;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector2(player.position.x, height);
        height += speed * Time.deltaTime;
        if(level - height <= 0)
        {
            speed += 0.3f;
            level += levelIncrease;
            levelIncrease += 3f;
        }
	}

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.StartsWith("block"))
        {
            Destroy(collision.gameObject);
        }
    }
}
