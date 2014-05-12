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

namespace PureComponents.TreeView
{
	using System;
	using System.IO;
	using System.Windows.Forms;
	using System.Runtime.Serialization.Formatters.Binary;

	
	/// <summary>
	/// TreeView
	/// </summary>
	internal class TreeViewDataAccess
	{
		public TreeViewDataAccess(){}

		/// <summary>
		/// TreeViewData
		/// </summary>
		[Serializable()]
		internal struct TreeViewData
		{
			public TreeNodeData[] Nodes;

			/// <summary>
				
			/// </summary>
			/// <param name="treeview"></param>
			public TreeViewData(TreeView treeview)
			{
				Nodes = new TreeNodeData[treeview.Nodes.Count];
				if (treeview.Nodes.Count == 0)
				{
					return;
				}
				for (int i = 0; i <= treeview.Nodes.Count - 1; i++) 
				{
					Nodes[i] = new TreeNodeData(treeview.Nodes[i]);
				}
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="treeview"></param>
			public void PopulateTree(TreeView treeview)
			{
				if (this.Nodes == null || this.Nodes.Length == 0) 
				{
					return;
				}

				treeview.BeginInit();
				
				for (int i = 0; i <= this.Nodes.Length - 1; i++) 
				{
					this.Nodes[i].ToTreeNode()._TreeView = treeview;

					treeview.Nodes.Add(this.Nodes[i].ToTreeNode());
				}
				
				treeview.EndInit();
			}
		}

		/// <summary>
		/// TreeNodeData
		/// </summary>
		[Serializable()]
		internal struct TreeNodeData
		{
			public string Text;
			public bool CheckBoxVisible;
			public bool Visible;
			public bool ShowPlusMinus;
			public bool Checked;
			public bool Underline;

			public TreeNodeData[] Nodes;

			/// <summary>
			/// 
			/// </summary>
			/// <param name="node"></param>
			public TreeNodeData(Node node)
			{
				this.Text = node.Text;
				
				CheckBoxVisible = node.CheckBoxVisible;
				Visible = node.Visible;
				ShowPlusMinus = node.ShowPlusMinus;
				Checked = node.Checked;
				Underline = node.Underline;

				this.Nodes = new TreeNodeData[node.Nodes.Count];

				if (node.Nodes.Count == 0) 
				{
					return;
				}
				for (int i = 0; i <= node.Nodes.Count - 1; i++) 
				{
					Nodes[i] = new TreeNodeData(node.Nodes[i]);
				}
			}

			/// <summary>
			/// 
			/// </summary>
			/// <returns></returns>
			public Node ToTreeNode()
			{
				Node ToTreeNode = new Node();
				ToTreeNode.Text = this.Text;

				ToTreeNode.CheckBoxVisible = this.CheckBoxVisible;
				ToTreeNode.Visible = this.Visible;
				ToTreeNode.ShowPlusMinus = this.ShowPlusMinus;
				ToTreeNode.Checked = this.Checked;
				ToTreeNode.Underline = this.Underline;
					
				if (this.Nodes == null && this.Nodes.Length == 0) 
				{
					return null;
				}
				if(ToTreeNode != null && this.Nodes.Length == 0)
				{
					return ToTreeNode;
				}
				for (int i = 0; i <= this.Nodes.Length - 1; i++) 
				{
					ToTreeNode.Nodes.Add(this.Nodes[i].ToTreeNode());
				}
				return ToTreeNode;
			}
		}
	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="treeView"></param>
		/// <param name="path"></param>
		public static void LoadTreeViewData(TreeView treeView, string path)
		{
			BinaryFormatter ser = new BinaryFormatter();
			Stream file = new FileStream(path,FileMode.Open,FileAccess.Read,FileShare.Read);
			TreeViewData treeData = ((TreeViewData)(ser.Deserialize(file)));
			treeData.PopulateTree(treeView);
			file.Close();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="treeView"></param>
		/// <param name="stream"></param>
		public static void LoadTreeViewData(TreeView treeView, Stream stream)
		{
			BinaryFormatter ser = new BinaryFormatter();
			
			TreeViewData treeData = ((TreeViewData)(ser.Deserialize(stream)));
			
			treeData.PopulateTree(treeView);					
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="treeView"></param>
		/// <param name="path"></param>
		public static void SaveTreeViewData(TreeView treeView, string path)
		{
			BinaryFormatter ser = new BinaryFormatter();
			Stream file = new FileStream(path,FileMode.Create);
			ser.Serialize(file,new TreeViewData(treeView));
			file.Close();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="treeView"></param>
		/// <param name="stream"></param>		
		public static void SaveTreeViewData(TreeView treeView, Stream stream)
		{
			BinaryFormatter ser = new BinaryFormatter();
			
			ser.Serialize(stream, new TreeViewData(treeView));

			stream.Flush();
		}
	}
}

