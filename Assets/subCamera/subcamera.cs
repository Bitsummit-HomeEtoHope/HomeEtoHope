using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subcamera : MonoBehaviour
{
    //�g��k������J����
    //Camera to zoom in and out
    public GameObject mainCanvas;
    //�g�厞�̃J����
    //Camera at magnification
    public Vector3 zoomInScale;
    //�k�����̃J����
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

        //�{�^���������ꂽ�Ƃ��Ɏ��s�����
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