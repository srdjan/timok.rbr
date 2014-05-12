// <copyright file="CommandSubscription.cs" company="no company">
// Distributed under Microsoft Public License (Ms-PL)
// </copyright>

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using RbrSiverlight.Extensions;

namespace RbrSiverlight.Input {
	/// <summary>
	/// Handles command subscription of a UIElement
	/// </summary>
	public class CommandSubscription {
		/// <summary>
		/// global subscriptions
		/// </summary>
		static readonly Dictionary<UIElement, Dictionary<string, CommandSubscription>> subscriptions =
			new Dictionary<UIElement, Dictionary<string, CommandSubscription>>();

		/// <summary>
		/// Initializes a new instance of the <see cref="CommandSubscription"/> class.
		/// </summary>
		/// <param name="element">The element attached to the command.</param>
		/// <param name="commandName">Name of the command.</param>
		public CommandSubscription(UIElement element, string commandName) {
			Element = element;
			CommandName = commandName;
		}

		/// <summary>
		/// Gets the element attached to the command
		/// </summary>
		/// <value>The element.</value>
		public UIElement Element { get; private set; }

		/// <summary>
		/// Gets the Command name
		/// </summary>
		/// <value>The name of the command.</value>
		public string CommandName { get; private set; }

		/// <summary>
		/// Registers the command.
		/// </summary>
		/// <param name="commandName">The command name.</param>
		/// <param name="element">The element.</param>
		internal static void RegisterCommand(string commandName, UIElement element) {
			var subscription = new CommandSubscription(element, commandName);

			Dictionary<string, CommandSubscription> elementSubscriptions;
			if (!subscriptions.TryGetValue(element, out elementSubscriptions)) {
				elementSubscriptions = new Dictionary<string, CommandSubscription>();
				subscriptions.Add(element, elementSubscriptions);
			}

			Command cmd = CommandService.FindCommand(commandName);
			if (cmd != null) {
				if (element is ButtonBase) {
					((ButtonBase) element).Click += subscription.CommandService_Click;
				}
				else if (element is TextBox) {
					(element).KeyDown += subscription.CommandService_KeyDown;
				}
				else {
					element.MouseLeftButtonUp += subscription.CommandService_Click;
				}

				var fe = element as FrameworkElement;
				if (fe != null) {
					fe.LayoutUpdated += subscription.LayoutUpdated;
				}
			}

			elementSubscriptions[commandName] = subscription;
		}

		private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args) {
			return null;
		}

		/// <summary>
		/// Unregister a command from an element
		/// </summary>
		/// <param name="commandName">The command name to remove</param>
		/// <param name="element">The element to be detached</param>
		internal static void UnregisterSubscription(string commandName, UIElement element) {
			Dictionary<string, CommandSubscription> elementSubscriptions;
			if (!subscriptions.TryGetValue(element, out elementSubscriptions)) {
				return;
			}

			CommandSubscription currentSubscription;
			if (!elementSubscriptions.TryGetValue(commandName, out currentSubscription)) {
				return;
			}

			currentSubscription.Unregister();
		}

		/// <summary>
		/// Handles the Click event of the CommandService control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void CommandService_Click(object sender, EventArgs e) {
			ExecuteCommand(sender);
		}

		/// <summary>
		/// Executes the command
		/// </summary>
		/// <param name="sender">The command sender</param>
		private void ExecuteCommand(object sender) {
			string commandName = CommandService.GetCommand(Element);
			Command cmd = CommandService.FindCommand(commandName);
			object parameter = CommandService.GetCommandParameter(Element);
			cmd.Execute(parameter, sender);
		}

		/// <summary>
		/// Handles the KeyDown event of the CommandService control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
		private void CommandService_KeyDown(object sender, KeyEventArgs e) {
			if (e.Key == Key.Enter) {
				ExecuteCommand(sender);
			}
		}

		/// <summary>
		/// The layout of the element has changed. Checks for removing command subscription if necessary
		/// </summary>
		/// <param name="sender">the source of the event</param>
		/// <param name="e">args of the event</param>
		private void LayoutUpdated(object sender, EventArgs e) {
			var fe = (FrameworkElement) Element;
			if (!fe.IsInVisualTree()) {
				Unregister();
			}
		}

		/// <summary>
		/// Unregister the current CommandSubscription
		/// </summary>
		private void Unregister() {
			Command cmd = CommandService.FindCommand(CommandName);
			if (cmd != null) {
				if (Element is ButtonBase) {
					((ButtonBase) Element).Click -= CommandService_Click;
				}
				else {
					Element.MouseLeftButtonUp -= CommandService_Click;
				}

				var fe = Element as FrameworkElement;
				if (fe != null) {
					fe.LayoutUpdated -= LayoutUpdated;
				}
			}

			Dictionary<string, CommandSubscription> elementSubscriptions;
			if (!subscriptions.TryGetValue(Element, out elementSubscriptions)) {
				return;
			}

			elementSubscriptions.Remove(CommandName);
			if (elementSubscriptions.Count == 0) {
				subscriptions.Remove(Element);
			}
		}
	}
}