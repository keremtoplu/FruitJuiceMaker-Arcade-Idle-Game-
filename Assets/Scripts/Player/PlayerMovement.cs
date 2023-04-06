using UnityEngine;

namespace Shadout.Models
{
	public class PlayerMovement
	{
		public PlayerMovement(PlayerMovementConfig playerMovementConfig, Rigidbody body, Transform transform)
		{
			this.playerMovementConfig = playerMovementConfig;
			this.body = body;
			this.transform = transform;
		}

		#region Variables

		public float MovementSpeed => movementSpeed;

		private PlayerMovementConfig playerMovementConfig;
		private Rigidbody body;
		private Transform transform;
		private float movementSpeed;
		private float rotationSpeed;

		#endregion

		#region Events

		#endregion

		#region Props

		#endregion

		#region Methods

		public void Initialize()
		{
			movementSpeed = PlayerPrefs.HasKey(nameof(movementSpeed)) ? PlayerPrefs.GetFloat(nameof(movementSpeed)) : playerMovementConfig.MovementSpeed;

			rotationSpeed = playerMovementConfig.RotationSpeed;
		}

		public void Save()
        {
            PlayerPrefs.SetFloat(nameof(movementSpeed), movementSpeed);
        }

		public void UpgradeSpeed()
		{
			movementSpeed += .2f;
			Save();
		}

		public void Update(Vector2 direction)
		{
			var movementValue = new Vector3(direction.x, 0, direction.y);
			body.velocity = movementValue * movementSpeed;

			transform.LookAt(transform.position + new Vector3(direction.x, 0, direction.y));
		}

		#endregion

		#region Callbacks

		#endregion
	}
}