namespace ArmyTechTask
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StudentTeacher")]
    public partial class StudentTeacher
    {
        public int ID { get; set; }

        public int StudentId { get; set; }

        public int TeacherId { get; set; }

        public virtual Student Student { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
