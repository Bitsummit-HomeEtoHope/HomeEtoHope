using UnityEngine;

public class Check_HumanDistance : MonoBehaviour
{
    public float check_Distance = 1f;
    public float check_Timer = 1f;
    public float check_Time = 0f;
    public float isBuildTimer = 0.15f;
    public float isBuildTime = 0f;
    [SerializeField]
    public bool isBuild = false;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (Vector2.Distance(player.transform.position, transform.position) < check_Distance)
            {
                isBuildTime += Time.deltaTime;
                if (isBuildTime > isBuildTimer)
                {
                    isBuild = true;
                }
            }
        }
    }

    private void Update()
    {
        if (isBuild)
        {
            check_Time += Time.deltaTime;
            if (check_Time > check_Timer)
            {
                GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        else
        {
            check_Time = 0f;
        }
    }
}
