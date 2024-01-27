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
			string[] lines = File.ReadAllLines("Assets/BehaviorTree/" + BehaviorTreeFileName);
			root = new Repeater();
			current = root;
			int currentLevel = 0;
			bool sameLevel = true;
			foreach (string line in lines)
			{
				string[] indents = line.Split("\t");
				int level = indents.Length - 1;
				sameLevel = level == currentLevel;
				currentLevel = level - currentLevel;
				string[] node_desc = indents.Last().Split(": ");
				
				// Debug.Log(words[0].Split("\t").Length);
			}
            // generate tree
            // set current = root
        }
	
	    // Update is called once per frame
	    void Update()
	    {

	       // if running call process on current 

	    }
	}
	
	public abstract class Node
	{
		private Node parent;
	    // success
	    // failure
	    // running

		public Node Parent => parent;

		public abstract Status Process();
	}
	
	/*************************
	 *   Composite Nodes
	 *************************/
	public abstract class Composite : Node
	{
		protected Node[] children;
		protected int current;

		public abstract override Status Process();
	}

	public class Sequence : Composite
	{
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

		public abstract override Status Process();
	}

	public class Succeeder : Decorator
	{
		public override Status Process()
		{
		// process child
		// return success
			return Status.Running;
		}
	}
	
	public class Inverter : Decorator
	{
		public override Status Process()
		{
		// process child
		// return inverse of child
			return Status.Running;
		}
	}
	
	public class Repeater : Decorator
	{
		public override Status Process()
		{
		// on child success/failure repeat process
			return Status.Running;
		}
	}


	/*************************
	 *   Leaf Nodes
	 *************************/
	public class Leaf : Node
	{
		public override Status Process()
		{
		// do stuff
		// while process not complete return running
		// return success or fail depending on result	
			return Status.Running;
		}
	}

}
