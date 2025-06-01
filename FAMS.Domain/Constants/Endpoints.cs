using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Constants
{
    public static class Endpoints
    {
        public const string GetSyllabuses = "api/fams/syllabuses";

        public const string TimeAllocation = "api/fams/syllabuses/time-allocation/{id}";

        public const string DetailSyllabus = "api/fams/syllabuses/{id}";

        public const string OutlineSyllabus = "api/fams/syllabuses/outline/{id}";

        public const string SyllabusCard = "api/fams/syllabuses/syllabus-card/{programCode}";

        public const string CreateSyllabusGeneralTab = "api/fams/syllabuses/create-syllabus-general-tab";

        public const string DuplicateSyllabus = "api/fams/syllabuses/duplicate-syllabus/{id}";

        public const string CreateSyllabusOutline = "api/fams/syllabuses/create-syllabus-outline";

        public const string CreateSyllabusOtherScreen = "api/fams/syllabuses/create-syllabus-otherscreen";

        public const string ImportExcel = "api/fams/syllabuses/import-excel";

        public const string CreateSyllabus = "api/fams/syllabus/create-syllabus";

        public const string UpdateSyllabus = "api/fams/syllabus";

        public const string DeleteSyllabus = "api/fams/syllabuses/{id}";

        // training program 

        public const string ViewTrainingProgramRoute = "api/fams/training-programs/{id}";

        public const string GetTrainingProgramsRoute = "api/fams/training-programs";

        public const string DuplicateTrainingProgramRoute = "api/fams/training-programs/{id}/duplicate-training-program";

        public const string ImportExcelRoute = "api/fams/importExcel";

        public const string CreateTrainingProgramRoute = "api/fams/training-programs";

        public const string UpdateTrainingProgramRoute = "api/fams/training-programs";

        public const string ChangeStatusTrainingProgramRoute = "api/fams/training-programs/{id}/change-status";

        // user 
        public const string SearchUsersRoute = "api/fams/users";

        public const string GetUserRoute = "api/fams/users/{id}";
        // class 
        public const string ViewDetailRoute = "api/fams/classes/{id}";
        public const string SearchClassOnListRoute = "api/fams/classes";
        public const string CreateClassRoute = "api/fams/classes";
        public const string UpdateClass01Route = "api/fams/classes";

        public static string[] GetSyllabusEndpoints_FullAccess()
        {
            return new string[] {
                GetSyllabuses,
                TimeAllocation,
                DetailSyllabus,
                OutlineSyllabus,
                SyllabusCard,
                CreateSyllabusGeneralTab,
                DuplicateSyllabus,
                CreateSyllabusOutline,
                CreateSyllabusOtherScreen,
                ImportExcel,
                CreateSyllabus,
                UpdateSyllabus,
                DeleteSyllabus
            };
        }

        public static string[] GetTrainingProgramEndpoints_FullAccess()
        {
            return new string[] {
                ViewTrainingProgramRoute,
                GetTrainingProgramsRoute,
                DuplicateTrainingProgramRoute,
                ImportExcelRoute,
                CreateTrainingProgramRoute,
                UpdateTrainingProgramRoute,
                ChangeStatusTrainingProgramRoute
            };
        }

        public static string[] GetUserEndpoints_FullAccess()
        {
            return new string[] {
                SearchUsersRoute,
                GetUserRoute
            };
        }

        public static string[] GetClassEndpoints_FullAccess()
        {
            return new string[] {
                ViewDetailRoute,
                SearchClassOnListRoute,
                CreateClassRoute,
                UpdateClass01Route
            };
        }
        
        public static string[] GetSyllabusEndpoints_CreateAndViewMode()
        {
            return new string[]
            {
                CreateSyllabus,CreateSyllabusGeneralTab,CreateSyllabusOtherScreen,CreateSyllabusOutline
                ,GetSyllabuses,DetailSyllabus,OutlineSyllabus,TimeAllocation,DuplicateSyllabus,SyllabusCard
            };
        }
        public static string[] GetTrainingProgramEndPoints_CreateAndViewMode()
        {
            return new string[]
            {
                GetTrainingProgramsRoute,ViewTrainingProgramRoute,CreateTrainingProgramRoute,DuplicateTrainingProgramRoute
            };
        }
        public static string[] GetClassEndPoints_CreateAndViewMode()
        {
            return new string[]
            {
                CreateClassRoute,ViewDetailRoute,SearchClassOnListRoute
            };
        }
        public static string[] GetUserEndPoints_CreateAndViewMode()
        {
            return new string[]
            {
                SearchClassOnListRoute,GetUserRoute
            };
        }
        public static string[] GetSyllabusEndPoints_UpdateAndViewMode()
        {
            return new string[]
        {
                UpdateSyllabus
                ,GetSyllabuses,DetailSyllabus,OutlineSyllabus,TimeAllocation,DuplicateSyllabus,SyllabusCard
        };
        }
        public static string[] GetTrainingProgramEndPoints_UpdateAndViewMode()
        {
            return new string[]
            {
                GetTrainingProgramsRoute,ViewTrainingProgramRoute,UpdateTrainingProgramRoute,ChangeStatusTrainingProgramRoute
            };
        }
        public static string[] GetClassEndPoints_UpdateAndViewMode()
        {
            return new string[]
            {
                UpdateClass01Route,ViewDetailRoute,SearchClassOnListRoute
            };
        }
        public static string[] GetUserEndPoints_UpdateAndViewMode()
        {
            return new string[]
            {
                GetUserRoute,GetUserRoute
            };
        }

    }
}
