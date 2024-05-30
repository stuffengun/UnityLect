using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Sheep : NonPlayer
{
    public override string Name => "Sheep";

    public override void OnInstantiate()
    {
        base.OnInstantiate();
        AddComp(new MoveComp(this, 30));
    }

    protected override void InitState()
    {
        //30%확률로 대기 70퍼 확률로 이동 
        //이동못하면 대기

        Vector2Int? dest = ThingSystem.Instance.GetRandomEmptyTile(Pos, 30);
        if(dest==null || Random.Range(0,1f)<0.3f)//이동불가거나 30퍼
        {
            curState = 0;//idle
            BehaviorComp.SetBehavior(new IdleBehavior(Random.Range(30, 180)));
        }

        else//70퍼
        {
            List<Vector2> path;
          
            curState = 1;//move
            BehaviorComp.SetBehavior(new MoveBehavior(this, Pos, dest.Value));
        }
    }

    protected override void NextState()
    {
        if(curState==1)
        {
            curState = 0;//idle
            BehaviorComp.SetBehavior(new IdleBehavior(Random.Range(30, 180)));
        }
        else
        {
            InitState();
        }
    }

}


