//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServerBingo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Bingosoc
    {

        public string Socsucursal { get; set; }
        public string Soccarne { get; set; }
        public string Socnom { get; set; }
        public string Socdocid { get; set; }
        public string Socoficio { get; set; }
        public System.DateTime Socfching { get; set; }
        public System.DateTime Socfchnac { get; set; }
        public string Socfoto { get; set; }
        public bool Socactivo { get; set; }
        public string Socubic { get; set; }
        public string Soctel { get; set; }
        public string Socbarrio { get; set; }
        public decimal Socpuntos { get; set; }
        [Key]
        public int Idusuario { get; set; }
    
        public virtual Bingousuario Bingousuario { get; set; }
    }
}
