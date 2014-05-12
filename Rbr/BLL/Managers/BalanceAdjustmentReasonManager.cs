using System.Collections.Generic;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Managers {
	internal class BalanceAdjustmentReasonManager {
		BalanceAdjustmentReasonManager() { }

		#region Getters

		internal static BalanceAdjustmentReasonDto Get(Rbr_Db pDb, int pBalanceAdjustmentReasonId) {
			var _balanceAdjustmentReasonRow = pDb.BalanceAdjustmentReasonCollection.GetByPrimaryKey(pBalanceAdjustmentReasonId);
			return mapToBalanceAdjustmentReason(_balanceAdjustmentReasonRow);
		}

		internal static BalanceAdjustmentReasonDto[] GetByType(Rbr_Db pDb, BalanceAdjustmentReasonType pBalanceAdjustmentReasonType) {
			var _balanceAdjustmentReasonRows = pDb.BalanceAdjustmentReasonCollection.GetByType(pBalanceAdjustmentReasonType);
			return mapToBalanceAdjustmentReasons(_balanceAdjustmentReasonRows);
		}

		#endregion Getters

		#region Actions

		internal static void Save(Rbr_Db pDb, BalanceAdjustmentReasonDto pBalanceAdjustmentReason) {
			var _balanceAdjustmentReasonRow = mapToBalanceAdjustmentReasonRow(pBalanceAdjustmentReason);
			if (_balanceAdjustmentReasonRow != null) {
				if (_balanceAdjustmentReasonRow.Balance_adjustment_reason_id == 0) {
					pDb.BalanceAdjustmentReasonCollection.Insert(_balanceAdjustmentReasonRow);
					pBalanceAdjustmentReason.BalanceAdjustmentReasonId = _balanceAdjustmentReasonRow.Balance_adjustment_reason_id;
				}
				else {
					pDb.BalanceAdjustmentReasonCollection.Update(_balanceAdjustmentReasonRow);
				}
			}
		}

		internal static void Delete(Rbr_Db pDb, int pBalanceAdjustmentReasonId) { pDb.BalanceAdjustmentReasonCollection.DeleteByPrimaryKey(pBalanceAdjustmentReasonId); }

		#endregion Actions

		#region mappings

		static BalanceAdjustmentReasonRow[] mapToBalanceAdjustmentReasonRows(BalanceAdjustmentReasonDto[] pBalanceAdjustmentReasons) {
			var _list = new List<BalanceAdjustmentReasonRow>();
			if (pBalanceAdjustmentReasons != null) {
				foreach (var _balanceAdjustmentReason in pBalanceAdjustmentReasons) {
					_list.Add(mapToBalanceAdjustmentReasonRow(_balanceAdjustmentReason));
				}
			}
			return _list.ToArray();
		}

		static BalanceAdjustmentReasonRow mapToBalanceAdjustmentReasonRow(BalanceAdjustmentReasonDto pBalanceAdjustmentReason) {
			if (pBalanceAdjustmentReason == null) {
				return null;
			}
			var _balanceAdjustmentReasonRow = new BalanceAdjustmentReasonRow();
			_balanceAdjustmentReasonRow.Balance_adjustment_reason_id = pBalanceAdjustmentReason.BalanceAdjustmentReasonId;
			_balanceAdjustmentReasonRow.Description = pBalanceAdjustmentReason.Description;
			_balanceAdjustmentReasonRow.BalanceAdjustmentReasonType = pBalanceAdjustmentReason.BalanceAdjustmentReasonType;

			return _balanceAdjustmentReasonRow;
		}

		static BalanceAdjustmentReasonDto[] mapToBalanceAdjustmentReasons(BalanceAdjustmentReasonRow[] pBalanceAdjustmentReasonRows) {
			var _list = new List<BalanceAdjustmentReasonDto>();
			if (pBalanceAdjustmentReasonRows != null) {
				foreach (BalanceAdjustmentReasonRow _balanceAdjustmentReasonRow in pBalanceAdjustmentReasonRows) {
					_list.Add(mapToBalanceAdjustmentReason(_balanceAdjustmentReasonRow));
				}
			}
			return _list.ToArray();
		}

		static BalanceAdjustmentReasonDto mapToBalanceAdjustmentReason(BalanceAdjustmentReasonRow pBalanceAdjustmentReasonRow) {
			if (pBalanceAdjustmentReasonRow == null) {
				return null;
			}
			var _balanceAdjustmentReason = new BalanceAdjustmentReasonDto();
			_balanceAdjustmentReason.BalanceAdjustmentReasonId = pBalanceAdjustmentReasonRow.Balance_adjustment_reason_id;
			_balanceAdjustmentReason.Description = pBalanceAdjustmentReasonRow.Description;
			_balanceAdjustmentReason.BalanceAdjustmentReasonType = pBalanceAdjustmentReasonRow.BalanceAdjustmentReasonType;

			return _balanceAdjustmentReason;
		}

		#endregion mappings
	}
}