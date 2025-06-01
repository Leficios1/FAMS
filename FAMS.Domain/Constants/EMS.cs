using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Constants
{
    public static class EMS
    {
        // Create/update user
        public const string EM01 = "User type is required.";
        public const string EM02 = "Name is required.";
        public const string EM03 = "Email address is required.";
        public const string EM04 = "Email address is existed. Please check and input another email address.";
        public const string EM05 = "Email address is invalid. Please check and input again.";
        public const string EM06 = "Phone is required.";
        public const string EM07 = "Phone is invalid. Please check and input again.";
        public const string EM08 = "Date of birth is required.";
        public const string EM09 = "User is created successfully.";
        public const string EM10 = "User is updated successfully.";
        public const string EM81 = "User not found";

        //Update role permission
        public const string EM11 = "Role is updated successfully.";

        //Create syllabus(general)
        public const string EM12 = "Syllabus name is required.";
        public const string EM13 = "Level is required.";
        public const string EM14 = "Attendee number is required.";
        public const string EM15 = "Technical requirement(s) is required.";
        public const string EM16 = "Course objectives are required.";

        //Create syllabus(outline)
        public const string EM17 = "Unit name is required.";
        public const string EM18 = "Content name is required.";
        public const string EM19 = "Output standard is required.";
        public const string EM20 = "Training time is required.";
        public const string EM21 = "Delivery type is required.";
        public const string EM22 = "Delete all content of the Day?";
        public const string EM23 = "Please input at least one day.";
        public const string EM24 = "Please input at least one unit into this day.";
        public const string EM25 = "Please input at least one content into this unit.";
        public const string EM26 = "The duration exceeds 8 hours per day. Please check again.";
        public const string EM72 = "Not Found Topic Code";
        public const string EM73 = "Can't add UnitTraining";

        //Create Syllabus (Others- Assessment scheme)
        public const string EM27 = "Quiz is required.";
        public const string EM28 = "Training time is required.";
        public const string EM29 = "Final is required.";
        public const string EM30 = "Final Theory is required.";
        public const string EM31 = "Final Practice is required.";
        public const string EM32 = "GPA is required.";
        public const string EM33 = "Total of all assessment is not 100%. Please check again.";

        //Import Excel
        public const string EM34 = "File is required.";
        public const string EM63 = "Please upload a valid Excel file with the name 'Template_Import_Syllabus.xlsx'.";
        public const string EM99 = "Please upload a valid Excel file with the name 'import_trainningprogramm.xlsx'.";
        public const string EM64 = "Not Found Syllabus";
        public const string EM65 = "Error when update";
        public const string EM66 = "Name Of WorkSheet Is Not Correct!!!";
        public const string EM67 = "Not Found Learning Object";
        public const string EM68 = "Not Found Deliver Type";
        public const string EM69 = "Not Found Training Unit";

        //Update Syllabus
        public const string EM75 = "Can not update Syllabus Outline.";
        public const string EM76 = "Can not save Syllabus Outline.";

        //Create training program
        public const string EM35 = "Program name is required.";
        public const string EM36 = "General information is required.";
        public const string EM37 = "Syllabus is required.";
        public const string EM38 = "List of syllabuses is required.";

        //Import training program
        public const string EM39 = "File is required.";

        //Update training material
        public const string EM40 = "File is invalid. Please check and upload again.";

        //Create class
        public const string EM41 = "Class name is required.";
        public const string EM42 = "Class time is required.";
        public const string EM43 = "Location is required.";
        public const string EM44 = "Trainer is required.";
        public const string EM45 = "Admin is required.";
        public const string EM46 = "FSU is required.";
        public const string EM47 = "Time frame is required.";
        public const string EM48 = "Training program is required.";

        //Others
        public const string EM49 = "There's no record matching with your criteria";

        public const string EM50 = "There is not found the class: ";
        public const string EM51 = "There is not found the user: ";
        public const string EM52 = "There is not found the syllabus: ";
        public const string EM53 = "There is not found the training program: ";
        public const string EM55 = "There is not found the training unit: ";
        public const string EM56 = "There is not found the training content: ";
        public const string EM57 = "There is not found the learning object: ";
        public const string EM58 = "There is not found the delivery type: ";
        public const string EM59 = "There is not found the material: ";
        public const string EM77 = "There is not found the Assessment Scheme"; 

        public const string EM54 = "The Id/Code is invalid (must be greater than 0). Please check again!";

        public const string EM60 = "Training program with the provided code does not exist.";
        public const string EM61 = "DateAndTimeStudy must be less than Duration";
        public const string EM62 = "Class cannot be edited as it is not in 'Planning' or 'Scheduled' status.";
        public const string EM70 = "Error, DB will RollBack";

        //Duplication Syllabus
        public const string EM71 = "Can not duplicate objects.";

        //Deleted Syllabus 
        public const string EM74 = "The syllabus was assigned to a training program. You must delete training program related to this syllabus first";

        //Authen
        public const string EM78 = "Email/Password is not right.";
        public const string EM79 = "Token is invalid";
        public const string EM80 = "Token does not contain email claim";

        //Calender
        public const string EM82 = "ClassID is required!!!";
        public const string EM83 = "DateAndTimeStudy is required!!!";
        public const string EM84 = "ClassID not Found";

        //Class
        public const string EM85 = "Class created successfully!";
        public const string EM86 = "Can't deleted the class because this class is opening";

        //Deliver Type
        public const string EM87 = "Type is not exist!";

        //Learning Objective
        public const string EM88 = "Not Found any learning object based on search";

        //Training Content
        public const string EM89 = "Training format can't be empty!";
        public const string EM90 = "Duration can't be 0!";
        public const string EM91 = "Training Content name can't be empty!";
        public const string EM92 = "Total duration is either null or zero";
        public const string EM93 = "No training content found for syllabus ID: ";
        public const string EM94 = "There is no content that has ID";
        public const string EM95 = "There is no unit that has Code: ";

        //Training Program
        public const string EM96 = "Sequency must be consecutive!!!";
        public const string EM97 = "The registered class must be not started. ";
        public const string EM98 = "there is fail to create new program! ";
        public const string EM100 = "Can't not duplicate objects";
        public const string EM101 = "Error in adding general program.";
        public const string EM102 = "Error in adding program syllabuses: ";
        public const string EM103 = "Can't remove this Training Program";

        //There is still TrainingUnit, UserPermission and User

        //AssessmentScheme
        public const string EM104 = "Error in updating assessment scheme.";

        //Deliver Type
        public const string EM105 = "Deliver Type is exist";
        public const string EMS106 = "Error in updating delivery type.";

    }
}
