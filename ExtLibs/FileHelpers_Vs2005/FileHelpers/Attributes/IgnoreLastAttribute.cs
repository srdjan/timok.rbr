#region "  � Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;

namespace FileHelpers
{
	/// <summary>Indicates the number of lines to be discarded at the end.</summary>
	/// <remarks>See the <a href="attributes.html">Complete Attributes List</a> for more clear info and examples of each one.</remarks>
	/// <seealso href="attributes.html">Attributes List</seealso>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class IgnoreLastAttribute : Attribute
	{
		/// <summary>Indicates that the last line must be discarded.</summary>
		public IgnoreLastAttribute() : this(1)
		{
		}

		/// <summary>Indicates the number of last lines to be ignored at the end.</summary>
		/// <param name="numberOfLines">The number of lines to be discarded at end.</param>
		public IgnoreLastAttribute(int numberOfLines)
		{
			mNumberOfLines = numberOfLines;
		}

		private int mNumberOfLines;

		/// <summary>The number of lines to be ignored at end.</summary>
		public int NumberOfLines
		{
			get { return mNumberOfLines; }
		}

	}
}