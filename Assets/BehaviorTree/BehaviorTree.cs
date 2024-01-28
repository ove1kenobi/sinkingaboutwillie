using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
	public enum Status
	{
		Success,
		Failure,
		Running
	}
    
	public class BehaviorTree : MonoBehaviour
	{
		public string BehaviorTreeFileName;

	    private Node root;
	    private Node current;
	
	        
	    // Start is called before the first frame update
	    void Start()
	    {
			// read init file
			string[] lines = File.ReadAllLines("Assets/BehaviorTree/" + BehaviorTreeFileName + ".txt");
			int currentLevel = 0;

			// generate behavior tree
			foreach (string line in lines)
			{
				string[] indents = line.Split("\t");
				int level = indents.Length - 1;
				int levelDiff = level - currentLevel;
				currentLevel += levelDiff;
				while (levelDiff < 0)
				{
					current = current.Parent; levelDiff++;
				}

				string[] node_desc = indents[^1].Split(": ");
				string nodeType = node_desc[0];
				string desc = node_desc[1];
				Node node = null;
				switch (nodeType)
				{
					case "root":
						root = new Repeater(null, desc);
						node = root;
						break;
					case "sequence":
						node = new Sequence(current, desc);
						current.AddChild(node);
						break;
					case "selector":
						node = new Selector(current, desc);
						current.AddChild(node);
						break;
					case "succeeder":
						node = new Succeeder(current, desc);
						current.AddChild(node);
						break;
					case "inverter":
						node = new Inverter(current, desc);
						current.AddChild(node);
						break;
					case "repeater":
						node = new Repeater(current, desc);
						current.AddChild(node);
						break;
					case "condition":
						switch (desc)
						{
							case "grabbed by ricky":
								node = new Grabbed(current, desc);
								break;
							case "in the air":
								node = new InAir(current, desc);
								break;
							case "close to helm":
								node = new NextToHelm(current, desc);
								break;
							case "on ground":
								node = new OnGround(current, desc);
								break;
						}
						current.AddChild(node);
						break;
					case "action":
						switch (desc)
						{
							case "idle":
								node = new Idle(current, desc);
								break;
							case "flail limbs":
								node = new Flail(current, desc);
								break;
							case "attack helm":
								node = new AttackHelm(current, desc);
								break;
							case "set goal":
								node = new SetGoal(current, desc);
								break;
							case "move toward goal":
								node = new MoveToGoal(current, desc);
								break;
						}
						current.AddChild(node);
						break;
				}
				current = node;
			}

			current = root;
        }
	
	    // Update is called once per frame
	    void Update()
	    {

	       // if running call process on current 

	    }
	}
	
	public abstract class Node
	{
		protected Node parent;
		protected string description;
	    // success
	    // failure
	    // running

		public Node Parent => parent;
		public string Description => description;

		public void Print()
		{
			Debug.Log(description);
		}

		public abstract Node GetChild();
		public abstract Node NextNode();
		public abstract void AddChild(Node child);
		public abstract Status Process();
	}
	
	/*************************
	 *   Composite Nodes
	 *************************/
	public abstract class Composite : Node
	{
		protected List<Node> children = new List<Node>();
		protected int current = 0;

		public abstract override Status Process();

        public override Node GetChild()
        {
            Node child = children[current];
			current = (current + 1) % children.Count;
			return child;
        }
        public override Node NextNode()
        {
            if (current < children.Count)
			{
				return children[current++];
			}
			else
			{
				current = 0; return parent;
			}
        }
        public override void AddChild(Node child)
		{
			children.Add(child);
		}
	}

	public class Sequence : Composite
	{
		public Sequence(Node parent, string description)
		{
			this.parent = parent;
			this.description = description;
		}
		
		public override Status Process()
		{
		// if current == success process next
		// at first fail return fail
		// else return success
			return Status.Running;
		}
	}
	
	public class Selector : Composite
	{
		public Selector(Node parent, string description)
		{
			this.parent = parent;
			this.description = description;
		}
		
		public override Status Process()
		{
		// if current == fail process next
		// at first success return success
		// else return fail
			return Status.Running;
		}
	} 
	
	
	/*************************
	 *   Decorator Nodes
	 *************************/
	public abstract class Decorator : Node
	{
		protected Node child;
		protected bool visited = false;

        public override Node GetChild()
        {
			return child;
        }
        public override Node NextNode()
        {
			if (visited)
			{
				visited = false;
				return parent;
			}
            else
			{
				visited = true;
				return child;
			}
        }
        public override void AddChild(Node child)
        {
            this.child = child;
        }

        public abstract override Status Process();
	}

	public class Succeeder : Decorator
	{
		public Succeeder(Node parent, string description)
		{
			this.parent = parent;
			this.description = description;
		}
		
		public override Status Process()
		{
		// process child
		// return success
			return Status.Success;
		}
	}
	
	public class Inverter : Decorator
	{
		public Inverter(Node parent, string description)
		{
			this.parent = parent;
			this.description = description;
		}
		
		public override Status Process()
		{
		// process child
		// return inverse of child
			return Status.Running;
		}
	}
	
	public class Repeater : Decorator
	{
		public Repeater(Node parent, string description)
		{
			this.parent = parent;
			this.description = description;
		}
		public override Status Process()
		{
		// on child success/failure repeat process
			return Status.Running;
		}
	}


	/*************************
	 *   Leaf Nodes
	 *************************/
	public abstract class Leaf : Node
	{
        public override Node GetChild()
        {
            throw new System.NotImplementedException();
        }
        public override void AddChild(Node child)
        {
            throw new System.NotImplementedException();
        }
        public override Node NextNode()
        {
			return Parent;
        }
        public override abstract Status Process();
	}

	public abstract class Condition : Leaf
	{
		public override abstract Status Process();
	}

	public abstract class Action : Leaf
	{
		public override abstract Status Process();
	}
	/*
	 *    The Actual Leafs
	 */

	public class Grabbed : Condition
	{
		public Grabbed(Node parent, string description)
		{
			this.parent = parent; this.description = description;
		}
        public override Status Process()
        {
            return Status.Success;
        }
    }
	public class InAir : Condition
	{
		public InAir(Node parent, string description)
		{
			this.parent = parent; this.description = description;
		}
        public override Status Process()
        {
            return Status.Success;
        }
	}
	public class NextToHelm : Condition
	{
		public NextToHelm(Node parent, string description)
		{
			this.parent = parent; this.description = description;
		}
        public override Status Process()
        {
            return Status.Success;
        }
	}
	public class OnGround : Condition
	{
		public OnGround(Node parent, string description)
		{
			this.parent = parent; this.description = description;
		}
        public override Status Process()
        {
            return Status.Success;
        }
	}
	public class Idle : Action
	{
		public Idle(Node parent, string description)
		{
			this.parent = parent; this.description = description;
		}
        public override Status Process()
        {
            return Status.Success;
        }
	}
	public class Flail : Action
	{
		public Flail(Node parent, string description)
		{
			this.parent = parent; this.description = description;
		}
        public override Status Process()
        {
            return Status.Success;
        }
	}
	public class AttackHelm : Action
	{
		public AttackHelm(Node parent, string description)
		{
			this.parent = parent; this.description = description;
		}
        public override Status Process()
        {
            return Status.Success;
        }
	}
	public class SetGoal : Action
	{
		public SetGoal(Node parent, string description)
		{
			this.parent = parent; this.description = description;
		}
        public override Status Process()
        {
            return Status.Success;
        }
	}
	public class MoveToGoal : Action
	{
		public MoveToGoal(Node parent, string description)
		{
			this.parent = parent; this.description = description;
		}
        public override Status Process()
        {
            return Status.Success;
        }
	}

}
