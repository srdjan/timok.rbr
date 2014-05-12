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

using System;
using System.Drawing;

namespace PureComponents.TreeView
{
	public class TreeViewStyleFactory
	{
		#region private members
		/// <summary>
		/// The singleton instance holder
		/// </summary>
		protected static TreeViewStyleFactory m_oInstance = null;
		#endregion

		#region construction
		/// <summary>
		/// Protected hidden construction
		/// </summary>
		protected TreeViewStyleFactory()
		{
		}

		/// <summary>
		/// The instance getter of the TreeViewStyleFactory singleton.
		/// </summary>
		/// <returns>Instance of the TreeViewStyleFactory singleton class</returns>
		public static TreeViewStyleFactory GetInstance()
		{
			if (m_oInstance == null)
				m_oInstance = new TreeViewStyleFactory();

			return m_oInstance;
		}
		#endregion

		#region implementation
		/// <summary>
		/// The default style scheme getter
		/// </summary>
		/// <returns>New TreeStyle object</returns>
		public TreeViewStyle GetDefaultTreeStyle()
		{
			return new TreeViewStyle();
		}		

		/// <summary>
		/// The sky color Tree style sceheme getter
		/// </summary>
		/// <returns></returns>
		public TreeViewStyle GetSkyTreeStyle()
		{
			TreeViewStyle oStyle = GetDefaultTreeStyle();

			oStyle.AutoCollapse = false;
			oStyle.BackColor = System.Drawing.Color.White;
			oStyle.BorderColor = System.Drawing.Color.RoyalBlue;
			oStyle.BorderStyle = PureComponents.TreeView.BorderStyle.Solid;
			oStyle.FadeColor = System.Drawing.Color.LightSteelBlue;
			oStyle.FillStyle = PureComponents.TreeView.FillStyle.DiagonalForward;
			oStyle.FlashColor = System.Drawing.Color.Red;
			oStyle.HighlightSelectedPath = false;
			oStyle.LineColor = System.Drawing.Color.RoyalBlue;
			oStyle.LineStyle = PureComponents.TreeView.LineStyle.Dot;
			oStyle.NodeStyle.CheckBoxStyle.BackColor = System.Drawing.Color.White;
			oStyle.NodeStyle.CheckBoxStyle.BorderColor = System.Drawing.Color.CornflowerBlue;
			oStyle.NodeStyle.CheckBoxStyle.BorderStyle = PureComponents.TreeView.CheckBoxBorderStyle.Solid;
			oStyle.NodeStyle.CheckBoxStyle.CheckColor = System.Drawing.Color.RoyalBlue;
			oStyle.NodeStyle.CheckBoxStyle.HoverBackColor = System.Drawing.Color.LightSteelBlue;
			oStyle.NodeStyle.CheckBoxStyle.HoverBorderColor = System.Drawing.Color.RoyalBlue;
			oStyle.NodeStyle.CheckBoxStyle.HoverCheckColor = System.Drawing.Color.Black;			
			oStyle.NodeStyle.ExpandBoxStyle.BackColor = System.Drawing.Color.CornflowerBlue;
			oStyle.NodeStyle.ExpandBoxStyle.BorderColor = System.Drawing.Color.RoyalBlue;
			oStyle.NodeStyle.ExpandBoxStyle.ForeColor = System.Drawing.Color.RoyalBlue;
			oStyle.NodeStyle.ExpandBoxStyle.Shape = PureComponents.TreeView.ExpandBoxShape.XP;			
			oStyle.NodeStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			oStyle.NodeStyle.ForeColor = System.Drawing.Color.Black;
			oStyle.NodeStyle.HoverBackColor = System.Drawing.Color.LightSteelBlue;
			oStyle.NodeStyle.HoverForeColor = System.Drawing.Color.Black;
			oStyle.NodeStyle.Parent = null;
			oStyle.NodeStyle.SelectedBackColor = System.Drawing.Color.LightSteelBlue;
			oStyle.NodeStyle.SelectedBorderColor = System.Drawing.Color.CornflowerBlue;
			oStyle.NodeStyle.SelectedFillStyle = PureComponents.TreeView.FillStyle.Flat;
			oStyle.NodeStyle.SelectedForeColor = System.Drawing.Color.Black;
			oStyle.NodeStyle.TooltipStyle.BackColor = System.Drawing.Color.LightSteelBlue;
			oStyle.NodeStyle.TooltipStyle.BorderColor = System.Drawing.Color.RoyalBlue;
			oStyle.NodeStyle.TooltipStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			oStyle.NodeStyle.TooltipStyle.ForeColor = System.Drawing.Color.Black;			
			oStyle.NodeStyle.UnderlineColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(0)), ((System.Byte)(0)));
			oStyle.NodeStyle.UnderlineStyle = PureComponents.TreeView.UnderlineStyle.Solid;			
			oStyle.ShowLines = true;
			oStyle.ShowPlusMinus = true;
			oStyle.ShowSubitemsIndicator = false;
			oStyle.TrackNodeHover = true;

