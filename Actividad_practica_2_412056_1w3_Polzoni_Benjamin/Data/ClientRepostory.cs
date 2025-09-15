using Actividad_practica_1_412056_1w3_Polzoni_Benjamin.Data.Interfaces;
using Actividad_practica_1_412056_1w3_Polzoni_Benjamin.Data.Utiliti;
using Actividad_practica_1_412056_1w3_Polzoni_Benjamin.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_practica_1_412056_1w3_Polzoni_Benjamin.Data
{
    public class ClientRepostory : IClientRepository
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Client> GetAll()
        {
            List<Client> lst = new List<Client>();

            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_CLIENTES");

            foreach (DataRow row in dt.Rows)
            {
                Client c = new Client();

                c.Cod = (int)row["cod_cliente"];
                c.Name = (string)row["nom_cliente"];
                c.LastName = (string)row["ape_cliente"];
                c.street = (string)row["calle"];
                c.Number = Convert.ToInt32(row["altura"]);
                c.Neighborhood = (int)row["cod_barrio"];
                c.Tellphone = row["nro_tel"] == DBNull.Value ? 0 : Convert.ToInt32(row["nro_tel"]);
                c.Email = row["e-mail"] == DBNull.Value ? "" : (string)row["e-mail"];

                lst.Add(c);
            }
            return lst;
        }

        public Client GetById(int id)
        {
            var list = new List<Parameter>();
            list.Add(new Parameter("@ID", id));

            DataTable dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_CLIENTE_POR_ID", list);

            Client client = new Client();

            if (dt != null || dt.Rows.Count > 0)
            {
                client.Cod = Convert.ToInt32(dt.Rows[0]["cod_cliente"]);
                client.Name = dt.Rows[0]["nom_cliente"].ToString();
                client.LastName = dt.Rows[0]["ape_cliente"].ToString();
                client.street = dt.Rows[0]["calle"].ToString();
                client.Number = Convert.ToInt32(dt.Rows[0]["altura"]);
                client.Neighborhood = Convert.ToInt32(dt.Rows[0]["cod_barrio"]);
                client.Tellphone = dt.Rows[0]["nro_tel"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["nro_tel"]) : 0;
                client.Email = dt.Rows[0]["e-mail"] != DBNull.Value ? dt.Rows[0]["e-mail"].ToString() : "";
            }
            return client;
        }

        public bool Save(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
