#region Copyright (c) PureComponents, All Rights Reserved

/* ---------------------------------------------------------------------*
*                           PureComponents                              *
*              Copyright (c) All Rights reserved                        *
*                                                                       *
*                                                                       *
* This file and its contents are protected by Czech and                 *
* International copyright laws.  Unauthorized reproduction and/or       *
* distribution of all or any portion of the code contained herein       *
* is strictly prohibited and will result in severe civil and criminal   *
* penalties.  Any violations of this copyright will be prosecuted       *
* to the fullest extent possible under law.                             *
*                                                                       *
* THE SOURCE CODE CONTAINED HEREIN AND IN RELATED FILES IS PROVIDED     *
* TO THE REGISTERED DEVELOPER FOR THE PURPOSES OF EDUCATION AND         *
* TROUBLESHOOTING. UNDER NO CIRCUMSTANCES MAY ANY PORTION OF THE SOURCE *
* CODE BE DISTRIBUTED, DISCLOSED OR OTHERWISE MADE AVAILABLE TO ANY     *
* THIRD PARTY WITHOUT THE EXPRESS WRITTEN CONSENT OF PURECOMPONENYS     *
*                                                                       *
* UNDER NO CIRCUMSTANCES MAY THE SOURCE CODE BE USED IN WHOLE OR IN     *
* PART, AS THE BASIS FOR CREATING A PRODUCT THAT PROVIDES THE SAME,     *
* SIMILAR, SUBSTANTIALLY SIMILAR OR THE SAME, FUNCTIONALITY AS ANY      *
* PURECOMPONENTS PRODUCT.                                               *
*                                                                       *
* THE REGISTERED DEVELOPER ACKNOWLEDGES THAT THIS SOURCE CODE           *
* CONTAINS VALUABLE AND PROPRIETARY TRADE SECRETS OF PURECOMPONENTS.    *
* THE REGISTERED DEVELOPER AGREES TO EXPEND EVERY EFFORT TO             *
* INSURE ITS CONFIDENTIALITY.                                           *
*                                                                       *
* THE END USER LICENSE AGREEMENT (EULA) ACCOMPANYING THE PRODUCT        *
* PERMITS THE REGISTERED DEVELOPER TO REDISTRIBUTE THE PRODUCT IN       *
* EXECUTABLE FORM ONLY IN SUPPORT OF APPLICATIONS WRITTEN USING         *
* THE PRODUCT.  IT DOES NOT PROVIDE ANY RIGHTS REGARDING THE            *
* SOURCE CODE CONTAINED HEREIN.                                         *
*                                                                       *
* THIS COPYRIGHT NOTICE MAY NOT BE REMOVED FROM THIS FILE.              *
* --------------------------------------------------------------------- *
*/

#endregion Copyright (c) PureComponents, All Rights Reserved

using System.ComponentModel;

namespace PureComponents.TreeView.Internal
{
	/// <summary>
	/// Represents the different states of a ScrollBar control's buttons and track
	/// </summary>
	[ToolboxItem(false)]
	internal enum ScrollBarStates
	{
		/// <summary>
		/// 
		/// </summary>
		Normal = 1,
		
		/// <summary>
		/// 
		/// </summary>
		Hot = 2,
		
		/// <summary>
		/// 
		/// </summary>
		Pressed = 3,
		
		/// <summary>
		/// 
		/// </summary>
		Disabled = 4,

		/// <summary>
		/// 
		/// </summary>
		UpNormal = 1,
		
		/// <summary>
		/// 
		/// </summary>
		UpHot = 2,
		
		/// <summary>
		/// 
		/// </summary>
		UpPressed = 3,
		
		/// <summary>
		/// 
		/// </summary>
		UpDisabled = 4,

		/// <summary>
		/// 
		/// </summary>
		DownNormal = 5,
		
		/// <summary>
		/// 
		/// </summary>
		DownHot = 6,
		
		/// <summary>
		/// 
		/// </summary>
		DownPressed = 7,
		
		/// <summary>
		/// 
		/// </summary>
		DownDisabled = 8,

		/// <summary>
		/// 
		/// </summary>
		LeftNormal = 9,
		
		/// <summary>
		/// 
		/// </summary>
		LeftHot = 10,
		
		/// <summary>
		/// 
		/// </summary>
		LeftPressed = 11,
		
		/// <summary>
		/// 
		/// </summary>
		LeftDisabled = 12,

		/// <summary>
		/// 
		/// </summary>
		RightNormal = 13,
		
		/// <summary>
		/// 
		/// </summary>
		RightHot = 14,
		
		/// <summary>
		/// 
		/// </summary>
		RightPressed = 15,
		
		/// <summary>
		/// 
		/// </summary>
		RightDisabled = 16,

		/// <summary>
		/// 
		/// </summary>
		RightAlign = 1,

		/// <summary>
		/// 
		/// </summary>
		LeftAlign = 2
	}
}
