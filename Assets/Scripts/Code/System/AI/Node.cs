using System.Collections.Generic;

public class Node
{
    public enum ReturnState
    {
        Success,
        Running,
        Failure
    }
    
    protected Blackboard myBlackboard = null;
    public Node Parent = null;
    protected List<Node> myChildren = new List<Node>();

    public Node (List<Node> someChildren, Blackboard aBlackBoard)
    {
        myChildren = someChildren;
        myBlackboard = aBlackBoard;

        if (someChildren == null) return;

        foreach (Node child in myChildren)
        {
            if (child != null) child.Parent = this;
        }
    }

    public virtual ReturnState Evaluate() => ReturnState.Failure;
}