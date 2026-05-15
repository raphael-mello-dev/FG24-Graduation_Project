using System.Collections.Generic;

public class Selector : Node
{
    public Selector(List<Node> someChildren, Blackboard aBlackBoard) : base(someChildren, aBlackBoard) { }

    public override ReturnState Evaluate()
    {
        ReturnState aState = ReturnState.Failure;

        foreach (Node child in myChildren)
        {
            switch (child.Evaluate())
            {
                case ReturnState.Success:
                    aState = ReturnState.Success;
                    return aState;

                case ReturnState.Failure:
                    continue;

                case ReturnState.Running:
                    aState = ReturnState.Running;
                    return aState;
            }
        }

        return aState;
    }
}