			return oStyle;
		}

		/// <summary>
		/// The ocean color Tree style scheme getter
		/// </summary>
		/// <returns></returns>
		public TreeViewStyle GetOceanTreeStyle()
		{
			TreeViewStyle oStyle = GetDefaultTreeStyle();

			oStyle.AutoCollapse = false;
			oStyle.BackColor = System.Drawing.Color.White;
			oStyle.BorderColor = System.Drawing.Color.Teal;
			oStyle.BorderStyle = PureComponents.TreeView.BorderStyle.Solid;
			oStyle.FadeColor = System.Drawing.Color.PaleTurquoise;
			oStyle.FillStyle = PureComponents.TreeView.FillStyle.DiagonalForward;
			oStyle.FlashColor = System.Drawing.Color.Red;
			oStyle.HighlightSelectedPath = false;
			oStyle.LineColor = System.Drawing.Color.Teal;
			oStyle.LineStyle = PureComponents.TreeView.LineStyle.Dot;
			oStyle.NodeStyle.CheckBoxStyle.BackColor = System.Drawing.Color.White;
			oStyle.NodeStyle.CheckBoxStyle.BorderColor = System.Drawing.Color.LightSeaGreen;
			oStyle.NodeStyle.CheckBoxStyle.BorderStyle = PureComponents.TreeView.CheckBoxBorderStyle.Solid;
			oStyle.NodeStyle.CheckBoxStyle.CheckColor = System.Drawing.Color.Teal;
			oStyle.NodeStyle.CheckBoxStyle.HoverBackColor = System.Drawing.Color.PaleTurquoise;
			oStyle.NodeStyle.CheckBoxStyle.HoverBorderColor = System.Drawing.Color.Teal;
			oStyle.NodeStyle.CheckBoxStyle.HoverCheckColor = System.Drawing.Color.Black;			
			oStyle.NodeStyle.ExpandBoxStyle.BackColor = System.Drawing.Color.LightSeaGreen;
			oStyle.NodeStyle.ExpandBoxStyle.BorderColor = System.Drawing.Color.Teal;
			oStyle.NodeStyle.ExpandBoxStyle.ForeColor = System.Drawing.Color.Teal;
			oStyle.NodeStyle.ExpandBoxStyle.Shape = PureComponents.TreeView.ExpandBoxShape.XP;			
			oStyle.NodeStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			oStyle.NodeStyle.ForeColor = System.Drawing.Color.Black;
			oStyle.NodeStyle.HoverBackColor = System.Drawing.Color.PaleTurquoise;
			oStyle.NodeStyle.HoverForeColor = System.Drawing.Color.Black;
			oStyle.NodeStyle.Parent = null;
			oStyle.NodeStyle.SelectedBackColor = System.Drawing.Color.PaleTurquoise;
			oStyle.NodeStyle.SelectedBorderColor = System.Drawing.Color.LightSeaGreen;
			oStyle.NodeStyle.SelectedFillStyle = PureComponents.TreeView.FillStyle.Flat;
			oStyle.NodeStyle.SelectedForeColor = System.Drawing.Color.Black;
			oStyle.NodeStyle.TooltipStyle.BackColor = System.Drawing.Color.PaleTurquoise;
			oStyle.NodeStyle.TooltipStyle.BorderColor = System.Drawing.Color.Teal;
			oStyle.NodeStyle.TooltipStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			oStyle.NodeStyle.TooltipStyle.ForeColor = System.Drawing.Color.Black;			
			oStyle.NodeStyle.UnderlineColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(0)), ((System.Byte)(0)));
			oStyle.NodeStyle.UnderlineStyle = PureComponents.TreeView.UnderlineStyle.Solid;			
			oStyle.ShowLines = true;
			oStyle.ShowPlusMinus = true;
			oStyle.ShowSubitemsIndicator = false;
			oStyle.TrackNodeHover = true;

			return oStyle;
		}

		/// <summary>
		/// The forect Tree style scheme getter
		/// </summary>
		/// <returns></returns>
		public TreeViewStyle GetForestTreeStyle()
		{
			TreeViewStyle oStyle = GetDefaultTreeStyle();

			oStyle.AutoCollapse = false;
			oStyle.BackColor = System.Drawing.Color.White;
			oStyle.BorderColor = System.Drawing.Color.ForestGreen;
			oStyle.BorderStyle = PureComponents.TreeView.BorderStyle.Solid;
			oStyle.FadeColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(255)), ((System.Byte)(192)));
			oStyle.FillStyle = PureComponents.TreeView.FillStyle.DiagonalForward;
			oStyle.FlashColor = System.Drawing.Color.Red;
			oStyle.HighlightSelectedPath = false;
			oStyle.LineColor = System.Drawing.Color.ForestGreen;
			oStyle.LineStyle = PureComponents.TreeView.LineStyle.Dot;			
			oStyle.NodeStyle.CheckBoxStyle.BackColor = System.Drawing.Color.White;
			oStyle.NodeStyle.CheckBoxStyle.BorderColor = System.Drawing.Color.LimeGreen;
			oStyle.NodeStyle.CheckBoxStyle.BorderStyle = PureComponents.TreeView.CheckBoxBorderStyle.Solid;
			oStyle.NodeStyle.CheckBoxStyle.CheckColor = System.Drawing.Color.ForestGreen;
			oStyle.NodeStyle.CheckBoxStyle.HoverBackColor = System.Drawing.Color.PaleGreen;
			oStyle.NodeStyle.CheckBoxStyle.HoverBorderColor = System.Drawing.Color.ForestGreen;
			oStyle.NodeStyle.CheckBoxStyle.HoverCheckColor = System.Drawing.Color.Black;			
			oStyle.NodeStyle.ExpandBoxStyle.BackColor = System.Drawing.Color.LimeGreen;
			oStyle.NodeStyle.ExpandBoxStyle.BorderColor = System.Drawing.Color.ForestGreen;
			oStyle.NodeStyle.ExpandBoxStyle.ForeColor = System.Drawing.Color.ForestGreen;
			oStyle.NodeStyle.ExpandBoxStyle.Shape = PureComponents.TreeView.ExpandBoxShape.XP;			
			oStyle.NodeStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			oStyle.NodeStyle.ForeColor = System.Drawing.Color.Black;
			oStyle.NodeStyle.HoverBackColor = System.Drawing.Color.PaleGreen;
			oStyle.NodeStyle.HoverForeColor = System.Drawing.Color.Black;
			oStyle.NodeStyle.Parent = null;
			oStyle.NodeStyle.SelectedBackColor = System.Drawing.Color.PaleGreen;
			oStyle.NodeStyle.SelectedBorderColor = System.Drawing.Color.LimeGreen;
			oStyle.NodeStyle.SelectedFillStyle = PureComponents.TreeView.FillStyle.Flat;
			oStyle.NodeStyle.SelectedForeColor = System.Drawing.Color.Black;			
			oStyle.NodeStyle.TooltipStyle.BackColor = System.Drawing.Color.PaleGreen;
			oStyle.NodeStyle.TooltipStyle.BorderColor = System.Drawing.Color.ForestGreen;
			oStyle.NodeStyle.TooltipStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			oStyle.NodeStyle.TooltipStyle.ForeColor = System.Drawing.Color.Black;			
			oStyle.NodeStyle.UnderlineColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(0)), ((System.Byte)(0)));
			oStyle.NodeStyle.UnderlineStyle = PureComponents.TreeView.UnderlineStyle.Solid;
			oStyle.NodeStyle = oStyle.NodeStyle;
			oStyle.ShowLines = true;
			oStyle.ShowPlusMinus = true;
			oStyle.ShowSubitemsIndicator = false;
			oStyle.TrackNodeHover = true;

			return oStyle;
		}

		/// <summary>
		/// The sunset Tree style scheme getter
		/// </summary>
		public TreeViewStyle GetSunsetTreeStyle()
		{
			TreeViewStyle oStyle = GetDefaultTreeStyle();

			oStyle.AutoCollapse = false;
			oStyle.BackColor = System.Drawing.Color.White;
			oStyle.BorderColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(64)), ((System.Byte)(0)));
			oStyle.BorderStyle = PureComponents.TreeView.BorderStyle.Solid;
			oStyle.FadeColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(192)), ((System.Byte)(128)));
			oStyle.FillStyle = PureComponents.TreeView.FillStyle.DiagonalForward;
			oStyle.FlashColor = System.Drawing.Color.Red;
			oStyle.HighlightSelectedPath = false;
			oStyle.LineColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(64)), ((System.Byte)(0)));
			oStyle.LineStyle = PureComponents.TreeView.LineStyle.Dot;
			oStyle.NodeStyle.CheckBoxStyle.BackColor = System.Drawing.Color.White;
			oStyle.NodeStyle.CheckBoxStyle.BorderColor = System.Drawing.Color.DarkOrange;
			oStyle.NodeStyle.CheckBoxStyle.BorderStyle = PureComponents.TreeView.CheckBoxBorderStyle.Solid;
			oStyle.NodeStyle.CheckBoxStyle.CheckColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(64)), ((System.Byte)(0)));
			oStyle.NodeStyle.CheckBoxStyle.HoverBackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(192)), ((System.Byte)(128)));
			oStyle.NodeStyle.CheckBoxStyle.HoverBorderColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(64)), ((System.Byte)(0)));
			oStyle.NodeStyle.CheckBoxStyle.HoverCheckColor = System.Drawing.Color.Black;			
			oStyle.NodeStyle.ExpandBoxStyle.BackColor = System.Drawing.Color.DarkOrange;
			oStyle.NodeStyle.ExpandBoxStyle.BorderColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(64)), ((System.Byte)(0)));
			oStyle.NodeStyle.ExpandBoxStyle.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(64)), ((System.Byte)(0)));
			oStyle.NodeStyle.ExpandBoxStyle.Shape = PureComponents.TreeView.ExpandBoxShape.XP;			
			oStyle.NodeStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			oStyle.NodeStyle.ForeColor = System.Drawing.Color.Black;
			oStyle.NodeStyle.HoverBackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(192)), ((System.Byte)(128)));
			oStyle.NodeStyle.HoverForeColor = System.Drawing.Color.Black;
			oStyle.NodeStyle.Parent = null;
			oStyle.NodeStyle.SelectedBackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(192)), ((System.Byte)(128)));
			oStyle.NodeStyle.SelectedBorderColor = System.Drawing.Color.DarkOrange;
			oStyle.NodeStyle.SelectedFillStyle = PureComponents.TreeView.FillStyle.Flat;
			oStyle.NodeStyle.SelectedForeColor = System.Drawing.Color.Black;
			oStyle.NodeStyle.TooltipStyle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(192)), ((System.Byte)(128)));
			oStyle.NodeStyle.TooltipStyle.BorderColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(64)), ((System.Byte)(0)));
			oStyle.NodeStyle.TooltipStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			oStyle.NodeStyle.TooltipStyle.ForeColor = System.Drawing.Color.Black;			
			oStyle.NodeStyle.UnderlineColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(0)), ((System.Byte)(0)));
			oStyle.NodeStyle.UnderlineStyle = PureComponents.TreeView.UnderlineStyle.Solid;			
			oStyle.ShowLines = true;
			oStyle.ShowPlusMinus = true;
			oStyle.ShowSubitemsIndicator = false;
			oStyle.TrackNodeHover = true;

			return oStyle;
		}

		/// <summary>
		/// The rose Tree style scheme getter
		/// </summary>
		public TreeViewStyle GetRoseTreeStyle()
		{
			TreeViewStyle oStyle = GetDefaultTreeStyle();

			oStyle.AutoCollapse = false;
			oStyle.BackColor = System.Drawing.Color.White;
			oStyle.BorderColor = System.Drawing.Color.Crimson;
			oStyle.BorderStyle = PureComponents.TreeView.BorderStyle.Solid;
			oStyle.FadeColor = System.Drawing.Color.Pink;
			oStyle.FillStyle = PureComponents.TreeView.FillStyle.DiagonalForward;
			oStyle.FlashColor = System.Drawing.Color.Red;
			oStyle.HighlightSelectedPath = false;
			oStyle.LineColor = System.Drawing.Color.Crimson;
			oStyle.LineStyle = PureComponents.TreeView.LineStyle.Dot;
			oStyle.NodeStyle.CheckBoxStyle.BackColor = System.Drawing.Color.White;
			oStyle.NodeStyle.CheckBoxStyle.BorderColor = System.Drawing.Color.IndianRed;
			oStyle.NodeStyle.CheckBoxStyle.BorderStyle = PureComponents.TreeView.CheckBoxBorderStyle.Solid;
			oStyle.NodeStyle.CheckBoxStyle.CheckColor = System.Drawing.Color.Crimson;
			oStyle.NodeStyle.CheckBoxStyle.HoverBackColor = System.Drawing.Color.Pink;
			oStyle.NodeStyle.CheckBoxStyle.HoverBorderColor = System.Drawing.Color.Crimson;
			oStyle.NodeStyle.CheckBoxStyle.HoverCheckColor = System.Drawing.Color.Black;			
			oStyle.NodeStyle.ExpandBoxStyle.BackColor = System.Drawing.Color.IndianRed;
			oStyle.NodeStyle.ExpandBoxStyle.BorderColor = System.Drawing.Color.Crimson;
			oStyle.NodeStyle.ExpandBoxStyle.ForeColor = System.Drawing.Color.Crimson;
			oStyle.NodeStyle.ExpandBoxStyle.Shape = PureComponents.TreeView.ExpandBoxShape.XP;			
			oStyle.NodeStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			oStyle.NodeStyle.ForeColor = System.Drawing.Color.Black;
			oStyle.NodeStyle.HoverBackColor = System.Drawing.Color.Pink;
			oStyle.NodeStyle.HoverForeColor = System.Drawing.Color.Black;
			oStyle.NodeStyle.Parent = null;
			oStyle.NodeStyle.SelectedBackColor = System.Drawing.Color.Pink;
			oStyle.NodeStyle.SelectedBorderColor = System.Drawing.Color.IndianRed;
			oStyle.NodeStyle.SelectedFillStyle = PureComponents.TreeView.FillStyle.Flat;
			oStyle.NodeStyle.SelectedForeColor = System.Drawing.Color.Black;
			oStyle.NodeStyle.TooltipStyle.BackColor = System.Drawing.Color.WhiteSmoke;
			oStyle.NodeStyle.TooltipStyle.BorderColor = System.Drawing.Color.FromArgb(((System.Byte)(170)), ((System.Byte)(142)), ((System.Byte)(125)));
			oStyle.NodeStyle.TooltipStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			oStyle.NodeStyle.TooltipStyle.ForeColor = System.Drawing.Color.Black;
			oStyle.NodeStyle.TooltipStyle = oStyle.NodeStyle.TooltipStyle;
			oStyle.NodeStyle.UnderlineColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(0)), ((System.Byte)(0)));
			oStyle.NodeStyle.UnderlineStyle = PureComponents.TreeView.UnderlineStyle.Solid;			
			oStyle.ShowLines = true;
			oStyle.ShowPlusMinus = true;
			oStyle.ShowSubitemsIndicator = false;
			oStyle.TrackNodeHover = true;
	
			return oStyle;
		}

		/// <summary>
		/// The gold Tree style scheme getter
		/// </summary>
		public TreeViewStyle GetGoldTreeStyle()
		{
			TreeViewStyle oStyle = GetDefaultTreeStyle();

			oStyle.AutoCollapse = false;
			oStyle.BackColor = System.Drawing.Color.White;
			oStyle.BorderColor = System.Drawing.Color.DarkGoldenrod;
			oStyle.BorderStyle = PureComponents.TreeView.BorderStyle.Solid;
			oStyle.FadeColor = System.Drawing.Color.Khaki;
			oStyle.FillStyle = PureComponents.TreeView.FillStyle.DiagonalForward;
			oStyle.FlashColor = System.Drawing.Color.Red;
			oStyle.HighlightSelectedPath = false;
			oStyle.LineColor = System.Drawing.Color.DarkGoldenrod;
			oStyle.LineStyle = PureComponents.TreeView.LineStyle.Dot;
			oStyle.NodeStyle.CheckBoxStyle.BackColor = System.Drawing.Color.White;
			oStyle.NodeStyle.CheckBoxStyle.BorderColor = System.Drawing.Color.Gold;
			oStyle.NodeStyle.CheckBoxStyle.BorderStyle = PureComponents.TreeView.CheckBoxBorderStyle.Solid;
			oStyle.NodeStyle.CheckBoxStyle.CheckColor = System.Drawing.Color.DarkGoldenrod;
			oStyle.NodeStyle.CheckBoxStyle.HoverBackColor = System.Drawing.Color.Khaki;
			oStyle.NodeStyle.CheckBoxStyle.HoverBorderColor = System.Drawing.Color.DarkGoldenrod;
			oStyle.NodeStyle.CheckBoxStyle.HoverCheckColor = System.Drawing.Color.Black;			
			oStyle.NodeStyle.ExpandBoxStyle.BackColor = System.Drawing.Color.Gold;
			oStyle.NodeStyle.ExpandBoxStyle.BorderColor = System.Drawing.Color.DarkGoldenrod;
			oStyle.NodeStyle.ExpandBoxStyle.ForeColor = System.Drawing.Color.DarkGoldenrod;
			oStyle.NodeStyle.ExpandBoxStyle.Shape = PureComponents.TreeView.ExpandBoxShape.XP;			
			oStyle.NodeStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			oStyle.NodeStyle.ForeColor = System.Drawing.Color.Black;
			oStyle.NodeStyle.HoverBackColor = System.Drawing.Color.Khaki;
			oStyle.NodeStyle.HoverForeColor = System.Drawing.Color.Black;
			oStyle.NodeStyle.Parent = null;
			oStyle.NodeStyle.SelectedBackColor = System.Drawing.Color.Khaki;
			oStyle.NodeStyle.SelectedBorderColor = System.Drawing.Color.Gold;
			oStyle.NodeStyle.SelectedFillStyle = PureComponents.TreeView.FillStyle.Flat;
			oStyle.NodeStyle.SelectedForeColor = System.Drawing.Color.Black;
			oStyle.NodeStyle.TooltipStyle.BackColor = System.Drawing.Color.Khaki;
			oStyle.NodeStyle.TooltipStyle.BorderColor = System.Drawing.Color.DarkGoldenrod;
			oStyle.NodeStyle.TooltipStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			oStyle.NodeStyle.TooltipStyle.ForeColor = System.Drawing.Color.Black;			
			oStyle.NodeStyle.UnderlineColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(0)), ((System.Byte)(0)));
			oStyle.NodeStyle.UnderlineStyle = PureComponents.TreeView.UnderlineStyle.Solid;			
			oStyle.ShowLines = true;
			oStyle.ShowPlusMinus = true;
			oStyle.ShowSubitemsIndicator = false;
			oStyle.TrackNodeHover = true;

			return oStyle;
		}

		/// <summary>
		/// The wood Tree style scheme getter
		/// </summary>
		public TreeViewStyle GetWoodTreeStyle()
		{
			TreeViewStyle oStyle = GetDefaultTreeStyle();

			oStyle.AutoCollapse = false;
			oStyle.BackColor = System.Drawing.Color.White;
			oStyle.BorderColor = System.Drawing.Color.Brown;
			oStyle.BorderStyle = PureComponents.TreeView.BorderStyle.Solid;
			oStyle.FadeColor = System.Drawing.Color.FromArgb(((System.Byte)(208)), ((System.Byte)(185)), ((System.Byte)(187)));
			oStyle.FillStyle = PureComponents.TreeView.FillStyle.DiagonalForward;
			oStyle.FlashColor = System.Drawing.Color.Red;
			oStyle.HighlightSelectedPath = false;
			oStyle.LineColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(64)), ((System.Byte)(0)));
			oStyle.LineStyle = PureComponents.TreeView.LineStyle.Dot;
			oStyle.NodeStyle.CheckBoxStyle.BackColor = System.Drawing.Color.White;
			oStyle.NodeStyle.CheckBoxStyle.BorderColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(64)), ((System.Byte)(64)));
			oStyle.NodeStyle.CheckBoxStyle.BorderStyle = PureComponents.TreeView.CheckBoxBorderStyle.Solid;
			oStyle.NodeStyle.CheckBoxStyle.CheckColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(64)), ((System.Byte)(64)));
			oStyle.NodeStyle.CheckBoxStyle.HoverBackColor = System.Drawing.Color.FromArgb(((System.Byte)(208)), ((System.Byte)(185)), ((System.Byte)(187)));
			oStyle.NodeStyle.CheckBoxStyle.HoverBorderColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(64)), ((System.Byte)(64)));
			oStyle.NodeStyle.CheckBoxStyle.HoverCheckColor = System.Drawing.Color.Black;			
			oStyle.NodeStyle.ExpandBoxStyle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(64)), ((System.Byte)(64)));
			oStyle.NodeStyle.ExpandBoxStyle.BorderColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(64)), ((System.Byte)(0)));
			oStyle.NodeStyle.ExpandBoxStyle.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(64)), ((System.Byte)(0)));
			oStyle.NodeStyle.ExpandBoxStyle.Shape = PureComponents.TreeView.ExpandBoxShape.XP;			
			oStyle.NodeStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			oStyle.NodeStyle.ForeColor = System.Drawing.Color.Black;
			oStyle.NodeStyle.HoverBackColor = System.Drawing.Color.FromArgb(((System.Byte)(208)), ((System.Byte)(185)), ((System.Byte)(187)));
			oStyle.NodeStyle.HoverForeColor = System.Drawing.Color.Black;
			oStyle.NodeStyle.Parent = null;
			oStyle.NodeStyle.SelectedBackColor = System.Drawing.Color.FromArgb(((System.Byte)(208)), ((System.Byte)(185)), ((System.Byte)(187)));
			oStyle.NodeStyle.SelectedBorderColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(64)), ((System.Byte)(64)));
			oStyle.NodeStyle.SelectedFillStyle = PureComponents.TreeView.FillStyle.Flat;
			oStyle.NodeStyle.SelectedForeColor = System.Drawing.Color.Black;
			oStyle.NodeStyle.TooltipStyle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(208)), ((System.Byte)(185)), ((System.Byte)(187)));
			oStyle.NodeStyle.TooltipStyle.BorderColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(64)), ((System.Byte)(0)));
			oStyle.NodeStyle.TooltipStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			oStyle.NodeStyle.TooltipStyle.ForeColor = System.Drawing.Color.Black;			
			oStyle.NodeStyle.UnderlineColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(0)), ((System.Byte)(0)));
			oStyle.NodeStyle.UnderlineStyle = PureComponents.TreeView.UnderlineStyle.Solid;			
			oStyle.ShowLines = true;
			oStyle.ShowPlusMinus = true;
			oStyle.ShowSubitemsIndicator = false;
			oStyle.TrackNodeHover = true;

			return oStyle;
		}

		/// <summary>
		/// The silver Tree style scheme getter
		/// </summary>
		/// <returns></returns>
		public TreeViewStyle GetSilverTreeStyle()
		{
			TreeViewStyle oStyle = GetDefaultTreeStyle();

			oStyle.AutoCollapse = false;
			oStyle.BackColor = System.Drawing.Color.White;
			oStyle.BorderColor = System.Drawing.Color.DimGray;
			oStyle.BorderStyle = PureComponents.TreeView.BorderStyle.Solid;
			oStyle.FadeColor = System.Drawing.Color.LightGray;
			oStyle.FillStyle = PureComponents.TreeView.FillStyle.DiagonalForward;
			oStyle.FlashColor = System.Drawing.Color.Red;
			oStyle.HighlightSelectedPath = false;
			oStyle.LineColor = System.Drawing.Color.DimGray;
			oStyle.LineStyle = PureComponents.TreeView.LineStyle.Dot;
			oStyle.NodeStyle.CheckBoxStyle.BackColor = System.Drawing.Color.White;
			oStyle.NodeStyle.CheckBoxStyle.BorderColor = System.Drawing.Color.Gray;
			oStyle.NodeStyle.CheckBoxStyle.BorderStyle = PureComponents.TreeView.CheckBoxBorderStyle.Solid;
			oStyle.NodeStyle.CheckBoxStyle.CheckColor = System.Drawing.Color.DimGray;
			oStyle.NodeStyle.CheckBoxStyle.HoverBackColor = System.Drawing.Color.LightGray;
			oStyle.NodeStyle.CheckBoxStyle.HoverBorderColor = System.Drawing.Color.DimGray;
			oStyle.NodeStyle.CheckBoxStyle.HoverCheckColor = System.Drawing.Color.Black;			
			oStyle.NodeStyle.ExpandBoxStyle.BackColor = System.Drawing.Color.Gray;
			oStyle.NodeStyle.ExpandBoxStyle.BorderColor = System.Drawing.Color.DimGray;
			oStyle.NodeStyle.ExpandBoxStyle.ForeColor = System.Drawing.Color.DimGray;
			oStyle.NodeStyle.ExpandBoxStyle.Shape = PureComponents.TreeView.ExpandBoxShape.XP;			
			oStyle.NodeStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			oStyle.NodeStyle.ForeColor = System.Drawing.Color.Black;
			oStyle.NodeStyle.HoverBackColor = System.Drawing.Color.LightGray;
			oStyle.NodeStyle.HoverForeColor = System.Drawing.Color.Black;
			oStyle.NodeStyle.Parent = null;
			oStyle.NodeStyle.SelectedBackColor = System.Drawing.Color.LightGray;
			oStyle.NodeStyle.SelectedBorderColor = System.Drawing.Color.Gray;
			oStyle.NodeStyle.SelectedFillStyle = PureComponents.TreeView.FillStyle.Flat;
			oStyle.NodeStyle.SelectedForeColor = System.Drawing.Color.Black;
			oStyle.NodeStyle.TooltipStyle.BackColor = System.Drawing.Color.LightGray;
			oStyle.NodeStyle.TooltipStyle.BorderColor = System.Drawing.Color.DimGray;
			oStyle.NodeStyle.TooltipStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			oStyle.NodeStyle.TooltipStyle.ForeColor = System.Drawing.Color.Black;			
			oStyle.NodeStyle.UnderlineColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(0)), ((System.Byte)(0)));
			oStyle.NodeStyle.UnderlineStyle = PureComponents.TreeView.UnderlineStyle.Solid;			
			oStyle.ShowLines = true;
			oStyle.ShowPlusMinus = true;
			oStyle.ShowSubitemsIndicator = false;
			oStyle.TrackNodeHover = true;

			return oStyle;
		}
		#endregion
	}
}
