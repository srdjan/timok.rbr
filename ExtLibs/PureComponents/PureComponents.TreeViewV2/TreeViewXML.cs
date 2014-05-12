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
using System.Web;
using System.Xml;
using PureComponents.TreeView;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;

internal class TreeViewXML
{
	public bool TreeViewStyle;
	public bool TreeViewContent;
	internal XmlTextWriter xtw;
	internal XmlTextReader xtr;
	internal PureComponents.TreeView.TreeView oTV;

	internal struct structFont
	{
		public string Name;
		public float Size;
		public FontStyle Style;
		public GraphicsUnit Unit;
		public byte gdiCharSet;
		public bool gdiVerticalFont;
	}

	public TreeViewXML(PureComponents.TreeView.TreeView objectTreeView)
	{
		//
		// TODO: Add constructor logic here
		//
		oTV = objectTreeView;

		TreeViewContent = true;
		TreeViewStyle = true;
	}
	
	public void SaveToXML(System.IO.Stream oStream, System.Text.Encoding Encoding)
	{			 			

		xtw = new System.Xml.XmlTextWriter(oStream, Encoding);

		xtw.Formatting = Formatting.Indented;
		xtw.Indentation = 4;
		xtw.IndentChar = ' ';
			
		xtw.WriteStartDocument();
			
		xtw.WriteStartElement("TreeView");

		//ukladani stylu TV
		if (this.TreeViewStyle)
		{
			xtw.WriteStartElement("TreeViewStyles");
			
			SaveObject(oTV.Style, "Style");

			xtw.WriteFullEndElement();
		}


		if (this.TreeViewContent)
		{
			xtw.WriteStartElement("TreeViewContent");
		
			xtw.WriteStartElement("Behavior");
			xtw.WriteAttributeString("CheckBoxes",oTV.CheckBoxes.ToString());
			xtw.WriteAttributeString("Flags",oTV.Flags.ToString());
			xtw.WriteAttributeString("Multiline",oTV.Multiline.ToString());
			xtw.WriteAttributeString("NodeAutoNumbering",oTV.NodeAutoNumbering.ToString());
			xtw.WriteAttributeString("Sorted",oTV.Sorted.ToString());
			xtw.WriteFullEndElement();

			SaveNodes(oTV.Nodes);

			xtw.WriteFullEndElement();
		}

		xtw.WriteEndDocument();

		xtw.Flush();
		
	}

	public void SaveToXML(string FileName, System.Text.Encoding Encoding)
	{

		System.IO.Stream sw = System.IO.File.Create(FileName);

		this.SaveToXML(sw, Encoding);
			
		sw.Close();			

	}

	public void LoadFromXML(string FileName)
	{

		System.IO.Stream sr = System.IO.File.OpenRead(FileName);

		this.LoadFromXML(sr);
			
		sr.Close();			

	}

	public void LoadFromXML(System.IO.Stream oStream)
	{
		xtr = new System.Xml.XmlTextReader(oStream);

		while (xtr.Read())
		{
			if (xtr.IsStartElement())
			{
				if ((xtr.LocalName.ToUpper() == "TREEVIEWSTYLES") && (this.TreeViewStyle))
					LoadObject(oTV, xtr.ReadInnerXml(), oTV.Style.NodeStyle);

				if ((xtr.LocalName.ToUpper() == "TREEVIEWCONTENT") && (this.TreeViewContent))
					LoadNodes(oTV, xtr.ReadInnerXml());
			}
		}
	}

