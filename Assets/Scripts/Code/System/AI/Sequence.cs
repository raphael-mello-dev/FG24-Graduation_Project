using System.Collections.Generic;

public class Sequence : Node
{
    public Sequence(List<Node> someChildren, Blackboard aBlackBoard) : base(someChildren, aBlackBoard) { }

    public override ReturnState Evaluate()
    {
        ReturnState aState = ReturnState.Failure;

        bool isRunning = false;

        foreach (Node child in myChildren)
        {
            switch (child.Evaluate())
            {
                case ReturnState.Success:
                    aState = ReturnState.Success;
                    continue;

                case ReturnState.Failure:
                    return aState;

                case ReturnState.Running:
                    isRunning = true;
                    aState = ReturnState.Running;
                    continue;
            }
        }

        if (isRunning) aState = ReturnState.Running;

        return aState;
    }
}