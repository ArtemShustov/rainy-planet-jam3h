using UnityEngine;

namespace RainyPlanet.Core {
	public class PlanetGenerator : MonoBehaviour {
		[SerializeField] private PlantSlot _slotPrefab;
		[SerializeField] private float _radius = 8;
		[SerializeField] private float _slotDistance = 2;
		[SerializeField] private float _costLinear = 1;
		[SerializeField] private float _costGrowth = 0.35f;

		private void Start() {
			Generate();
		}

		private void Generate() {
			var count = Mathf.Max(1, Mathf.FloorToInt(2f * Mathf.PI * _radius / _slotDistance));
			var step = 360f / count;

			for (var i = 0; i < count; i++) {
				var angle = 90f + i * step;
				var rad = angle * Mathf.Deg2Rad;
				var dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f);

				var position = transform.position + dir * _radius;
				var rotation = Quaternion.Euler(0f, 0f, angle - 90f);

				var slot = Instantiate(_slotPrefab, position, rotation, transform);
				slot.SetCost(CalculateCost(Mathf.Min(i, count - i)));
			}
		}

		private int CalculateCost(int ring) {
			if (ring == 0) {
				return 0;
			}

			return Mathf.RoundToInt(ring * (_costLinear + ring * _costGrowth));
		}
	}
}
