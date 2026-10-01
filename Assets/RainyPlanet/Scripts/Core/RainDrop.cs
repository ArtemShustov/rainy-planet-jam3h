using UnityEngine;

namespace RainyPlanet.Core {
	public class RainDrop : MonoBehaviour {
		[SerializeField] private float _moveSpeed = 3;
		[SerializeField] private float _damage = 0.3f;

		private void Update() {
			transform.rotation = Quaternion.LookRotation(Vector3.forward, transform.position);
			transform.position += -transform.up * (_moveSpeed * Time.deltaTime);
		}

		private void OnTriggerEnter2D(Collider2D other) {
			if (other.TryGetComponent<DestroyRainTag>(out _)) {
				Destroy(gameObject);
			}

			if (other.TryGetComponent<Plant>(out var plant)) {
				plant.TakeProgress(_damage);
			}
		}
	}
}
