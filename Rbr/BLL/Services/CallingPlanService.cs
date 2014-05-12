using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Services {
  public class CallingPlanService {
    private CallingPlanService() { }

    public static CountryDto GetCountryById(int pCountryId) {
      return CallingPlanController.GetCountry(pCountryId);
    }

    public static CountryDto GetCountryByCode(int pCountryCode) {
      return CallingPlanController.GetCountryByCountryCode(pCountryCode);
    }
  }
}
