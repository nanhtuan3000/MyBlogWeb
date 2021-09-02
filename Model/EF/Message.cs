namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Message")]
    public partial class Message
    {
        public long ID { get; set; }

        [StringLength(250)]
        public string UserName { get; set; }

        [Column(TypeName = "ntext")]
        public string Detail { get; set; }
                
        public DateTime? CreateDate { get; set; }

        [Column(TypeName = "ntext")]
        public string Answer { get; set; }

        public DateTime? AnswerDate { get; set; }

        [StringLength(50)]
        public string AnswerBy { get; set; }

        [StringLength(250)]
        public string MetaKeywords { get; set; }

        [StringLength(250)]
        public string MetaDescriptions { get; set; }

        public bool Status { get; set; }
    }
}
