#region "  � Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;

namespace FileHelpers
{
	/// <summary>Indicates that this class represents a delimited record. </summary>
	/// <remarks>See the <a href="attributes.html">Complete Attributes List</a> for more clear info and examples of each one.</remarks>
	/// <seealso href="attributes.html">Attributes List</seealso>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DelimitedRecordAttribute : TypedRecordAttribute
	{
		internal string Separator;

	/// <summary>Indicates that this class represents a delimited record. </summary>
		/// <param name="sep">The separator string used to split the fields of the record.</param>
		public DelimitedRecordAttribute(string sep)
		{
			if (Separator != String.Empty)
				this.Separator = sep;
			else
				throw new ArgumentException("sep debe ser <> \"\"");
		}


	}
}