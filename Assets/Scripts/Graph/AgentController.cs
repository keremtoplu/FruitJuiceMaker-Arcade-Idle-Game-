using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shadout.Controllers
{
	public class AgentController : AgentBase, IStacker
	{
		#region SerializedFields

		public AgentSpawner spawner;
		private Graph graph => spawner.Graph;

		[SerializeField]
		private Animator animator;
		private List<ShelfController> shelfs => spawner.Shelfs;
		private CashierController cashierController => spawner.Cashier;
		
		[SerializeField]
		private Node spawnNode => spawner.SpawnNode;

		[SerializeField]
		private Node despawnNode => spawner.DespawnNode;
		
		[SerializeField]
		private Node cashier => spawner.CashierNode;

		[SerializeField]
		private StackController stack;

		[SerializeField]
		private int capacity;

		[SerializeField]
		private float speed;

		private List<ShelfController> activeShelfs;

		#endregion

		#region Variables
		
		private StateMachine stateMachine;
		private List<IState> states;

		#endregion

		#region Events

		#endregion

		#region Props

        public StackController Stack { get => stack; set {} }
        public int Capacity { get => capacity; set {} }

        #endregion

        #region Unity Methods

        protected override void Start() 
		{
			base.Start();
			stack.Setup();
			animator.SetTrigger("walk");
		}

		protected override void Update() 
		{
			base.Update();
			stateMachine.Tick();
			stack.MoveStack(AgentState.MovingToTarget == agentState);
		}

		#endregion

		#region Methods

		public void Initialize()
		{
			stateMachine = new StateMachine();
			activeShelfs = new List<ShelfController>();
			for (int i = 0; i < shelfs.Count; i++)
			{
				if (shelfs[i].BuyController.IsBought)
				{
					activeShelfs.Add(shelfs[i]);
				}
			}
			currentNode = spawnNode;
			this.agentSpeed = speed; 
			
			graphSolver = spawner.GraphSolver;

			stack.Setup();

			states = new List<IState>();

			var shelf = activeShelfs[0];

			var goShelfs = new GoToState(this, shelf.Node, animator);
			var takeJuice = new TakeJuiceState(activeShelfs, this, shelf);
			var goCashier = new GoToState(this, cashier, animator);
			var giveMoney = new GiveMoneyState(this, cashierController);
			var goExit = new GoToState(this, despawnNode, animator);
			var despawn = new DespawnState(gameObject);

			states.Add(goShelfs);
			states.Add(takeJuice);
			states.Add(goCashier);
			states.Add(giveMoney);
			states.Add(goExit);
			states.Add(despawn);

			stateMachine.AddTransition
			(
				goShelfs,
				takeJuice,
				()=> agentState == AgentState.ReachedToTarget
			);

			stateMachine.AddTransition
			(
				takeJuice,
				goCashier,
				()=> stack.ItemCount >= capacity
			);

			stateMachine.AddTransition
			(
				takeJuice,
				goShelfs,
				()=> shelf.Stack.IsEmpty
			);

			stateMachine.AddTransition
			(
				goCashier,
				giveMoney,
				()=> agentState == AgentState.ReachedToTarget
			);

			stateMachine.AddTransition
			(
				giveMoney,
				goExit,
				()=> stack.IsEmpty
			);

			stateMachine.AddTransition
			(
				goShelfs,
				goExit,
				()=> shelf.Stack.IsEmpty
			);

			stateMachine.AddTransition
			(
				goExit,
				despawn,
				()=> agentState == AgentState.ReachedToTarget
			);

			stateMachine.SetState(goShelfs);
		}

        public void CollectFruit(TreeController treeController)
        {
        }

        #endregion

        #region Callbacks

        #endregion
    }
}