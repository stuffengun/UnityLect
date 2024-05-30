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
        //30%Ȯ���� ��� 70�� Ȯ���� �̵� 
        //�̵����ϸ� ���

        Vector2Int? dest = ThingSystem.Instance.GetRandomEmptyTile(Pos, 30);
        if(dest==null || Random.Range(0,1f)<0.3f)//�̵��Ұ��ų� 30��
        {
            curState = 0;//idle
            BehaviorComp.SetBehavior(new IdleBehavior(Random.Range(30, 180)));
        }

        else//70��
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


