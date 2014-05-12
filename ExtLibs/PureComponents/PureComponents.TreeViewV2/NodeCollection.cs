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
using System.Collections;

using System.ComponentModel;
using System.ComponentModel.Design;

using System.Drawing;
using System.Drawing.Design;

using System.Diagnostics;

using System.Windows.Forms;

namespace PureComponents.TreeView
{
	/// <summary>
	/// The typed and customized Node enumerator for the NodeCollection implementation
	/// </summary>
	public class NodeEnumerator : IEnumerator
	{
		#region private members
		private IEnumerator m_oEnumerator;
		private IEnumerable m_oCollection;
		#endregion
            
		#region construction
		/// <summary>
		/// The copy construction
		/// </summary>		
		public NodeEnumerator(NodeCollection oCollection) 
		{
			this.m_oCollection = ((IEnumerable)(oCollection));
			this.m_oEnumerator = m_oCollection.GetEnumerator();
		}
		#endregion
            
		#region manipulation functions
		public Node Current 
		{
			get 
			{
				return ((Node)(m_oEnumerator.Current));
			}
		}
            
		object IEnumerator.Current 
		{
			get 
			{
				return m_oEnumerator.Current;
			}
		}
            
		public bool MoveNext() 
		{
			return m_oEnumerator.MoveNext();
		}
            
		bool IEnumerator.MoveNext() 
		{
			return m_oEnumerator.MoveNext();
		}
            
		public void Reset() 
		{
			m_oEnumerator.Reset();
		}
            
		void IEnumerator.Reset() 
		{
			m_oEnumerator.Reset();
		}
		#endregion
	}

	/// <summary>
	/// 
	/// </summary>	
	public class NodeCollection : CollectionBase
	{
		private class NodeCollectionOrderComparer : IComparer
		{
			public int Compare(object x, object y)
			{
				Node a = x as Node;
				Node b = y as Node;

				if (a.m_nCollectionOrder < b.m_nCollectionOrder)
					return -1;

				if (a.m_nCollectionOrder > b.m_nCollectionOrder)
					return 1;

				return 0;
			}
		}

		#region private members
		// the parent holder
		internal Node ParentNode = null;		
		internal TreeView ParentTree = null;

		// the empty item provider
		private Node m_oNull = null;
		#endregion

		#region construction
		public NodeCollection() : base()
		{
		}

		public NodeCollection(Node oParent) : this()
		{			
			ParentNode = oParent;			
		}

		public NodeCollection(TreeView oParent) : this()
		{
			ParentTree = oParent;
		}

		public NodeCollection(NodeCollection oCollection) : this()
		{
			AddRange(oCollection);
		}	

		public NodeCollection(Node[] aItems) : this()
		{
			AddRange(aItems);
		}
		#endregion

		#region operators
		internal Node Null
		{
			get
			{
				return m_oNull;
			}
		}

		public Node this[int index] 
		{
			get 
			{
				return ((Node)(List[index]));
			}
			set 
			{
				List[index] = value;
			}
		}
		#endregion

		#region manipulation functions
		public Node Add(string sNodeText, string sNodeKey)
		{
			TreeView treeView = null;

			if (ParentTree != null)
				treeView = ParentTree;

			if (ParentNode != null && treeView == null)
				treeView = ParentNode.TreeView;

			if (treeView != null && treeView.GetNodeByKey(sNodeKey) != null)
				throw new Exception("Node with the specified key [" + sNodeKey + "] already exists. Node cannot be added.");

			Node oNode = Add(sNodeText);
			oNode.Key = sNodeKey;

			return oNode;
		}

		public Node Add(string sNodeText)
		{
			Node oNode = new Node();
			oNode._TreeView = this.ParentTree;

			oNode.Text = sNodeText;

			Add(oNode);
			oNode.SetParent(ParentNode);
			oNode.TreeView = ParentTree;

			return oNode;
		}

		public int Add(Node value) 
		{			
			TreeView treeView = null;

			if (ParentTree != null)
				treeView = ParentTree;

			if (ParentNode != null && treeView == null)
				treeView = ParentNode.TreeView;			

			if (List.Contains(value))
				return List.IndexOf(value);

			if (treeView != null && treeView.NodePool.Contains(value) == true)
				throw new Exception("Node is already added in other nodes collection.");

			value.SetNodeTreeViewReference(treeView);								
			
			int nPos = List.Add(value);			

			if (treeView != null)
			{			
				treeView.NodePool.Add(value);
				treeView.InvokeNodeAdded(value);
			}
			
			return nPos;
		}       		

		public void AddRange(Node[] value) 
		{
			foreach (Node a in value) 
			{
				Add(a);
			}
		}
        
