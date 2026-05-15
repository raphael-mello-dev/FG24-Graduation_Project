using System.Collections.Generic;

public class BehaviorTree
{
    public Node root;
    public Blackboard blackboard;

    public void UpdateTree() => root.Evaluate();
}

public class Blackboard
{
    public Dictionary<string, object> data;

    public Blackboard() => data = new Dictionary<string, object>();
}