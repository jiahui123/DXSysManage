namespace DataBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("dxmanagesys.mail")]
    public partial class mail
    {
        [StringLength(100)]
        public string id { get; set; }

        [Required]
        [StringLength(50000)]
        public string subject { get; set; }

        [Required]
        [StringLength(100)]
        public string mailUID { get; set; }

        [Required]
        [StringLength(5000)]
        public string sendAddress { get; set; }


        [StringLength(500000)]
        public string MailBody { get; set; }

        [StringLength(50000)]
        public string Attachment { get; set; }

        public DateTime creatDate { get; set; }

        [Required]
        [StringLength(100)]
        public string creatUser { get; set; }

        [Required]
        public int dataStaus { get; set; }



    }
}
