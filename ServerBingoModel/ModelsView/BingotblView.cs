using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerBingoModel.ModelsView
{
    public class BingotblView
    {
        [Key]
        public string Tblnro { get; set; }
        public string Tblb1 { get; set; }
        public string Tblb2 { get; set; }
        public string Tblb3 { get; set; }
        public string Tblb4 { get; set; }
        public string Tblb5 { get; set; }
        public string Tbli1 { get; set; }
        public string Tbli2 { get; set; }
        public string Tbli3 { get; set; }
        public string Tbli4 { get; set; }
        public string Tbli5 { get; set; }
        public string Tbln1 { get; set; }
        public string Tbln2 { get; set; }
        public string Tbln3 { get; set; }
        public string Tbln4 { get; set; }
        public string Tbln5 { get; set; }
        public string Tblg1 { get; set; }
        public string Tblg2 { get; set; }
        public string Tblg3 { get; set; }
        public string Tblg4 { get; set; }
        public string Tblg5 { get; set; }
        public string Tblo1 { get; set; }
        public string Tblo2 { get; set; }
        public string Tblo3 { get; set; }
        public string Tblo4 { get; set; }
        public string Tblo5 { get; set; }
        public bool Tblandroid { get; set; }
        public string Alias { get; set; }
    }
}
