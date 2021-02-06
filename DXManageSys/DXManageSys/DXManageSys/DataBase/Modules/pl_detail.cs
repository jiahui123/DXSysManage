using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Modules
{
    public partial class pl_detail
    {
        [StringLength(100)]
        [Required]
        public string id { get; set; }

        [Required]
        public string pl_head_id { get; set; }

        [ForeignKey("pl_head_id")]
        [Required]
        public pl_head pl_head { get; set; }

        [StringLength(800)]
        [Required]
        public string goods { get; set; }

        [Required]
        public int qty { get; set; }

        [Required]
        public decimal cin { get; set; }

        [Required]
        public decimal nkgs { get; set; }

        [Required]
        public decimal gkgs { get; set; }

        [Required]
        public decimal cbm { get; set; }
    }
}
