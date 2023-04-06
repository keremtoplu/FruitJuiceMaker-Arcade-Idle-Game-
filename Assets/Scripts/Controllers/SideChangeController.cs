using UnityEngine;

namespace Shadout.Controllers
{
	public class SideChangeController : MonoBehaviour
	{
		#region SerializedFields

		#endregion

		#region Variables

		#endregion

		#region Events

		#endregion

		#region Props

		#endregion

		#region Unity Methods

		private void OnTriggerExit(Collider other) 
		{
			if(other.TryGetComponent<PlayerController>(out PlayerController player))
			{
				//player.ChangeFieldOfViewEnable(player.transform.position.x > transform.position.x);
			}
		}

		#endregion

		#region Methods

		#endregion

		#region Callbacks

		#endregion
	}
}