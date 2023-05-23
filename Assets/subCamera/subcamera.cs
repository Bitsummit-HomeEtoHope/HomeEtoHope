using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subcamera : MonoBehaviour
{
    //拡大縮小するカメラ
    //Camera to zoom in and out
    public GameObject mainCanvas;
    //拡大時のカメラ
    //Camera at magnification
    public Vector3 zoomInScale;
    //縮小時のカメラ
    //Camera at reduced size
    public Vector3 zoomOutScale;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnButtonPreessed()
    {

        //ボタンが押されたときに実行される
        //Executed when the button is pressed
        if (mainCanvas.transform.localScale==zoomInScale)
        {
            mainCanvas.transform.localScale = zoomOutScale;
        }
        else
        {
                mainCanvas.transform.localScale = zoomInScale;
        }
    }
        
 }