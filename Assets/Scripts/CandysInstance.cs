using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace CandyCrush
{
    class CandysInstance : MonoBehaviour
    {
       public GameObject[] candysPrefab;
       public static Candy[,] currentCandys;
        public int widht;
        public int height;

        public void Start()
        { 
            currentCandys = new Candy[widht,height];
            
            for (int x = 0; x < widht; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    InstanceCandy(x,y);
                }
            }
        }

        public void InstanceCandy(int x, int y)
        {
            GameObject randomCandy = RandomCandy();
            GameObject newCandy = Instantiate(randomCandy, new Vector3(x, y+10f, 0f), quaternion.identity);
            Candy candy = newCandy.GetComponent<Candy>();
            candy.candyName = randomCandy.name;
            candy.AddNewLocation(x,y);
            currentCandys[x,y] = candy;

        }
        public GameObject RandomCandy()
        {
            int randX = Random.Range(0, candysPrefab.Length);
            return candysPrefab[randX];
        }

    }
    
}