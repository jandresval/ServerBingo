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
    
    public partial class Bingoreye
    {
        [Key]
        public int Bingoreyid { get; set; }
        public System.DateTime Jgofch { get; set; }
        public string Jgonro { get; set; }
        public string Jgocod { get; set; }
        public int Idusuario { get; set; }
        public string Tblnro { get; set; }
        public bool Tblactive { get; set; }
    
        public virtual Bingotbl Bingotbl { get; set; }
        public virtual Bingousuario Bingousuario { get; set; }
    }
}
