using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonManager<GameManager>
{
    public LevelDataCurrent levelDataCurrent;
    [Tooltip("Temporarily replace the shady screen at the end of the level")]
    public GameObject shady;
    [SerializeField]
    public bool isPause;


    // Start is called before the first frame update
    void Start()
    {
        // levelDataCurrent = FindObjectOfType<LevelDataCurrent>();
        if (isPause)
        {
            Time.timeScale = 0;
            // ShadyOpen();
        }
    }

    public void ShadyOpen()
    {
        shady.SetActive(true);
    }

    public void ShadyClose()
    {
        //shady.SetActive(false);
        isPause = false;
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ¼ì²âÊó±ê×ó¼üµã»÷
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                    ShadyClose();
            }
        }
    }
}
