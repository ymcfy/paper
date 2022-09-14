﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class mouse1 : MonoBehaviour {

        public Camera camera;
        public GameObject gameCheck;

        void Update()
        {

            //if (!EventSystem.current.IsPointerOverGameObject())
            //{
            //    if (Input.GetMouseButtonDown(0))
            //    {
            //        var ray = camera.ScreenPointToRay(Input.mousePosition);//鼠标的屏幕坐标转化为一条射线
            //        RaycastHit hit;

            //        //距离为5
            //        //if(Physics.Raycast(ray, out hit, 5)) {
            //        //    var hitObj = hit.collider.gameObject;
            //        //    Debug.Log(hitObj);
            //        //}
            //        //无距离限制
            //        if (Physics.Raycast(ray, out hit))
            //        {
            //            var hitObj = hit.collider.gameObject;
            //            SetObjectHighlight(hitObj);

            //        }
            //    }
            //}
        }

        /// <summary>
        /// 设置物体高亮
        /// </summary>
        /// <param name="obj"></param>
        public void SetObjectHighlight(GameObject obj)
        {
            if (gameCheck == null)
            {
                AddComponent(obj);
            }
            else if (gameCheck == obj)
            {
                RemoveComponent(obj);
            }
            else
            {
                RemoveComponent(gameCheck);
                AddComponent(obj);
            }
        }

        /// <summary>
        /// 移出组件
        /// </summary>
        /// <param name="obj"></param>
        public void RemoveComponent(GameObject obj)
        {
            if (obj.GetComponent<SpectrumController>() != null)
            {
                Destroy(obj.GetComponent<SpectrumController>());
            }

            if (obj.GetComponent<HighlightableObject>() != null)
            {
                Destroy(obj.GetComponent<HighlightableObject>());
            }

            gameCheck = null;
        }

        /// <summary>
        /// 添加高亮组件
        /// </summary>
        /// <param name="obj"></param>
        public void AddComponent(GameObject obj)
        {
            if (obj.GetComponent<SpectrumController>() == null)
            {
                obj.AddComponent<SpectrumController>();
            }
            gameCheck = obj;
        }
        public void delete()
        {
            if (gameCheck != null)
            {
                Destroy(gameCheck);
            }
        }
    }