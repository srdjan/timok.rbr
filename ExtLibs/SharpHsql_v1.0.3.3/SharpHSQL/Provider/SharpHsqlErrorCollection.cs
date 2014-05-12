#region Usings
using System;
using System.Collections;
#endregion

#region License
/*
 * SharpHsqlErrorCollection.cs
 *
 * Copyright (c) 2004, Andres G Vettori
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *
 * Redistributions of source code must retain the above copyright notice, this
 * list of conditions and the following disclaimer.
 *
 * Redistributions in binary form must reproduce the above copyright notice,
 * this list of conditions and the following disclaimer in the documentation
 * and/or other materials provided with the distribution.
 *
 * Neither the name of the HSQL Development Group nor the names of its
 * contributors may be used to endorse or promote products derived from this
 * software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE REGENTS OR CONTRIBUTORS BE LIABLE FOR
 * ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *
 * This package is based on HypersonicSQL, originally developed by Thomas Mueller.
 *
 * C# SharpHsql ADO.NET Provider by Andr�s G Vettori.
 * http://workspaces.gotdotnet.com/sharphsql
 */
#endregion

namespace System.Data.Hsql
{
	/// <summary>
	/// Strong typed collection of <see cref="SharpHsqlError"/> objects used in <see cref="SharpHsqlException"/>.
	/// <seealso cref="SharpHsqlError"/>
	/// <seealso cref="SharpHsqlException"/>
	/// </summary>
	/// <remarks>Not serializable on Compact Framework 1.0</remarks>
	#if !POCKETPC
	[Serializable]
	#endif
	public sealed class SharpHsqlErrorCollection : CollectionBase
	{
		#region Constructor

		/// <summary>
		/// Default constructor.
		/// </summary>
		internal SharpHsqlErrorCollection() : base()
		{
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Strong typed version of the Add method for adding <see cref="SharpHsqlError"/>
		/// objects to the collection.
		/// <seealso cref="SharpHsqlError"/>
		/// <seealso cref="SharpHsqlException"/>
		/// </summary>
		/// <param name="error"></param>
		public void Add( SharpHsqlError error )
		{
			base.InnerList.Add(error);
		}

		/// <summary>
		/// Strong typed version of the Add method for adding <see cref="SharpHsqlError"/>
		/// objects to the collection.
		/// <seealso cref="SharpHsqlError"/>
		/// <seealso cref="SharpHsqlException"/>
		/// </summary>
		public SharpHsqlError this[ int index ]
		{
			get
			{
				return (SharpHsqlError)base.InnerList[index];
			}
			set 
			{
				base.InnerList[index] = value;
			}
		}

		#endregion
	}
}
