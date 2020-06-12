using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArmyTechTask.Models
{
    public class StudentViewModel
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StudentViewModel()
        {
            //StudentTeachers = new HashSet<StudentTeacher>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }

        public int GovernorateId { get; set; }

        public int? NeighborhoodId { get; set; }

        public int FieldId { get; set; }
       
        public int TeacherId { get; set; }
        //public virtual Teacher Teacher { get; set; }

        //public virtual Field Field { get; set; }

        //public virtual Governorate Governorate { get; set; }

        //public virtual Neighborhood Neighborhood { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StudentTeacher> StudentTeachers { get; set; }
    }
}