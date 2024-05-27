using System.Collections.Generic;
using UnityEngine;

public class MoveBehavior : Behavior
{
    private MoveComp moveComp;
    private Vector2Int from, to;

    public MoveBehavior(Thing thing, Vector2Int from, Vector2Int to)
    {
        moveComp = (MoveComp)thing.GetComp(typeof(MoveComp));
        this.from = from;
        this.to = to;
    }


    public override void InitSteps()
    {
        steps = new List<Step>
        { new MoveStep(moveComp, new Vector2Int(0,0),new Vector2Int(1,0)),
         new MoveStep(moveComp, new Vector2Int(1,0),new Vector2Int(2,0)),
          new MoveStep(moveComp, new Vector2Int(2,0),new Vector2Int(3,0)),
           new MoveStep(moveComp, new Vector2Int(3,0),new Vector2Int(4,0)),
            new MoveStep(moveComp, new Vector2Int(4,0),new Vector2Int(5,0))
        };
    }
}