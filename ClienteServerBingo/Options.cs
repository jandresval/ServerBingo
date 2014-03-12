using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteServerBingo
{
    class Options
    {
        [Option('U', "User", Required = true, 
            HelpText = "Este es el nombre de usuario que se conectara al servidor.")]
        public string User { get; set; }

        [Option('A', "Anotacion", Required = false, HelpText = "Determina si la anotacion inicia (true) y finaliza (false).")]
        public string Anotacion { get; set; }

        [Option('J', "EstadoJuego", Required = false,
            HelpText = "Determina cuando el juego inicia (true) y cuando termina (false)")]
        public bool EstadoJuego { get; set; }

        [Option('S', "TablasSolicitadas", Required = false, DefaultValue= false,
            HelpText = "Retorna las tablas que han sido solicitadas por los jugadores (false y true).")]
        public bool TablasSolicitadas { get; set; }

        [Option('T', "TablasAnotadas", Required = false,
            HelpText = "Recibe las tablas que han sido anotadas para ser enviadas a los jugadores (nroTabla1,nroTabla2).")]
        public string TablasAnotadas { get; set; }

        [Option('B', "Balotas", Required = false,
            HelpText = "Recibe los numeros que estan siendo jugados en el bingo (##).")]
        public string Balotas { get; set; }

        [Option('N', "TablasNoAnotadas", Required = false,
            HelpText = "Recibe las tablas que no pudieron ser anotadas (nroTabla1, nroTabla2).")]
        public string TablasNoAnotadas { get; set; }

        [Option('G', "Ganadores", Required = false,
            HelpText = "Listado de ganadores (usuarioGanador,tablaGanadora,balota1:balota2;usuarioGanador2,tablaGanadora,balota1:balota2).")]
        public string Ganadores { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }

    }
}
