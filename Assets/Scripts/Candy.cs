using System;

namespace CandyCrush
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Candy : MonoBehaviour
    {
        private GameObject _selectTool;
        private float x, y;
        public bool isdofall = true;
       

        void Start()
        {
 _selectTool = GameObject.FindGameObjectWithTag("SelectTool");
        }

        public void AddNewLocation(float _x, float _y)
        {
            x = _x;
            y = _y;
        }

        // Update is called once per frame
        void Update()
        {
            if (isdofall)
            {
                if (transform.position.y - y < 0.05f)
                {
                    isdofall = false;
                    transform.position = new Vector3(x, y, 0f);
                }
                transform.position = Vector3.Lerp(transform.position,new Vector3(x,y,0f),Time.deltaTime * 3f);
            }
        }

        private void OnMouseDown()
        {
            _selectTool.transform.position = transform.position;
        }
    }

}