using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shadout.Controllers
{
	public class AgentSpawner : MonoBehaviour
	{
		#region SerializedFields

		[SerializeField]
		private GameObject agentPrefab;

		[SerializeField]
		private float spawnTime;
		[SerializeField]
		private Graph graph;
		[SerializeField]
		private List<ShelfController> shelfs;
		[SerializeField]
		private CashierController cashierController;

		public Node SpawnNode;
		public Node DespawnNode;
		public Node CashierNode;

		public Graph Graph => graph;
		public List<ShelfController> Shelfs => shelfs;
		public CashierController Cashier => cashierController;
		private GraphSolver solver;
		public GraphSolver GraphSolver => solver;

		#endregion

		#region Variables

		#endregion

		#region Events

		#endregion

		#region Props

		#endregion

		#region Unity Methods

		private void Start() 
		{
			solver = new GraphSolver(graph);
			StartCoroutine(Spawn());
		}

		#endregion

		#region Methods

		private AgentController SpawnAgent()
		{
			var agentGO = Instantiate(agentPrefab, SpawnNode.transform.position, Quaternion.identity);
			agentGO.transform.SetParent(transform);
			var agent = agentGO.GetComponent<AgentController>();
			agent.spawner = this;
			agent.Initialize();
			return agent;
		}

		private IEnumerator Spawn()
		{
			yield return new WaitForSeconds(spawnTime);
			var agent = SpawnAgent();
			StartCoroutine(Spawn());
		}

		#endregion

		#region Callbacks

		#endregion
	}
}