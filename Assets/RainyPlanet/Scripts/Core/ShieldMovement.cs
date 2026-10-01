using System.Collections.Generic;
using UnityEngine;

namespace RainyPlanet.Core {
	public class ShieldMovement : PointerDragHandler {
		private Camera _camera;
		private float _angleOffset;

		private readonly List<Collider2D> _hits = new List<Collider2D>();

		private void Awake() {
			_camera = Camera.main;
		}

		public override bool TryBeginDrag(Vector2 screenPos) {
			if (!IsPointerOverShield(_camera.ScreenToWorldPoint(screenPos))) {
				return false;
			}

			_angleOffset = Mathf.DeltaAngle(GetPointerAngle(screenPos), transform.eulerAngles.z);
			return true;
		}

		public override void Drag(Vector2 screenPos) {
			transform.rotation = Quaternion.Euler(0f, 0f, GetPointerAngle(screenPos) + _angleOffset);
		}

		private float GetPointerAngle(Vector2 screenPos) {
			var direction = _camera.ScreenToWorldPoint(screenPos) - transform.position;
			return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		}

		private bool IsPointerOverShield(Vector2 worldPos) {
			var size = Physics2D.OverlapCircle(worldPos, 0.1f, ContactFilter2D.noFilter, _hits);
			for (var i = 0; i < size; i++) {
				var hit = _hits[i];

				if (hit != null && hit.transform.IsChildOf(transform)) {
					return true;
				}
			}

			return false;
		}
	}
}