		public void AddRange(NodeCollection value) 
		{
			foreach (Node oItem in value) 
			{
				Add(oItem);
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public void AddRange(string [] aNodes)
		{
			foreach (string sNode in aNodes)
				Add(sNode);
		}
        
		public bool Contains(Node value) 
		{
			return List.Contains(value);
		}
        
		public void CopyTo(Node[] array, int index) 
		{
			List.CopyTo(array, index);
		}
        
		public int IndexOf(Node value) 
		{
			return List.IndexOf(value);
		}
        
		public void Insert(int index, Node value) 
		{
			if (List.Contains(value))
				throw new Exception("Node is already in collection.");

			if (this.ParentNode != null)
			{
				// check if the node is not already in the set
				if (this.ParentNode.TreeView.NodePool.Contains(value) == true)
					throw new Exception("Node is already in collection.");

				value.TreeView = ParentNode.TreeView;
				value.SetParent(ParentNode);				
			}
			else
			{
				if (ParentTree != null)
				{
					// check if the node is not already in the set
					if (ParentTree.NodePool.Contains(value) == true)
						throw new Exception("Node is already in collection.");

					value.TreeView = ParentTree;					
				}
			}

			List.Insert(index, value);

			TreeView treeView = null;

			if (ParentTree != null)
				treeView = ParentTree;

			if (ParentNode != null && treeView == null)
				treeView = ParentNode.TreeView;
			
			if (treeView != null)
			{			
				treeView.NodePool.Add(value);

				foreach (Node oCollectionNode in this)
					oCollectionNode.m_nCollectionOrder = oCollectionNode.Index;

				treeView.Invalidate();

				treeView.InvokeNodeAdded(value);
			}
		}
        
		public new NodeEnumerator GetEnumerator() 
		{
			return new NodeEnumerator(this);
		}
        
		public void Remove(Node value) 
		{
			bool bIsSelected = false;

			TreeView treeView = null;

			if (ParentTree != null)
				treeView = ParentTree;

			if (ParentNode != null && treeView == null)
				treeView = ParentNode.TreeView;			

			if (treeView != null && treeView.SelectedNode == value)
				bIsSelected = true;

			List.Remove(value);

			if (treeView != null)
			{			
				treeView.InvokeNodeRemoved(value);
				treeView.NodePool.Remove(value);
				treeView.ClearNodeKey(value.Key);

				if (bIsSelected == true)
					treeView.SetSelectedNode(null);

				treeView.Invalidate();
			}
		}

		public new void RemoveAt(int nIndex)
		{			
			Node oNode = List[nIndex] as Node;			

			oNode.Remove();			
		}

		public new void Clear()
		{
			foreach (Node oNode in ToNodeArray())
				oNode.Remove();

			TreeView treeView = null;

			if (ParentTree != null)
				treeView = ParentTree;

			if (ParentNode != null && treeView == null)
				treeView = ParentNode.TreeView;

			if (treeView == null)
				return;

			treeView.Invalidate();

			if (ParentTree != null && ParentNode == null)
				treeView.SetSelectedNode(null);			
		}

		public Node [] ToNodeArray()
		{
			Node [] aNodes = new Node[List.Count];
			List.CopyTo(aNodes, 0);

			return aNodes;
		}
		#endregion

		#region helper functions
		/// <summary>
		/// Gets all nodes in the collection sorted by the m_nCollectionOrder
		/// </summary>
		/// <returns>Sorted array of Nodes</returns>
		internal Node [] GetNodesCollectionSorted()
		{
			ArrayList aSorted = new ArrayList();
			
//			SortedList aSorted = new SortedList();
//
//			foreach (Node oNode in List)
//			{
//				try
//				{
//					aSorted.Add(oNode.m_nCollectionOrder, oNode);
//				}
//				catch
//				{
//					aSorted.Add(oNode.Collection.IndexOf(oNode), oNode);
//				}
//			}

			foreach (Node oNode in List)
				aSorted.Add(oNode);

			aSorted.Sort(new NodeCollectionOrderComparer());

			Node [] aNodes = new Node[List.Count];

			for (int nIndex = 0; nIndex < aSorted.Count; nIndex ++)
				aNodes[nIndex] = aSorted[nIndex] as Node;

			return aNodes;
		}
		#endregion

		#region setter functions for all nodes in the collection
		public void SetNodesStyle(NodeStyle Style)
		{
			foreach (Node oNode in List)
				oNode.NodeStyle = Style;
		}

		public void SetNodesStyleSource(NodeStyleSource Source)
		{
			foreach (Node oNode in List)
				oNode.NodeStyleSource = Source;
		}

		public void SetNodesTag(object Tag)
		{
			foreach (Node oNode in List)
				oNode.Tag = Tag;
		}

		public void SetNodesImage(Image Image)
		{
			foreach (Node oNode in List)
				oNode.Image = Image;
		}

		public void SetNodesImageIndex(int ImageIndex)
		{
			foreach (Node oNode in List)			
				oNode.ImageIndex = ImageIndex;			
		}

		public void SetNodesContextMenu(ContextMenu ContextMenu)
		{
			foreach (Node oNode in List)
				oNode.ContextMenu = ContextMenu;
		}

		public void SetNodesContextMenuSource(ContextMenuSource ContextMenuSource)
		{
			foreach (Node oNode in List)
				oNode.ContextMenuSource = ContextMenuSource;
		}
		#endregion

		#region events
		protected override void OnSet(int index, object oldValue, object newValue) 
		{
			base.OnSet(index, oldValue, newValue);
		}
        
		protected override void OnInsert(int index, object value) 
		{
			if (value != null) 
			{
				Node oItem = (Node)value;

				TreeView treeView = null;

				if (ParentTree != null)
					treeView = ParentTree;

				if (ParentNode != null && treeView == null)
					treeView = ParentNode.TreeView;

				oItem.TreeView = treeView;
				oItem.SetParent(ParentNode);				
			}

			base.OnInsert(index, value);
		}
        
		protected override void OnClear() 
		{
			base.OnClear();
		}
        
		protected override void OnRemove(int index, object value) 
		{
			base.OnRemove(index, value);
		}
        
		protected override void OnValidate(object value) 
		{
			base.OnValidate(value);
		}
		#endregion
	}	
}
