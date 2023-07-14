using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Point_Teacher : MonoBehaviour
{
    [Header("your Ending")]
    //public float credits = 0f;
    [Header("will End_I -- numal")]
    public float credits_end_i = 0f;
    [Header("will End_II -- red(bad guys)")]
    public float credits_end_ii = 0f;
    [Header("will End_III -- flower(die)")]
    public float credits_end_iii = 0f;
    [Header("will End_IV -- building")]
    public float credits_end_iv = 0f;
    public bool for_credits_end_iv = false;
    [Header("will End_V -- factory")]
    public float credits_end_v = 0f;
    public bool for_credits_end_v = false;
    [Header("will End_VI -- farm")]
    public float credits_end_vi = 0f;
    public bool for_credits_end_vi = false;
    [Header("--will Pass-- if Bad Ending")]
    public float passScore = 100f;

    [Header("Level_states")]
    public LevelDataCurrent levelDataCurrent;
    [SerializeField]private int number_buildend;
    [SerializeField]private int number_humanend;

    void Start()
    {
        number_buildend = levelDataCurrent._theBuildNumber;
        number_humanend = levelDataCurrent._theHumanNumber;

    }

    public void AddPoints(float points ,float points_end_i, float points_end_ii, float points_end_iii, float points_end_iv, float points_end_v, float points_end_vi)
    {
        //credits += points;
        credits_end_i += points_end_i;
        credits_end_ii += points_end_ii;
        credits_end_iii += points_end_iii;
        credits_end_iv += points_end_iv;
        credits_end_v += points_end_v;
        credits_end_vi += points_end_vi;

        Debug.Log("Points added: " + points + " | Total credits: " + credits_end_i);
    }

    public void totalScore()
    {
        number_buildend = levelDataCurrent._theBuildNumber;
        number_humanend = levelDataCurrent._theHumanNumber;

        if (credits_end_iv - credits_end_v >= number_buildend && credits_end_iv - credits_end_vi >= number_buildend) for_credits_end_iv = true;
        if (credits_end_v - credits_end_iv >= number_buildend && credits_end_v - credits_end_vi >= number_buildend) for_credits_end_v = true;
        if (credits_end_vi - credits_end_iv >= number_buildend && credits_end_vi - credits_end_v >= number_buildend) for_credits_end_vi = true;

        if (credits_end_i < passScore) { SceneManager.LoadScene("Ending_bad"); Debug.Log("--BE--"); }
        if (credits_end_i >= passScore) { SceneManager.LoadScene("Ending_i"); Debug.Log("--nomal--"); }
        if (credits_end_i >= passScore) if (credits_end_ii >= number_humanend) { SceneManager.LoadScene("Ending_iii"); Debug.Log("--bad--"); }
        if (credits_end_i >= passScore) if (credits_end_ii < number_humanend) if (credits_end_iii >= number_humanend) { SceneManager.LoadScene("Ending_ii"); Debug.Log("--die--") ; }
        if (credits_end_i >= passScore) if (credits_end_ii < number_humanend) if (credits_end_iii < number_humanend) if (for_credits_end_iv) { SceneManager.LoadScene("Ending_iv"); Debug.Log("--building--"); }
        if (credits_end_i >= passScore) if (credits_end_ii < number_humanend) if (credits_end_iii < number_humanend) if (for_credits_end_v) { SceneManager.LoadScene("Ending_v"); Debug.Log("--factory--"); }
        if (credits_end_i >= passScore) if (credits_end_ii < number_humanend) if (credits_end_iii < number_humanend) if (for_credits_end_vi) { SceneManager.LoadScene("Ending_vi"); Debug.Log("--farm--"); }
    }
}
