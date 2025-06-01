using FAMS.Domain.Models.Entities;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FAMS.Domain.Models.Dtos.Request
{
    public class CreateClassDto
    {
        [Required(ErrorMessage = "TrainingProgram is required.")]
        public int TrainingProgramCode { get; set; }
        [Required(ErrorMessage = "Class name is required.")]
        public string ClassName { get; set; }
        [Required(ErrorMessage = "Class Code is required.")]
        public string ClassCode { get; set; }
        public int? Duration { get; set; }
        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; }
        [Required(ErrorMessage = "Location is required.")]
        public string? Location { get; set; }
        [Required(ErrorMessage = "Attendee is required.")]
        public string? Attendee { get; set; }
        [Required(ErrorMessage = "FSU is required.")]
        public string? FSU { get; set; }
        [Required(ErrorMessage = "Class time is required.")]
        public DateTimeOffset StartDate { get; set; }
        [Required(ErrorMessage = "Class time is required.")]
        public DateTimeOffset EndDate { get; set; }
        [Required(ErrorMessage = "Class time is required.")]
        public DateTime? ClassTimeStart { get; set; }
        [Required(ErrorMessage = "Class time is required.")]
        public DateTime? ClassTimeEnd { get; set; }
        [Required(ErrorMessage = "Create By is required.")]
        public string? CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public TrainerUnit[]? Trainers { get; set; }
        public int[]? AdminId { get; set; }
        public DateTime[]? Calendar { get; set; }
    }
    public class TrainerUnit
    {
        public int TrainerId { get; set; }
        public int UnitCode { get; set; }
        public string? Location { get; set; }
    }

}
