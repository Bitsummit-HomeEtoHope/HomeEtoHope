using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Tutorial of FSM (Check it in wiki)
//It's a class that implements the functions of interface in "IState", plz don't modify "IState"
//If you want to creat a new state, creat a script named "XXState" in the folder State
//The format is like the following Content:

//  public class XXState : IState    (If there is an error, don't be afraid, just move your mouse over where the error occur and press "Alt + Enter" and choose the first option)
//  {
//      private Parameter parameter;
//      private FSM manager;
//      
//      public XXState(FSM manager)
//          {
//              this.manager = manager;
//              parameter = manger.parameter;
//          }
//      public void Onenter()
//      {
//          *Here is the function that need to be done when the state exit.
//      }
//      
//      public void OnUpdate()
//      {
//          *Here is the main logic of the state that it will run in Update(in the script "FSM") per frame.
//      }
//      
//      public void OnExit()
//      {
//          *Here is the function that need to be done when the state exit.   
//      }
//  }

//  I greatly recommend you refer to my code!
//  Everyone who cannot understand just @me to ask questions!! :) 
#endregion

public class PatrolState : IState
{
    private Parameter parameter;
    private FSM manager;

    public PatrolState(FSM manager)
    {
        this.manager = manager;
        parameter = manager.parameter;
    }
    
    public void Onenter()
    {
        GetTarget();
    }

    public void OnUpdate()
    {
        //Move To Target
        if (parameter.currentTarget != null)
            manager.transform.position = Vector3.MoveTowards(manager.transform.position, 
                parameter.currentTarget, parameter.moveSpeed*Time.deltaTime);
        
        //Switch Target
        if (Vector2.Distance(manager.transform.position, parameter.currentTarget) < 0.1f)
            manager.TransitState(StateType.Idle);
    }

    public void OnExit()
    {
        
    }

    //private void GetTarget()
    //{
    //    var a = Random.Range(0, parameter.patrolPoints.Length);

    //    //Get Random Move Direction
    //    var randomDir = new Vector3(Random.Range(-parameter.patrolRadius, parameter.patrolRadius),
    //        Random.Range(-parameter.patrolRadius, parameter.patrolRadius)).normalized;
    //    //Set currentTarget
    //    parameter.currentTarget = parameter.patrolPoints[a].position + randomDir*parameter.patrolRadius;
    //}
    private void GetTarget()
    {
        if (parameter.patrolPoints == null || parameter.patrolPoints.Length == 0)
        {
            Debug.LogError("Patrol points array is empty or not initialized!");
            return;
        }

        var a = Random.Range(0, parameter.patrolPoints.Length);

        if (a >= parameter.patrolPoints.Length)
        {
            Debug.LogError("Invalid index for patrol points array!");
            return;
        }

        // Get Random Move Direction
        var randomDir = new Vector3(Random.Range(-parameter.patrolRadius, parameter.patrolRadius),
            Random.Range(-parameter.patrolRadius, parameter.patrolRadius)).normalized;

        // Set currentTarget
        parameter.currentTarget = parameter.patrolPoints[a].position + randomDir * parameter.patrolRadius;
    }

}
