using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitStep : Step
{
    // Start is called before the first frame update

    private int waitTick;
    public WaitStep(int waitTick)
    {
        this.waitTick = waitTick;
    }

    public override bool IsCanceled()
    { return false; }
    public override bool IsFinished()=>
        waitTick <= 0;

    public override void Tick()
    {
        base.Tick();
        waitTick--;
    }

}