	private void SaveNodes(NodeCollection nc)
	{
		for (int i=0; i<nc.Count; i++)
		{
			try
			{
				xtw.WriteStartElement("Node");
				xtw.WriteAttributeString("CheckBoxVisible",nc[i].CheckBoxVisible.ToString());
				xtw.WriteAttributeString("FlagVisible",nc[i].FlagVisible.ToString());
				xtw.WriteAttributeString("ImageIndex",nc[i].ImageIndex.ToString());
				xtw.WriteAttributeString("Checked",nc[i].Checked.ToString());
				xtw.WriteAttributeString("NodeStyleSource",nc[i].NodeStyleSource.ToString());				
				xtw.WriteAttributeString("Underline",nc[i].Underline.ToString());
				xtw.WriteAttributeString("Visible",nc[i].Visible.ToString());

				if (nc[i].Key != null)
					xtw.WriteAttributeString("Key",nc[i].Key.ToString());

				if (nc[i].Tag is string)
				{
					xtw.WriteAttributeString("Tag",nc[i].Tag.ToString());
				}				
				
				SaveMultiLineText(nc[i].Text, "Text");
				
				SaveObject(nc[i].Flag, "Flag");

				SaveMultiLineText(nc[i].Tooltip, "Tooltip");

				if (nc[i].NodeStyleSource == PureComponents.TreeView.NodeStyleSource.Local)
				{
					SaveObject(nc[i].NodeStyle, "NodeStyle");
				}



				if (nc[i].Nodes.Count > 0)
				{
					SaveNodes(nc[i].Nodes);
				}

				xtw.WriteFullEndElement();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.ToString());										
			}

		}

	}

	private void SaveObject(object obj, string TagName)
	{

		if (obj == null) return;

		Type t = obj.GetType();		

		PropertyInfo[] pi = t.GetProperties();		
		
		xtw.WriteStartElement(TagName);

		for (int i=0; i<pi.GetLength(0); i++)
		{		
			object oValue = pi[i].GetValue(obj,null);			

			if ((oValue != null) && (pi[i].CanRead))
			{
				string sValue = oValue.ToString();
						
				if (! IsChildObject(oValue.GetType()))
				{
					switch (oValue.GetType().FullName)
					{
						case "System.Drawing.Color" :
							System.Drawing.Color col = (System.Drawing.Color)oValue;
							xtw.WriteAttributeString(pi[i].Name, col.ToArgb().ToString("x"));							
							break;

						case "System.Drawing.FontFamily" :
							break;

						case "PureComponents.TreeView.Node" : 
							break;

						default : 
							xtw.WriteAttributeString(pi[i].Name, oValue.ToString());
							break;

					}
				}
			}
		}		

		for (int i=0; i < pi.GetLength(0); i++)
		{		
			object oValue = pi[i].GetValue(obj,null);

			if (oValue != null)
			{
				string sValue = oValue.ToString();
						
				if (IsChildObject(oValue.GetType()))				
					SaveObject(oValue, pi[i].Name);
			}
		}

		xtw.WriteFullEndElement();
	}

	bool IsChildObject(object obj)
	{
		string sValue = obj.ToString();

		switch (sValue)
		{
			case "PureComponents.TreeView.NodeStyle" : return true;
			case "PureComponents.TreeView.ExpandBoxStyle" :	return true;
			case "PureComponents.TreeView.CheckBoxStyle" : return true;
			case "PureComponents.TreeView.NodeTooltipStyle" : return true;			
			case "System.Drawing.Font" : return true;
			default : return false;					
		}
	}
	private void SaveMultiLineText(string sText, string TagName)
	{
		
		if (sText == null) return;

		int j;

		xtw.WriteStartElement(TagName);

		do
		{
			j = sText.IndexOf(((char)13).ToString() + ((char)10).ToString());
			if (j>0) 
				sText = sText.Remove(j,1);
			else
				j = sText.IndexOf((char)10);	

			if (j > 0)
			{
				xtw.WriteString(sText.Substring(0,j));
				xtw.WriteStartElement("br");
				xtw.WriteEndElement();
				sText = sText.Substring(j+1);
			}
			else
				xtw.WriteString(sText);						

		} while (j > 0);				

		xtw.WriteFullEndElement();

	}

	private string LoadMultiLineText(string s)
	{
		if (s == null) return null;

		int j = 0;
		int i;

		i = s.IndexOf((char)13);
		while (i > 0)
		{
			s = s.Remove(i, 1);
			i = s.IndexOf((char)13);
		}

		i = s.IndexOf((char)10);
		while (i > 0)
		{
			s = s.Remove(i, 1);
			i = s.IndexOf((char)10);
		}

		i = s.IndexOf("<br");				

		while (i > 0)
		{
			j = s.IndexOf(">", i);

			s = s.Substring(0, i) + (char)13 + (char)10 + s.Substring(j + 1);

			i = s.IndexOf("<br");			
		}

		return s;
	}

	private void LoadObject(object objParent, string sXML, NodeStyle ns)
	{
		if (sXML.Length == 0) return;

		XmlTextReader tr = new XmlTextReader(sXML, System.Xml.XmlNodeType.Element , null);		

		bool NextElement = true;

		while (NextElement)
		{
			while (tr.Read())
				if (tr.IsStartElement()) break;

			string ElementName = tr.LocalName;

			if (ElementName.Length == 0) break;

			Type t1 = objParent.GetType();			
			
			PropertyInfo pi1 = t1.GetProperty(ElementName);
			
			this.SetPropertyObject(objParent, pi1, tr, ns);

			//hledani koncoveho tagu
			int j = 0;
			while (tr.Read())
				if (tr.LocalName.ToUpper() == ElementName.ToUpper())
				{
					if (tr.IsStartElement()) j++;
					else j--;
			
					if (j == -1) break;
				}
			
			if (tr.ReadState == ReadState.EndOfFile) NextElement = false;						

		}
		
		tr.Close();

		//zpracovani child tagu
		tr = new XmlTextReader(sXML, System.Xml.XmlNodeType.Element , null);

		NextElement = true;

		while (NextElement)
		{
			while (tr.Read())
				if (tr.IsStartElement()) break;

			string ElementName = tr.LocalName;

			if (ElementName.Length == 0) break;

			Type t1 = objParent.GetType();
			
			PropertyInfo pi1 = t1.GetProperty(ElementName);			

			object obj = (System.Type.GetType(pi1.GetType().Name));
			obj = pi1.GetValue(objParent,null);
			
			this.LoadObject(obj, tr.ReadInnerXml(), ns);

		}

		tr.Close();

	}

	private void LoadNodes(object objParent, string sXML)
	{
		if (sXML.Length == 0) return;

		XmlTextReader tr = new XmlTextReader(sXML, System.Xml.XmlNodeType.Element , null);

		while (tr.ReadState != ReadState.EndOfFile)
		{

			while (tr.Read())
				if (tr.IsStartElement()) break;

			string ElementName = tr.LocalName;

			if (ElementName.Length == 0) break;

			switch (ElementName.ToUpper())
			{
				case "BEHAVIOR" :

					for (int i = 0; i < tr.AttributeCount; i++)
					{						

						tr.MoveToAttribute(i);						

						SetProperty(oTV, tr.Name, tr.Value);

					}
					
					break;

				case "NODE" :

					while (tr.ReadState != ReadState.EndOfFile)
					{
						if (tr.LocalName.ToUpper() == "NODE")
						{
							Node n = null;

							string s = tr.ReadOuterXml();

							if (s.Length > 0)
							{
								n = LoadNode(s);
					
								if (n != null) oTV.Nodes.Add(n);
							}
						}
						else tr.Read();
					}

					break;
			}

		}
	}

	private Node LoadNode(string sXML)
	{
		Node ReturnedNode = null;

		if (sXML.Length == 0) return ReturnedNode;

		ReturnedNode = new Node();
		ReturnedNode.TreeView = oTV;

		XmlTextReader tr = new XmlTextReader(sXML, System.Xml.XmlNodeType.Element , null);		

		while (tr.Read())
			if (tr.IsStartElement()) break;

		//nastavi atributy nodu
		for (int i = 0; i < tr.AttributeCount; i++)
		{

			tr.MoveToAttribute(i);

			SetProperty(ReturnedNode, tr.Name, tr.Value);

		}

		bool NextElement = true;


		while (NextElement)
		{
			while (tr.Read())
				if (tr.IsStartElement()) break;

			string ElementName = tr.LocalName;

			if (ElementName.Length == 0) break;

			switch (ElementName.ToUpper())
			{
				case "TEXT" :
					string s = this.LoadMultiLineText(tr.ReadString());
					
					SetProperty(ReturnedNode, "Text", s);

					break;

				case "FLAG" :
					for (int i = 0; i < tr.AttributeCount; i++)
					{
						tr.MoveToAttribute(i);

						SetProperty(ReturnedNode.Flag, tr.Name, tr.Value);
					}
					break;

				case "NODESTYLE" :
					this.LoadObject(ReturnedNode, tr.ReadOuterXml(), ReturnedNode.NodeStyle);
					break;

				case "NODE" :				
					Node n = LoadNode(tr.ReadOuterXml());					
					if (n != null)
					{
						ReturnedNode.Nodes.Add(n);
						//n.Parent = ReturnedNode;
						//n.TreeView = ReturnedNode.TreeView;
					}
					break;

			}
		}

		return ReturnedNode;
	}

	private void SetPropertyObject(object objParent, PropertyInfo pi, XmlTextReader tr, NodeStyle ns)
	{
		object obj;

		switch (pi.PropertyType.FullName)
			{
				case "System.Drawing.Font" :
					structFont fnt;
					fnt.Style = 0;
					fnt.Unit = GraphicsUnit.Point;
					fnt.Name="";
					fnt.Size=0;
					fnt.gdiCharSet=0;
					fnt.gdiVerticalFont=false;

					for (int i = 0; i < tr.AttributeCount; i++)
					{
						tr.MoveToAttribute(i);

						switch (tr.Name.ToUpper())
						{
							case "NAME" : fnt.Name = tr.Value; break;
							case "SIZE" : fnt.Size = Convert.ToSingle(tr.Value); break;
							case "GDICHARSET" : fnt.gdiCharSet = Convert.ToByte(tr.Value); break;
							case "GDIVERTICALFONT" : fnt.gdiVerticalFont = Convert.ToBoolean(tr.Value); break;
							case "BOLD" : if (Convert.ToBoolean(tr.Value)) fnt.Style = fnt.Style | FontStyle.Bold ; break;
							case "ITALIC" : if (Convert.ToBoolean(tr.Value)) fnt.Style = fnt.Style | FontStyle.Italic ; break;
							case "STRIKEOUT" : if (Convert.ToBoolean(tr.Value)) fnt.Style = fnt.Style | FontStyle.Strikeout ; break;
							case "UNDERLINE" : if (Convert.ToBoolean(tr.Value)) fnt.Style = fnt.Style | FontStyle.Underline; break;
							case "UNIT" : 
								Type o = oTV.Style.NodeStyle.Font.Unit.GetType();
								fnt.Unit = (System.Drawing.GraphicsUnit)System.Enum.Parse(o , tr.Value, true);
								break;

							default :
								break;
						}
					}

					System.Drawing.Font NewFont = new System.Drawing.Font(fnt.Name, fnt.Size, fnt.Style, fnt.Unit, fnt.gdiCharSet, fnt.gdiVerticalFont);

				Type t1 = objParent.GetType();

				switch (t1.FullName)
				{
					case "PureComponents.TreeView.NodeStyle" :						
						ns.Font = NewFont;
						break;

					case "PureComponents.TreeView.NodeTooltipStyle" :
						ns.TooltipStyle.Font = NewFont;
						break;

				}

					break;

				default :
					

					obj = (System.Type.GetType(pi.GetType().Name));
					obj = pi.GetValue(objParent,null);

					for (int i = 0; i < tr.AttributeCount; i++)
					{

						tr.MoveToAttribute(i);

						SetProperty(obj, tr.Name, tr.Value);

					}
					break;
			}

	}

	private void SetProperty(object obj, string sPropertyName, string sValue)
	{
		Type t2 = obj.GetType();

		PropertyInfo pi = t2.GetProperty(sPropertyName);

		if (pi == null) return;

		switch (pi.PropertyType.ToString())
		{
			case "System.Drawing.Color" :							
				try 
				{ 
					pi.SetValue(obj, System.Drawing.Color.FromArgb(int.Parse(sValue)), null); 
				} 
				catch 
				{
					pi.SetValue(obj, System.Drawing.Color.FromArgb(int.Parse(sValue, System.Globalization.NumberStyles.HexNumber)), null);
				}

				break;

			case "System.Boolean" :
				pi.SetValue(obj, Convert.ToBoolean(sValue), null);
				break;

			case "System.Int32" :
				pi.SetValue(obj, Convert.ToInt32(sValue), null);
				break;

			case "System.String" :				
				pi.SetValue(obj, sValue, null);
				break;

			case "System.Object" :
				pi.SetValue(obj, sValue, null);
				break;

			default :		
				pi.SetValue(obj, System.Enum.Parse(pi.PropertyType, sValue, true), null);
				break;
		}

	}

}
