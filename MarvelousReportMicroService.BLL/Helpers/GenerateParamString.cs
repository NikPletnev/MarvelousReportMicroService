using MarvelousReportMicroService.DAL.Enums;

namespace MarvelousReportMicroService.BLL.Helpers
{
    public static class GenerateParamString
    {
        public static string Generate(LeadSearchParams? enumParam, string searchedString)
        {
            switch (enumParam)
            {
                case LeadSearchParams.Start:
                    return $"{searchedString}%";
                case LeadSearchParams.End:
                    return $"%{searchedString}";
                case LeadSearchParams.Contains:
                    return $"%{searchedString}%";
                case LeadSearchParams.Equals:
                    return $"{searchedString}";
                default:
                    return null;
            }
        }
    }
}
