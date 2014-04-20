using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerBingo.Models
{
    [Table(Name = "Bingousutbl")]
    public partial class Bingousutbl
    {
        public Bingousutbl()
        {
        }

        [Key]
        public int Idbingousutbl { get; set; }

        public int Idusuario { get; set; }

        public string Alias { get; set; }

        public string Tblnro { get; set; }

    }
}
