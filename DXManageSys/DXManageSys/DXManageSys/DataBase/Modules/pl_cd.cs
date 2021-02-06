using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Modules
{
    public class pl_cd
    {
        [StringLength(100)]
        [Required]
        public string id { get; set; }

        [Required]
        public string bgNumber { get; set; }

        [Required]
        public decimal jianshu { get; set; }

        [Required]
        public decimal maozhong { get; set; }

        [Required]
        public decimal tiji { get; set; }

        [Required]
        [StringLength(100)]
        public string fileName { get; set; }
    }
}
