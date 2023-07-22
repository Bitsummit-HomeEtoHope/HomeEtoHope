using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Smoke_front : MonoBehaviour
    {
        public GameObject player;
        public GameObject finishEffect;
        
        private LevelDataCurrent levelDataCurrent;
//        private LevelData levelData;
        private float buildTime=0f;

        private void Start()
        {
            
            
        }
        private void Update()
         {
            buildTime+=Time.deltaTime;
            if(buildTime> levelDataCurrent._levelData._buildTime)
            {
                finishEffect.SetActive(true);
                buildTime=0f;
                this.gameObject.SetActive(false);
                
                
            }
       //     Debug.Log(buildTime);
            
        }
    }

