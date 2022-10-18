using System;
using UnityEditor;
using UnityEngine.Serialization;

namespace CandyCrush
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Candy : MonoBehaviour
    {
        public static Candy firstInput;
        public static Candy secondInput;
        private GameObject _selectTool;
        public Vector3 targetLocation;
        public int x;
        public int y;
        public bool isdofall = true;
        public bool isMoving = false;
        public List<Candy> xAxisInCandys;
        public List<Candy> yAxisInCandys;
        public string candyName;
        public Animator animation;


        void Start()
        {
          _selectTool = GameObject.FindGameObjectWithTag("SelectTool");
          animation = GetComponent<Animator>();

        }

        public void AddNewLocation(float _x, float _y)
        {
            x = (int)_x;
            y = (int)_y;
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
                transform.position = Vector3.Lerp(transform.position,new Vector3(x,y,0f),Time.deltaTime * 2f);
            }

            if (isMoving)
            {
                LocationMove();
            }
        }

        private void OnMouseDown()
        {
            _selectTool.transform.position = transform.position;
             CandyControl();
        }

        public void CandyControl()
        {
            if (firstInput == null)
            {
                firstInput = this;
            }
            else
            {
                secondInput = this;
                if (firstInput != secondInput)
                {
                    float _differenceX = Mathf.Abs(firstInput.x - secondInput.x);
                    float _differenceY = Mathf.Abs(firstInput.y - secondInput.y);
                    if (_differenceX + _differenceY == 1)
                    {
                        Debug.Log("yer değişsin");
                        firstInput.targetLocation = secondInput.transform.position;
                        secondInput.targetLocation = firstInput.transform.position;
                        firstInput.isMoving = true;
                        secondInput.isMoving = true;
                        ChangeVeriable();
                        firstInput.XAxisControls();
                        firstInput.YAxisControls();
                        secondInput.XAxisControls();
                        secondInput.YAxisControls();
                        StartCoroutine(firstInput.Destroy());
                        StartCoroutine(secondInput.Destroy());
                    }
                    else
                    {
                        firstInput = secondInput;
                    }
                }
                secondInput = null;
            }
        }

        public void ChangeVeriable()
        {
            CandysInstance.currentCandys[(int)firstInput.x, (int)firstInput.y] = secondInput;
            CandysInstance.currentCandys[(int)secondInput.x, (int)secondInput.y] = firstInput;
            float _firstInputX = firstInput.x;
            float _firstInputY = firstInput.y;

            firstInput.x = secondInput.x;
            firstInput.y = secondInput.y;

            secondInput.x =(int) _firstInputX;
            secondInput.y =(int) _firstInputY;
        }

        public void LocationMove()
        {
            transform.position = Vector3.Lerp(transform.position, targetLocation, 0.02f);
        }

        public void XAxisControls()
        {
            for (int i = x+1; i < CandysInstance.currentCandys.GetLength(0); i++)
            {
                Candy candyRight = CandysInstance.currentCandys[i,y];
                if (candyName == candyRight.candyName)
                {
                    xAxisInCandys.Add(candyRight);
                }
                else
                {
                    break;
                }
            }
            for (int i = x-1; i >= 0; i--)
            {
                Candy candyLeft = CandysInstance.currentCandys[i,y];
                if (candyName == candyLeft.candyName)
                {
                    xAxisInCandys.Add(candyLeft);
                }
                else
                {
                    break;
                }
            }
            
        }

        public void YAxisControls()
        {
            for (int i =y+1; i < CandysInstance.currentCandys.GetLength(1); i++)
            {
                Candy candyDown = CandysInstance.currentCandys[x,i];
                if (candyName == candyDown.candyName)
                {
                        yAxisInCandys.Add(candyDown);
                }
                else
                {
                        break;
                }
            }
            for (int i = y-1; i >= 0; i--) 
            { 
                Candy candyUp = CandysInstance.currentCandys[x,i];
                if (candyName == candyUp.candyName)
                {
                        yAxisInCandys.Add(candyUp);
                }
                else
                {
                        break;
                }
            }
        }

        IEnumerator Destroy()
        {
            yield return new WaitForSeconds(0.3f);
            if (xAxisInCandys.Count >= 2 || yAxisInCandys.Count >= 2)
            {
                animation.SetBool("isDestroy",true);
                
                if (xAxisInCandys.Count >= 2)
                {
                    foreach (var item in xAxisInCandys)
                    {
                        item.animation.SetBool("isDestroy",true);
                    }
                }
                else
                { 
                    foreach (var item in yAxisInCandys)
                    {
                        item.animation.SetBool("isDestroy",true);
                    }
                }
            }
            
        }

        public void AnimasyonDestroy()
        {
            Destroy(gameObject);
            Debug.Log("kayboldu");
        }
    }
    

}