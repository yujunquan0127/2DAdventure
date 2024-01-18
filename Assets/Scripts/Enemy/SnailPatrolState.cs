using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
    }

    public override void OnExit()
    {
        currentEnemy.anim.SetBool("walk",false);
    }

    public override void PhysicsUpdate()
    {
    }

    public override void LogicUpdate()
    {
        if (currentEnemy.FoundPlayer())
        {
            // currentEnemy.SwitchState(NPCState.Skill);
        }
        if(!currentEnemy.pysicsCheck.isGrounded || (currentEnemy.pysicsCheck.touchLeftWall && currentEnemy.faceDirection.x < 0) || (currentEnemy.pysicsCheck.touchRightWall && currentEnemy.faceDirection.x > 0))
        {
            currentEnemy.wait = true;
            currentEnemy.anim.SetBool("walk",false);
        }
        else
        {
            currentEnemy.anim.SetBool("walk",true);
        }
    }
}
