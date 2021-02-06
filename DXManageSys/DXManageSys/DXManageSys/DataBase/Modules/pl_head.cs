using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Modules
{
    public partial class pl_head
    {
        [StringLength(100)]
        [Required]
        public string id { get; set; }

        [StringLength(100)]
        [Required]
        public string invoice { get; set; }

        [StringLength(100)]
        [Required]
        public string order_no { get; set; }

        [StringLength(100)]
        [Required]
        public string date { get; set; }

        [StringLength(100)]
        [Required]
        public string cif { get; set; }
        

        [Required]
        public decimal total_qty { get; set; }


        [Required]
        public decimal total_cin { get; set; }


        [Required]
        public decimal total_nkgs { get; set; }


        [Required]
        public decimal total_gkgs { get; set; }

        [Required]
        public decimal total_cbm { get; set; }

        [StringLength(500)]
        [Required]
        public string english { get; set; }


        [InverseProperty("pl_head")]
        public List<pl_detail> pl_details { get; set; }

        [NotMapped]
        public string matched { get; set; }

        [NotMapped]
        public string path { get; set; }

        [NotMapped]
        public string port { get; set; }
    }


}
