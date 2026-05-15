using System.Collections.Generic;
using UnityEngine;

public class ActionStack
{
    public interface IAction
    {
        void OnBegin(bool bIsFirstTime);
        void OnUpdate();
        void OnEnd();
        bool IsDone();
    }

    public abstract class Action : IAction
    {
        public virtual bool IsDone() { return true; }

        public virtual void OnBegin(bool bIsFirstTime) { }

        public virtual void OnEnd() { }

        public virtual void OnUpdate() { }

        public override string ToString() { return GetType().Name; }
    }

    public abstract class ActionBehaviour : MonoBehaviour, IAction
    {
        public virtual bool IsDone() { return true; }

        public virtual void OnBegin(bool bIsFirstTime) { }

        public virtual void OnEnd() { }

        public virtual void OnUpdate() { }

        public override string ToString() { return GetType().Name; }
    }

    public abstract class ActionObject : ScriptableObject, IAction
    {
        public virtual bool IsDone() { return true; }

        public virtual void OnBegin(bool bIsFirstTime) { }

        public virtual void OnEnd() { }

        public virtual void OnUpdate() { }

        public override string ToString() { return GetType().Name; }
    }

    private List<IAction> actionStack = new List<IAction>();
    private HashSet<IAction> firstTimeActions = new HashSet<IAction>();
    private IAction currentAction;

    public List<IAction> Stack => actionStack;
    public IAction CurrentAction => currentAction;
    public bool IsEmpty => currentAction == null && actionStack.Count == 0;

    public void PushAction(IAction anAction)
    {
        if (anAction != null)
        {
            actionStack.RemoveAll(a => a == anAction);
            actionStack.Insert(0, anAction);

            if (currentAction != null && currentAction != anAction)
                currentAction = null;
        }
    }

    public virtual void UpdateActions()
    {
        if (IsEmpty) return;

        while (currentAction == null && actionStack.Count > 0)
        {
            currentAction = actionStack[0];

            bool bFirstTime = !firstTimeActions.Contains(currentAction);
            firstTimeActions.Add(currentAction);
            currentAction.OnBegin(bFirstTime);
        }

        if (currentAction != null)
        {
            if (actionStack.Count > 0 && currentAction != actionStack[0])
            {
                currentAction = null;
                UpdateActions();
                return;
            }
        }

        if (currentAction != null)
        {
            currentAction.OnUpdate();
            
            if (actionStack.Count > 0 && currentAction == actionStack[0])
            {
                if (currentAction.IsDone())
                {
                    actionStack.RemoveAt(0);
                    currentAction.OnEnd();
                    firstTimeActions.Remove(currentAction);
                    currentAction = null;
                }
            }
            else
                currentAction = null;
        }
    }
}