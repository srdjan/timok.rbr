using System;
using Timok.Logger;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Controllers {
	public interface INumberPortabilityRepository {
		void Upsert(string pRouteId, string pRoutingNumber, string pTelephoneNumber);
		void Delete(string pRouteId, string pRoutingNumber, string pTelephoneNumber);
	}

	public class NumberPortabilityController : INumberPortabilityRepository {
		public void Upsert(string pRouteId, string pRoutingNumber, string pDialedNumber) {
			try {
				int _routeId;
				int _routingNumber;
				long _dialedNumber;
				prepare(pRouteId, pRoutingNumber, pDialedNumber, out _routeId, out _routingNumber, out _dialedNumber);

				BaseRouteDto _route;
				DialCodeDto _dialCode;
				getRouteAndDialCode(_routeId, _routingNumber, _dialedNumber, out _route, out _dialCode);

				//_route.RoutingNumber == _routingNumber,	so:
				// insert pDialed Number, duplicate error is Ok, log and swallow
				CallingPlanController.AddDialCode(_dialCode);
       
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "NumberPortabilityController.Upsert", string.Format("Exception: {0}-{1}-{2}\r\n{3}", pRoutingNumber, pRouteId, pDialedNumber, _ex));
			}
			TimokLogger.Instance.LogRbr(LogSeverity.Info, "NumberPortabilityController.Upsert", string.Format("Updated: {0}-{1}-{2}", pRoutingNumber, pRouteId, pDialedNumber));
		}

		public void Delete(string pRouteId, string pRoutingNumber, string pDialedNumber) {
			try {
				int _routeId;
				int _routingNumber;
				long _dialedNumber;
				prepare(pRouteId, pRoutingNumber, pDialedNumber, out _routeId, out _routingNumber, out _dialedNumber);

				BaseRouteDto _route;
				DialCodeDto _dialCode;
				getRouteAndDialCode(_routeId, _routingNumber, _dialedNumber, out _route, out _dialCode);

				//_route.RoutingNumber == _routingNumber,	so:
				// insert pDialed Number, duplicate error is Ok, log and swallow
				CallingPlanController.DeleteDialCode(_dialCode);
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "NumberPortabilityController.Delete", string.Format("Exception:  {0}-{1}-{2}\r\n{3}", pRoutingNumber, pRouteId, pDialedNumber, _ex));
			}
			TimokLogger.Instance.LogRbr(LogSeverity.Info, "NumberPortabilityController.Delete", string.Format("Deleted:  {0}-{1}-{2}", pRoutingNumber, pRouteId, pDialedNumber));
		}

		void getRouteAndDialCode(int pRouteId, int pRoutingNumber, long pDialedNumber, out BaseRouteDto pRoute, out DialCodeDto pDialCode) {
			pRoute = RoutingControllerFactory.Create().GetRoute(pRouteId);
			if (pRoute == null) {
				throw new Exception(string.Format("RouteId NOT found, {0}", pRouteId));
			}

			if (pRoute.RoutingNumber > 0 && pRoute.RoutingNumber != pRoutingNumber) {
				throw new Exception(string.Format("Routing Numbers different, {0} != {1}", pRoute.RoutingNumber, pRoutingNumber));
			}

			if (pRoute.RoutingNumber == 0) {
				// set Routing number and insert pDialed Number, duplicate error is Ok, log and swallow
				pRoute.RoutingNumber = pRoutingNumber;
				RoutingControllerFactory.Create().UpdateBaseRoute(pRoute);
			}

			pDialCode = new DialCodeDto
			{
				BaseRouteId = pRoute.BaseRouteId,
				CallingPlanId = pRoute.CallingPlanId,
				Code = pDialedNumber,
				Version = 0
			};
		}

		//----------------------------------------- private ----------------------------------------------

		void prepare(string pRouteId, string pRoutingNumber, string pDialedNumber, out int _routeId, out int _routingNumber, out long _dialedNumber) {
			if (string.IsNullOrEmpty(pRouteId)) {
				throw new Exception("RoutingID null or empty");
			}
			Int32.TryParse(pRouteId, out _routeId);

			if (string.IsNullOrEmpty(pRoutingNumber)) {
				throw new Exception("Routing Number null or empty");
			}
			Int32.TryParse(pRoutingNumber, out _routingNumber);

			if (string.IsNullOrEmpty(pDialedNumber)) {
				throw new Exception("Dialed Number null or empty");
			}
			Int64.TryParse(pDialedNumber, out _dialedNumber);
		}
	}
}