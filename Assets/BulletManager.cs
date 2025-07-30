using UnityEngine;
using System.Collections;
using System.Numerics;
using NUnit.Framework;
using System.Collections.Generic;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class BulletManager : MonoBehaviour
{
    private static BulletManager bManagerInstance;
    private List<GameObject> bulletList;
    private static float cd = 0.5f;
    private static bool shot = false;

    BulletManager()
    {
        bulletList = new List<GameObject>();
    }

    public static BulletManager GetManagerInstance()
    {
        if (bManagerInstance == null)
        {
            bManagerInstance = new BulletManager();
            return bManagerInstance;
        }
        return bManagerInstance;
    }

    public void CreateBullet()
    {
        if (!shot)
        {
            GameObject bullet = GameObject.Find("bullet");
            GameObject player = GameObject.Find("player");
            GameObject bulletGroup = GameObject.Find("player bullets");

            GameObject bullet_instance = Instantiate(bullet, player.transform.position, Quaternion.identity, bulletGroup.transform);   //object to instantiate, position to instantiate, rotation, parent object
            bullet_instance.GetComponent<SpriteRenderer>().enabled = true;

            bulletList.Add(bullet_instance); //add to list for frame update
            shot = true;
        }
    }

    public void Update()
    {
        for (int c = 0; c < bulletList.Count; c++)
        {
            if (bulletList[c] != null)
            {
                bulletList[c].GetComponent<Rigidbody2D>().linearVelocity = Vector3.up * 3;
                bulletList[c].GetComponent<Rigidbody2D>().simulated = true;
                if (OutofBounds(bulletList[c]))
                {
                   bulletList[c].SetActive(false);
                   bulletList.Remove(bulletList[c]);
                   break;
                }
            }
        }
        if ((shot) && (cd > 0))
        {
            cd -= Time.deltaTime;
        }
        else if (cd <= 0)
        {
            cd = 0.5f;
            shot = false;
        }
    }

    //checks if bullet is outside playarea
    private bool OutofBounds(GameObject obj)
    {
        Bounds playarea_bounds = GameObject.Find("background").GetComponent<SpriteRenderer>().bounds;
        Bounds objBounds = obj.GetComponent<SpriteRenderer>().bounds;
        if (playarea_bounds.Intersects(objBounds) == false)
        {
            return true;
        }
        return false;
    }
}
