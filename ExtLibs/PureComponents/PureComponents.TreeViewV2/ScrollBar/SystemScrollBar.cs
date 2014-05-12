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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using PureComponents.TreeView.Internal;

namespace PureComponents.TreeView.Internal
{
	/// <summary>
	/// Abstract implementation of system scrollbar control implementing custom painting and theming
	/// </summary>
	[ToolboxItem(false)]	
	[DesignTimeVisible(false)]	
	internal abstract class SystemScrollBar : CustomScrollBarBase
	{
		#region private members

		#endregion

		#region construction

		#endregion

		#region implementation

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gr"></param>
		/// <param name="rect"></param>
		/// <param name="d"></param>
		/// <param name="state"></param>
		protected override void DrawArrow(Graphics gr, Rectangle rect, Direction d, ElementState state)
		{
			if (ThemeFactory.VisualStylesEnabled && ThemeFactory.VisualStylesSupported)
			{
				ThemeFactory.DrawScrollBar(gr, rect, GetArrowScrollBarStates(d, state), ScrollBarParts.ArrowBtn);	
			}
			else				
			{
				ScrollButton sb = DirectionToScrollButton(d);
				ButtonState bs = ElementStateToButtonState(state);

				ControlPaint.DrawScrollButton(gr, rect, sb, bs);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gr"></param>
		/// <param name="rect"></param>
		/// <param name="o"></param>
		/// <param name="state"></param>
		protected override void DrawThumb(Graphics gr, Rectangle rect, Orientation o, ElementState state)
		{
			if (ThemeFactory.VisualStylesEnabled && ThemeFactory.VisualStylesSupported)
			{
				ThemeFactory.DrawScrollBar(gr, rect, ElementStateToScrollBarStates(state),
					o == Orientation.Horizontal ? ScrollBarParts.ThumbBtnHorz : ScrollBarParts.ThumbBtnVert);

				if (FirstCoord((Point) rect.Size) > 17)
				{
					ThemeFactory.DrawScrollBar(gr, rect, ElementStateToScrollBarStates(state),
						o == Orientation.Horizontal ? ScrollBarParts.GripperHorz : ScrollBarParts.GripperVert);
				}
			}
			else
			{
				if (state == ElementState.Pushed)
					ControlPaint.DrawButton(gr, rect, ButtonState.Pushed);
				else
					ControlPaint.DrawButton(gr, rect, ButtonState.Normal);
			}						
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gr"></param>
		/// <param name="rect"></param>
		/// <param name="d"></param>
		/// <param name="state"></param>
		protected override void DrawInside(Graphics gr, Rectangle rect, Direction d, ElementState state)
		{
			if (ThemeFactory.VisualStylesEnabled && ThemeFactory.VisualStylesSupported)
			{
				ScrollBarParts p = ScrollBarParts.UpperTrackVert;

				switch (d)
				{
					case Direction.Up:
						p = ScrollBarParts.UpperTrackVert;
						break;
					case Direction.Down:
						p = ScrollBarParts.LowerTrackVert;
						break;
					case Direction.Left:
						p = ScrollBarParts.UpperTrackHorz;
						break;
					case Direction.Right:
						p = ScrollBarParts.LowerTrackHorz;
						break;
				}

				ThemeFactory.DrawScrollBar(gr, rect, ElementStateToScrollBarStates(state), p);
			}
			else
			{
				HatchBrush br = new HatchBrush(HatchStyle.Percent50, SystemColors.ScrollBar, Color.White);
				gr.FillRectangle(br, rect);

				br.Dispose();
				br = null;
			}			
		}

		#endregion

		#region helper functions

		/// <summary>
		/// 
		/// </summary>
		/// <param name="state"></param>
		/// <returns></returns>
		private static ButtonState ElementStateToButtonState(ElementState state)
		{
			ButtonState bs = ButtonState.Normal;
			switch (state)
			{
				case ElementState.Disabled:
					bs = ButtonState.Inactive;
					break;
				case ElementState.Pushed:
					bs = ButtonState.Pushed;
					break;
			}
			return bs;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="state"></param>
		/// <returns></returns>
		private static ScrollBarStates ElementStateToScrollBarStates(ElementState state)
		{
			ScrollBarStates bs = ScrollBarStates.Normal;
			switch (state)
			{
				case ElementState.Disabled:
					bs = ScrollBarStates.Normal;
					break;
				case ElementState.Pushed:
					bs = ScrollBarStates.Pressed;
					break;
				case ElementState.Hover:
					bs = ScrollBarStates.Hot;
					break;
			}
			return bs;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="d"></param>
		/// <param name="state"></param>
		/// <returns></returns>
		private static ScrollBarStates GetArrowScrollBarStates(Direction d, ElementState state)
		{
			ScrollBarStates sbs = ElementStateToScrollBarStates(state);

			switch (d)
			{
				case Direction.Up:
					break;
				case Direction.Down:
					sbs = (ScrollBarStates) (((int) sbs) + 4);
					break;
				case Direction.Left:
					sbs = (ScrollBarStates) (((int) sbs) + 8);
					break;
				case Direction.Right:
					sbs = (ScrollBarStates) (((int) sbs) + 12);
					break;
			}

			return sbs;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		private static ScrollButton DirectionToScrollButton(Direction d)
		{
			ScrollButton sb = ScrollButton.Down;
			switch (d)
			{
				case Direction.Up:
					sb = ScrollButton.Up;
					break;
				case Direction.Down:
					sb = ScrollButton.Down;
					break;
				case Direction.Left:
					sb = ScrollButton.Left;
					break;
				case Direction.Right:
					sb = ScrollButton.Right;
					break;
			}
			return sb;
		}

		#endregion

		#region event handlers

		#endregion

		#region properties		

		#endregion		
	}
}