using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    public List<GameObject> bag = new List<GameObject>();
    public GameFloat minItemDropRadius;
    public float minItemDropRadiust;
    public GameFloat maxItemDropRadius;
    //public float maxItemDropRadius;
    public bool empty = true;

    private void Start()
    {
        if(bag.Count > 0)
        {
            empty = false;
        }
    }

    public void DropItems()
    {
        if (!empty)
        {
            //drop all items from bag 

            //get player position
            Vector3 ppos = gameObject.transform.position;

            //iterate through bag


            for (int i = 0; i < bag.Count; i++)
            {
                //get random x and y near the player
                float rx = Random.Range(minItemDropRadius.value(), maxItemDropRadius.value());
                float ry = Random.Range(minItemDropRadius.value(), maxItemDropRadius.value());
                int coin = Random.Range(0, 2);
                if (coin == 0)
                {
                    coin = -1;
                }
                else
                {
                    coin = 1;
                }
                Vector3 itemDropLoc = new Vector3(ppos.x + (coin * rx), ppos.y + (coin * ry), 0f);
                //unparent item from player
                GameObject currentItem = bag[i];
                currentItem.transform.parent = null;
                currentItem.transform.position = itemDropLoc;
                currentItem.GetComponent<Collider2D>().enabled = true;
                currentItem.SetActive(true);
            }

            empty = true;
        }


    }
}
