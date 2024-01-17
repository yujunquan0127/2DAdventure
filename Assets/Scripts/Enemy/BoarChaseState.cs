using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarChaseState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.anim.SetBool("run",true);
    }

    public override void OnExit()
    {
        currentEnemy.lostTimeCounter = currentEnemy.lostTime;
        currentEnemy.anim.SetBool("run",false);
    }

    public override void PhysicsUpdate()
    {
    }

    public override void LogicUpdate()
    {
        if (currentEnemy.lostTimeCounter <= 0)
        {
            currentEnemy.SwitchState(NPCState.Patrol);
        }
        if(!currentEnemy.pysicsCheck.isGrounded || (currentEnemy.pysicsCheck.touchLeftWall && currentEnemy.faceDirection.x < 0) || (currentEnemy.pysicsCheck.touchRightWall && currentEnemy.faceDirection.x > 0))
        {
            currentEnemy.transform.localScale = new Vector3(currentEnemy.faceDirection.x,1,1);
        }
    }
}
