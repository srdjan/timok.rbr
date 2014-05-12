// <copyright file="CommandService.cs" company="no company">
// Distributed under Microsoft Public License (Ms-PL)
// </copyright>
using System;
using System.Windows;

namespace RbrSiverlight.Input {
	/// <summary>
	/// Service class that provides the system implementation for linking Command
	/// </summary>
	public static class CommandService {
		/// <summary>
		///     The DependencyProperty for the CommandParameter property.
		/// </summary> 
		public static readonly DependencyProperty CommandParameterProperty =
			DependencyProperty.RegisterAttached(
				"CommandParameter", // Name 
				typeof (object), // Type 
				typeof (CommandService), // Owner
				new PropertyMetadata(CommandParameterChanged));

		/// <summary>
		///     The DependencyProperty for the Command property.
		/// </summary> 
		public static readonly DependencyProperty CommandProperty =
			DependencyProperty.RegisterAttached(
				"Command", // Name 
				typeof (string), // Type 
				typeof (CommandService), // Owner
				new PropertyMetadata(CommandChanged));

		/// <summary>
		///     Gets the value of the Command property. 
		/// </summary>
		/// <param name="element">The object on which to query the property.</param>
		/// <returns>The value of the property.</returns> 
		public static string GetCommand(DependencyObject element) {
			if (element == null) {
				throw new ArgumentNullException("element");
			}

			return (string) element.GetValue(CommandProperty);
		}

		/// <summary>
		///     Gets the value of the CommandParameter property. 
		/// </summary>
		/// <param name="element">The object on which to query the property.</param>
		/// <returns>The value of the property.</returns> 
		public static object GetCommandParameter(DependencyObject element) {
			if (element == null) {
				throw new ArgumentNullException("element");
			}

			return element.GetValue(CommandParameterProperty);
		}

		/// <summary> 
		///     Sets the value of the Command property.
		/// </summary>
		/// <param name="element">The object on which to set the value.</param> 
		/// <param name="value">The desired value of the property.</param> 
		public static void SetCommand(DependencyObject element, string value) {
			if (element == null) {
				throw new ArgumentNullException("element");
			}

			element.SetValue(CommandProperty, value);
		}

		/// <summary> 
		///     Sets the value of the CommandParameter property.
		/// </summary>
		/// <param name="element">The object on which to set the value.</param> 
		/// <param name="value">The desired value of the property.</param> 
		public static void SetCommandParameter(DependencyObject element, object value) {
			if (element == null) {
				throw new ArgumentNullException("element");
			}

			element.SetValue(CommandParameterProperty, value);
		}

		/// <summary>
		/// Finds the command.
		/// </summary>
		/// <param name="commandName">The command name.</param>
		/// <returns>returns the command for a given commandName</returns>
		internal static Command FindCommand(string commandName) {
			if (string.IsNullOrEmpty(commandName)) {
				return null;
			}

			// Check from cache
			Command cmd = null;
			if (Command.CommandCache.TryGetValue(commandName, out cmd)) {
				return cmd;
			}

			return null;
		}

		/// <summary>
		/// occurs when the command change on a <see cref="DependencyObject"/>
		/// </summary>
		/// <param name="d">The dependency object.</param>
		/// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
		private static void CommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			if (d is UIElement) {
				var elem = (UIElement) d;
				CommandSubscription.UnregisterSubscription(e.OldValue as string, elem);
				CommandSubscription.RegisterCommand(e.NewValue as string, elem);
			}
		}

		/// <summary>
		/// occurs when the command change on a <see cref="DependencyObject"/>
		/// </summary>
		/// <param name="d">The dependency object.</param>
		/// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
		private static void CommandParameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {}
	}
}