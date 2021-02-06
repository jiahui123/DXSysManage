namespace DataBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("dxmanagesys.user")]
    public partial class user
    {
        [StringLength(100)]
        public string id { get; set; }

        [Required]
        [StringLength(100)]
        public string userId { get; set; }

        [Required]
        [StringLength(100)]
        public string userPassword { get; set; }

        [Required]
        [StringLength(100)]
        public string auth { get; set; }

        public DateTime creatDate { get; set; }

        [Required]
        [StringLength(100)]
        public string creatUser { get; set; }

        public int dataStaus { get; set; }



    }
}
