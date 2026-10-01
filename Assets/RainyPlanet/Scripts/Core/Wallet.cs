using System;

namespace RainyPlanet.Core {
	public class Wallet {
		private int _value;
		
		public int Value {
			get => _value;
			set => SetValue(value);
		}
		
		public event Action<int, int> ValueChanged;

		public Wallet(int value = 0) {
			_value = value;
		}

		public void SetValue(int value) {
			if (_value == value) {
				return;
			}
			var oldValue = _value;
			_value = value;
			ValueChanged?.Invoke(oldValue, value);
		}
	}
}
