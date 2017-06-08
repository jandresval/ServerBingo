using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;

namespace ServerBingo.Models
{
    public class BingoServerEntities : DbContext
    {
        public BingoServerEntities()
            : base("BingoEnties")
        { }

        public virtual DbSet<Bingobalota> Bingobalotas { get; set; }
        public virtual DbSet<Bingodia> Bingodias { get; set; }
        public virtual DbSet<Bingoganadore> Bingoganadores { get; set; }
        public virtual DbSet<Bingosoc> Bingosocs { get; set; }
        public virtual DbSet<Bingotbl> Bingotbl { get; set; }
        public virtual DbSet<Bingousuario> Bingousuarios { get; set; }
        public virtual DbSet<Bingoreye> Bingoreyes { get; set; }
        public virtual DbSet<Bingousutbl> Bingousutbl { get; set; }



    }
}