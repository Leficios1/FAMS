using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Constants
{
    public static class OrderByConstant
    {
        public const string ClassTime_Morning = "morning";
        public const string ClassTime_Afternoon = "afternoon";
        public const string ClassTime_Evening = "evening";
        public const string ClassTime_Noon = "noon";

        public const string ClassTime_MorningBeginRange = "07:00:00";
        public const string ClassTime_MorningEndRange = "12:00:00";
        public const string ClassTime_NoonBeginRange = "12:00:00";
        public const string ClassTime_NoonEndRange = "14:00:00";
        public const string ClassTime_AfternoonBeginRange = "14:00:00";
        public const string ClassTime_AfternoonEndRange = "17:00:00";
        public const string ClassTime_EveningBeginRange = "17:00:00";
        public const string ClassTime_EveningEndRange = "19:00:00";

        public const string OrderByASC = "asc";
        public const string OrderByDESC = "desc";

        public const string SortBy_Id = "id";
        public const string SortBy_Name = "name"; 
        public const string SortBy_Code = "code";
        public const string SortBy_CreatedBy = "createdby";
        public const string SortBy_CreatedDate = "createddate";
        public const string SortBy_ModifiedDate = "modifieddate";
        public const string SortBy_Duration = "duration";
        public const string SortBy_Attendee = "attendee"; 
        public const string SortBy_Location ="location";
        public const string SortBy_Status = "status";
        public const string SortBy_FSU = "fsu";

        public const string SortBy_OutputStandards = "outputstandards";

        public const string SortBy_Email = "email";
        public const string SortBy_DateOfBirth = "dateofbirth";
        public const string SortBy_Gender = "gender";
        public const string SortBy_Rolename = "rolename";
        public const string SortBy_Phone = "phone";
    }
}
