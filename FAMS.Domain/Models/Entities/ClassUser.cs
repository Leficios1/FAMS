using System.ComponentModel.DataAnnotations;

namespace FAMS.Domain.Models.Entities
{
    public class ClassUser
    {

        public int UserId { get; set; }

        public int ClassId { get; set; }

        /// <summary>
        /// Unknown the purpose for this property
        /// </summary>
        public int UserType { get; set; }

        public User? User { get; set; }

        public Class? Class { get; set; }
    }
}
