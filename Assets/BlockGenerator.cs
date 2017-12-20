using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BlockGenerator : MonoBehaviour {

    public float _generatedXPos = 0;
    public float _generatedYPos = 1.5f;
    public float _generatedSize = 1;

    public int healpackCount = 0;
    public int healpackNextPosition = 10;

    public int height = 1;
    
    public GameObject PlayerObject;
    public GameObject BlockObject;
    public GameObject HealpackObject;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if((int)PlayerObject.transform.position.y + 8 > _generatedYPos)
        {
            GenerateBlock();
        }
	}

    void GenerateBlock()
    {
        bool isSatisfyCondition = false;
        do
        {
            float xPos = _generatedXPos + Random.Range(-2, 2f) * 1.2f;
            float yPos = _generatedYPos + 3.6f;
            float size = Random.Range(3, 7);

            if(
                (xPos + size / 2) - (_generatedXPos + _generatedSize / 2) > 1 ||
                (_generatedXPos - _generatedSize / 2) - (xPos - size / 2) > 1
                )
            {
                isSatisfyCondition = true;
                _generatedXPos = xPos;
                _generatedYPos = yPos;
                _generatedSize = size;
            }
        } while (!isSatisfyCondition);

        GameObject block = Instantiate(BlockObject);
        SpriteRenderer blockSprite = block.GetComponent<SpriteRenderer>();
        BoxCollider2D blockCollider = block.GetComponent<BoxCollider2D>();

        block.transform.position = new Vector2(_generatedXPos, _generatedYPos);
        blockSprite.size = new Vector2(_generatedSize, 1);
        blockCollider.size = new Vector2(_generatedSize, 1);

        block.name = "block" + height++.ToString();

        if(++healpackCount == healpackNextPosition)
        {
            healpackNextPosition += Random.Range(9, 12);
            GameObject healpack = Instantiate(HealpackObject);
            float healpackPosX = _generatedXPos + Random.Range(-(_generatedSize / 2f), _generatedSize / 2f);
            healpack.transform.position = new Vector2(healpackPosX, _generatedYPos + 1f);
            healpack.name = "케토톱";
        }
    }
}
