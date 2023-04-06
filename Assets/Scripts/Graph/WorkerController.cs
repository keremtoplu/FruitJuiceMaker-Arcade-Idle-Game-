using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shadout.Controllers
{
	public class WorkerController //: //AgentBase//, IStacker
	 {
	// 	#region SerializedFields

	// 	[SerializeField]
	// 	private Graph graph;

	// 	[SerializeField]
	// 	private Node tree;
		
	// 	[SerializeField]
	// 	private Node machine;

	// 	[SerializeField]
	// 	private Node export;
		
	// 	[SerializeField]
	// 	private Node cashier;

	// 	[SerializeField]
	// 	private StackController stack;

	// 	[SerializeField]
	// 	private int capacity;

	// 	[SerializeField]
	// 	private float speed;

	// 	#endregion

	// 	#region Variables
		
	// 	private StateMachine stateMachine;
	// 	private List<IState> states;

	// 	#endregion

	// 	#region Events

	// 	#endregion

	// 	#region Props

    //     public StackController Stack { get => stack; set {} }
    //     public int Capacity { get => capacity; set {} }

    //     #endregion

    //     #region Unity Methods

    //     protected override void Start() 
	// 	{
	// 		base.Start();
	// 		Initialize();
	// 	}

	// 	protected override void Update() 
	// 	{
	// 		base.Update();
	// 		stateMachine.Tick();
	// 		stack.MoveStack(AgentState.MovingToTarget == agentState);
	// 	}

	// 	#endregion

	// 	#region Methods

	// 	public void Initialize()
	// 	{
	// 		currentNode = cashier;
	// 		this.agentSpeed = speed; 
			
	// 		graphSolver = new GraphSolver(graph);

	// 		stack.Setup();

	// 		states = new List<IState>();

	// 		stateMachine = new StateMachine();

	// 		var goToTrees = new GoToState(this, tree);
	// 		var collectFruits = new WaitState();
	// 		var goToMachine = new GoToState(this, machine);
	// 		var uploadFruits = new WaitState();
	// 		var goToExport = new GoToState(this, export);
	// 		var takeJuices = new WaitState();
	// 		var goToCashier = new GoToState(this, cashier);
	// 		var giveJuices = new WaitState();

	// 		states.Add(goToTrees);
	// 		states.Add(collectFruits);
	// 		states.Add(goToMachine);
	// 		states.Add(uploadFruits);
	// 		states.Add(goToExport);
	// 		states.Add(takeJuices);
	// 		states.Add(goToCashier);
	// 		states.Add(giveJuices);

	// 		stateMachine.AddTransition
	// 		(
	// 			goToTrees,
	// 			collectFruits,
	// 			()=> agentState == AgentState.ReachedToTarget
	// 		);

	// 		stateMachine.AddTransition
	// 		(
	// 			collectFruits,
	// 			goToMachine,
	// 			()=> stack.IsFull
	// 		);

	// 		stateMachine.AddTransition
	// 		(
	// 			goToMachine,
	// 			uploadFruits,
	// 			()=> agentState == AgentState.ReachedToTarget
	// 		);

	// 		stateMachine.AddTransition
	// 		(
	// 			uploadFruits,
	// 			goToExport,
	// 			()=> stack.IsEmpty
	// 		);

	// 		stateMachine.AddTransition
	// 		(
	// 			goToExport,
	// 			takeJuices,
	// 			()=> agentState == AgentState.ReachedToTarget
	// 		);

	// 		stateMachine.AddTransition
	// 		(
	// 			takeJuices,
	// 			goToCashier,
	// 			()=> stack.IsFull
	// 		);

	// 		stateMachine.AddTransition
	// 		(
	// 			goToCashier,
	// 			giveJuices,
	// 			()=> agentState == AgentState.ReachedToTarget
	// 		);

	// 		stateMachine.AddTransition
	// 		(
	// 			giveJuices,
	// 			goToTrees,
	// 			()=> stack.IsEmpty
	// 		);

	// 		stateMachine.SetState(goToTrees);
	// 	}

    //     public void CollectFruit(TreeController tree)
    //     {
    //         FruitController fruit;
    //         if (tree.TryCollectFruit(out fruit) && stack.ItemCount < capacity)
    //         {
    //             stack.Add(fruit,null,true);
    //         }
    //     }

    //     #endregion

    //     #region Callbacks

    //     #endregion
     }
}