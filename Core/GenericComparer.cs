using System;
using System.Collections;
using System.ComponentModel;

namespace Timok.Core {

	/// <summary>     
	/// Class used to sort objects     
	/// </summary>    
	public class GenericComparer : IComparer {    
		private ArrayList sortInfos;    
		/// <summary>    
		/// The ArrayList of sorting info    
		/// </summary>    
		public ArrayList SortInfos {    
			get { return sortInfos; }    
		}

		/// <summary>
		/// Default Constructor
		/// </summary>
		public GenericComparer() {    
			sortInfos = new ArrayList();    
		}

		/// <summary>    
		/// Constructor that takes an array of sorting info    
		/// </summary>    
		/// <param name="pSortInfos">The prebuilt array of sort information</param>
		public GenericComparer(SortInfo[] pSortInfos) {
			sortInfos = new ArrayList();
			foreach (SortInfo _sortInfo in pSortInfos) {
				sortInfos.Add(_sortInfo);
			}
		}

		/// <summary>
		/// Constructor that takes the information about one sort
		/// </summary>
		/// <param name="pSortColumn">The column to sort on</param>
		/// <param name="pSortDirection">The direction to sort</param>
		public GenericComparer(string pSortColumn, ListSortDirection pSortDirection) {
			sortInfos = new ArrayList();
			sortInfos.Add(new SortInfo(pSortColumn, pSortDirection));
		}

		/// <summary>
		/// IComparer interface implementation to compare two objects
		/// </summary>
		/// <param name="x">Object 1</param>
		/// <param name="y">Object 2</param>
		/// <returns></returns>
		public int Compare(object x, object y) {
			if (sortInfos.Count == 0) {
				return 0;
			}
			return compare(0, x, y);
		}

		/// <summary>
		/// Recursive function to do sorting
		/// </summary>
		/// <param name="pSortLevel">The current level we are sorting at</param>
		/// <param name="pObj1">Object 1</param>
		/// <param name="pObj2">Object 2</param>
		/// <returns></returns>
		private int compare(int pSortLevel, object pObj1, object pObj2) {
			int returnVal = 0;
			if (sortInfos.Count - 1 >= pSortLevel) {
				object valueOf1 = pObj1.GetType().GetProperty(((SortInfo) sortInfos[pSortLevel]).SortColumn).GetValue(pObj1, null);
				object valueOf2 = pObj2.GetType().GetProperty(((SortInfo) sortInfos[pSortLevel]).SortColumn).GetValue(pObj2, null);
				
				if (((SortInfo) sortInfos[pSortLevel]).SortDirection == ListSortDirection.Ascending) {
					//returnVal = ((IComparable) valueOf1).CompareTo(valueOf2);
					returnVal = compare(valueOf1, valueOf2);
				}
				else {
					//returnVal = ((IComparable) valueOf2).CompareTo(valueOf1);
					returnVal = compare(valueOf2, valueOf1);
				}

				if (returnVal == 0) {
					returnVal = compare(pSortLevel + 1, pObj1, pObj2);
				}
			}
			return returnVal;
		}

		private int compare(object value1, object value2) {
			IComparable _v1 = null;
			IComparable _v2 = null;

			if (value1 is IComparable) {
				_v1 = (IComparable) value1;
			}
			else {
				_v1 = (IComparable) value1.ToString();
			}

			if (value2 is IComparable) {
				_v2 = (IComparable) value2;
			}
			else {
				_v2 = (IComparable) value2.ToString();
			}

			if (_v1 == null && _v2 == null) {
				//x(null) and y(null) are equal
				return 0;
			}
			else if (_v1 == null && _v2 != null) {
				//null is always less then anything else
				return -1;
			}
			else {
				//Compare actual values
				return _v1.CompareTo(_v2);
			}

		}
	}
	
//	/// <summary>
//	/// Enumeration to determine sorting direction
//	/// </summary>
//	public enum SortDirection {
//		/// <summary>Sort Ascending</summary>
//		Ascending = 1,
//		/// <summary>Sort Descending</summary>
//		Descending = 2
//	}
	/// <summary>
	/// Class used to hold sort information
	/// </summary>
	public class SortInfo {
		/// <summary>
		/// Default constructor taking a column and a direction
		/// </summary>
		/// <param name="SortColumn">The column to sort on</param>
		/// <param name="SortDirection">The direction to sort.</param>
		public SortInfo(string SortColumn, ListSortDirection SortDirection) {
			this.SortColumn = SortColumn;
			this.SortDirection = SortDirection;
		}

		private string _sortColumn;
		/// <summary>
		/// The column to sort on
		/// </summary>
		public string SortColumn {
			get { return _sortColumn; }
			set { _sortColumn = value; }
		}

		private ListSortDirection sortDirection;
		/// <summary>
		/// The direction to sort
		/// </summary>
		public ListSortDirection SortDirection {
			get { return sortDirection; }
			set { sortDirection = value; }
		}
	}
}