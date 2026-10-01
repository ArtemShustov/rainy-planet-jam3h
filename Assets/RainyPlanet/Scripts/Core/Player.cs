using UnityEngine;

namespace RainyPlanet.Core {
	public class Player : PointerDragHandler {
		public readonly Wallet Money = new Wallet();
		private readonly Collider2D[] _hits = new Collider2D[16];

		private Camera _camera;

		private void Awake() {
			_camera = Camera.main;
		}

		public override bool TryBeginDrag(Vector2 screenPos) {
			var worldPos = _camera.ScreenToWorldPoint(screenPos);
			var size = Physics2D.OverlapCircle(worldPos, 0.1f, ContactFilter2D.noFilter, _hits);
			var used = false;

			for (var i = 0; i < size; i++) {
				if (_hits[i].TryGetComponent<Plant>(out var plant) && plant.Progress >= 1) {
					plant.TakeProgress(1);
					Money.Value += 1;
					used = true;
				}

				if (_hits[i].TryGetComponent<PlantSlot>(out var slot) && !slot.IsUnlocked && Money.Value >= slot.MoneyCost) {
					slot.Unlock();
					Money.Value -= slot.MoneyCost;
				}
			}

			return used;
		}
	}
}
