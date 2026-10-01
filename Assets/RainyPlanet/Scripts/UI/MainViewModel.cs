using System;
using RainyPlanet.Core;
using Unity.Properties;
using UnityEngine.UIElements;

namespace RainyPlanet.UI {
	[GeneratePropertyBag]
	public class MainViewModel : INotifyBindablePropertyChanged, IDisposable {
		private readonly Wallet _wallet;
		
		public event EventHandler<BindablePropertyChangedEventArgs> propertyChanged;
		
		[CreateProperty] public string Money => FormatValue(_wallet.Value, 6);

		public MainViewModel(Wallet wallet) {
			_wallet = wallet;
			_wallet.ValueChanged += OnWalletChanged;
		}

		public void Dispose() {
			_wallet.ValueChanged -= OnWalletChanged;
		}

		private void OnWalletChanged(int oldValue, int newValue) {
			propertyChanged?.Invoke(this, new BindablePropertyChangedEventArgs(nameof(Money)));
		}

		private static string FormatValue(int value, int minLength = 5) {
			var digits = value.ToString();
			var padCount = Math.Max(0, minLength - digits.Length);

			if (padCount == 0) {
				return $"<#FFFF>{digits}";
			}

			return $"<#AAAA>{new string('0', padCount)}<#FFFF>{digits}";
		}
	}
}
