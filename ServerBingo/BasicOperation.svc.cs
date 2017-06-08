using Newtonsoft.Json;
using ServerBingo.Models;
using ServerBingoModel.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServerBingo
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BasicOperation" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select BasicOperation.svc or BasicOperation.svc.cs at the Solution Explorer and start debugging.
    public class BasicOperation : IBasicOperation
    {
        public int IniciarProcesoDescargaTablas(string name)
        {
            BingoServerEntities db = new BingoServerEntities();
            try
            {
                if (UserHandler.DataConnections.ContainsKey(name))
                {
                    UserHandler.DataConnections.Remove(name);
                }

                if (!db.Database.Exists())
                {
                    throw new Exception();
                }

                var bingoTblListNotUser = from n in db.Bingotbl
                                          from p in db.Bingousutbl.Where(pt => pt.Tblnro == n.Tblnro).DefaultIfEmpty()
                                          where n.Tblandroid == true &&
                                          n.Tblinhabil == false &&
                                          p.Tblnro == null
                                          select new BingotblView
                                          {
                                              Tblnro = n.Tblnro,
                                              Tblb1 = n.Tblb1,
                                              Tblb2 = n.Tblb2,
                                              Tblb3 = n.Tblb3,
                                              Tblb4 = n.Tblb4,
                                              Tblb5 = n.Tblb5,
                                              Tbli1 = n.Tbli1,
                                              Tbli2 = n.Tbli2,
                                              Tbli3 = n.Tbli3,
                                              Tbli4 = n.Tbli4,
                                              Tbli5 = n.Tbli5,
                                              Tbln1 = n.Tbln1,
                                              Tbln2 = n.Tbln2,
                                              Tbln3 = n.Tbln3,
                                              Tbln4 = n.Tbln4,
                                              Tbln5 = n.Tbln5,
                                              Tblg1 = n.Tblg1,
                                              Tblg2 = n.Tblg2,
                                              Tblg3 = n.Tblg3,
                                              Tblg4 = n.Tblg4,
                                              Tblg5 = n.Tblg5,
                                              Tblo1 = n.Tblo1,
                                              Tblo2 = n.Tblo2,
                                              Tblo3 = n.Tblo3,
                                              Tblo4 = n.Tblo4,
                                              Tblo5 = n.Tblo5,
                                              Tblandroid = n.Tblandroid,
                                              Alias = string.Empty
                                          };

                var bingoTblList = from n in db.Bingotbl
                                   join
                                       p in db.Bingousutbl on n.Tblnro equals p.Tblnro
                                   where n.Tblandroid == true &&
                                         n.Tblinhabil == false &&
                                         p.Alias == name
                                   select new BingotblView
                                   {
                                       Tblnro = n.Tblnro,
                                       Tblb1 = n.Tblb1,
                                       Tblb2 = n.Tblb2,
                                       Tblb3 = n.Tblb3,
                                       Tblb4 = n.Tblb4,
                                       Tblb5 = n.Tblb5,
                                       Tbli1 = n.Tbli1,
                                       Tbli2 = n.Tbli2,
                                       Tbli3 = n.Tbli3,
                                       Tbli4 = n.Tbli4,
                                       Tbli5 = n.Tbli5,
                                       Tbln1 = n.Tbln1,
                                       Tbln2 = n.Tbln2,
                                       Tbln3 = n.Tbln3,
                                       Tbln4 = n.Tbln4,
                                       Tbln5 = n.Tbln5,
                                       Tblg1 = n.Tblg1,
                                       Tblg2 = n.Tblg2,
                                       Tblg3 = n.Tblg3,
                                       Tblg4 = n.Tblg4,
                                       Tblg5 = n.Tblg5,
                                       Tblo1 = n.Tblo1,
                                       Tblo2 = n.Tblo2,
                                       Tblo3 = n.Tblo3,
                                       Tblo4 = n.Tblo4,
                                       Tblo5 = n.Tblo5,
                                       Tblandroid = n.Tblandroid,

                                   };


                List<BingotblView> listBingotblView = bingoTblListNotUser.ToList<BingotblView>();
                listBingotblView.AddRange(bingoTblList.ToList<BingotblView>());

                UserHandler.DataConnections.Add(name, listBingotblView);

                return listBingotblView.Count();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string RetornarTablas(string name, int inicial, int final)
        {
            try
            {
                if (UserHandler.DataConnections.ContainsKey(name))
                {
                    string retorno = "";
                    List<BingotblView> listBingotblViewCompleto;
                    if (UserHandler.DataConnections.TryGetValue(name, out listBingotblViewCompleto))
                    {
                        List<BingotblView> listBingotblView = listBingotblViewCompleto.GetRange(inicial, final);
                        retorno = JsonConvert.SerializeObject(listBingotblView);
                    }
                    return retorno;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int IniciarProcesoDescargaxUsuario(string name)
        {
            BingoServerEntities db = new BingoServerEntities();
            try
            {
                if (UserHandler.DataConnections.ContainsKey(name))
                {
                    UserHandler.DataConnections.Remove(name);
                }
                if (!db.Database.Exists())
                {
                    throw new Exception();
                }

                var bingoTblList = from n in db.Bingotbl
                                   join
                                       p in db.Bingousutbl on n.Tblnro equals p.Tblnro
                                   where n.Tblandroid == true &&
                                         n.Tblinhabil == false &&
                                         p.Alias == name
                                   select new BingotblView
                                   {
                                       Tblnro = n.Tblnro,
                                       Tblb1 = n.Tblb1,
                                       Tblb2 = n.Tblb2,
                                       Tblb3 = n.Tblb3,
                                       Tblb4 = n.Tblb4,
                                       Tblb5 = n.Tblb5,
                                       Tbli1 = n.Tbli1,
                                       Tbli2 = n.Tbli2,
                                       Tbli3 = n.Tbli3,
                                       Tbli4 = n.Tbli4,
                                       Tbli5 = n.Tbli5,
                                       Tbln1 = n.Tbln1,
                                       Tbln2 = n.Tbln2,
                                       Tbln3 = n.Tbln3,
                                       Tbln4 = n.Tbln4,
                                       Tbln5 = n.Tbln5,
                                       Tblg1 = n.Tblg1,
                                       Tblg2 = n.Tblg2,
                                       Tblg3 = n.Tblg3,
                                       Tblg4 = n.Tblg4,
                                       Tblg5 = n.Tblg5,
                                       Tblo1 = n.Tblo1,
                                       Tblo2 = n.Tblo2,
                                       Tblo3 = n.Tblo3,
                                       Tblo4 = n.Tblo4,
                                       Tblo5 = n.Tblo5,
                                       Tblandroid = n.Tblandroid,

                                   };

                List<BingotblView> listBingotblView = bingoTblList.ToList<BingotblView>();

                UserHandler.DataConnections.Add(name, listBingotblView);

                return listBingotblView.Count();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
