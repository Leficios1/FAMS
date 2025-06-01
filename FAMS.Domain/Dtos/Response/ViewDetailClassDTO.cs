using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Response
{
    public class ViewDetailClassDTO
    {
        public int Id { get; set; } 
        public string ClassCode { get; set; } = string.Empty;

        public string ClassName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string ClassTime { get; set; } = string.Empty;
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public string Attendee { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int DurationByDay { get; set; } 
        public float DurationByHour { get; set; } 
        public string FSU {  get; set; }
        public string EmailFSU {get; set; } 
        public string CreatedBy { get; set; } = string.Empty;

        public string CreatedDate { get; set; } = string.Empty;

        public string ModifiedBy {  get; set; } = string.Empty;
        public string ModifiedDate {  get; set; } = string.Empty;
        public string[] CalendarStudy { get; set; } = new string[] { };
       

        public InfoTrainer[] InfoTrainers { get; set; } =new InfoTrainer[] {};

        public InfoUser[] InfoAdmins { get; set; } = new InfoUser[] { };

        

        public TrainingProgramCardInClass TrainingProgram { get; set; } = new TrainingProgramCardInClass();

    }
    public class InfoFSU
    {
        public string FSU=string.Empty;
        public string Email=string.Empty;
    }
    public class InfoTrainer
    {   

        public int SyllabusId { get; set; }
        public int TrainerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location {  set; get; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int UnitCode { get; set; } = 0;
    }
    public class InfoUser
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;

        public string AvatarUrl { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

    }

    public class TrainingProgramCardInClass
    {
        public int TrainingProgramCode { get; set; } 

        public string TrainingProgramName { get; set; } = string.Empty;
            
        public string ModifiedDate { get; set; } = string.Empty;

        public string ModifiedBy { get; set; } = string.Empty;

        public int DurationByDay { get; set; } 

        public float DurationByHour { get; set; } 

        public SyllabusCard[] Syllabuses {  get; set; } = new SyllabusCard[] { };
    }
}

