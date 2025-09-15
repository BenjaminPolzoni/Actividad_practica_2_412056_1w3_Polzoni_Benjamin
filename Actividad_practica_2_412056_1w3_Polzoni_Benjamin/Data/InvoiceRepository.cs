using Actividad_practica_1_412056_1w3_Polzoni_Benjamin.Data.Interfaces;
using Actividad_practica_1_412056_1w3_Polzoni_Benjamin.Data.Utiliti;
using Actividad_practica_1_412056_1w3_Polzoni_Benjamin.Domain;
using Actividad_practica_1_412056_1w3_Polzoni_Benjamin.Services;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_practica_1_412056_1w3_Polzoni_Benjamin.Data
{
    public class InvoiceRepository : IInvoiceReposiotry
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Invoice> GetAll()
        {
            SellerServices sellerServices = new SellerServices();
            ClientServices clientServices = new ClientServices();

            List<Invoice> lst = new List<Invoice>();

            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_FACTURA");
            foreach (DataRow row in dt.Rows)
            {
                Invoice I = new Invoice();
                I.Cod = Convert.ToInt32(row["nro_factura"]);
                I.date = Convert.ToDateTime(row["fecha"]);

                // Traer vendedor
                var codSeller = Convert.ToInt32(row["cod_vendedor"]);
                I.Seller = sellerServices.GetById(codSeller);

                // Traer Cliente
                var codVendedor = Convert.ToInt32(row["cod_cliente"]);
                I.Client = clientServices.GetById(codVendedor);

                var listDetail = GetDetailId(codVendedor);
                I.DetailInvoices = listDetail;
                lst.Add(I);
            }
            return lst;
        }

        public Invoice? GetById(int id)
        {
            SellerServices sellerServices = new SellerServices();
            ClientServices clientServices = new ClientServices();

            List<Parameter> param = new List<Parameter>();
            param.Add(new Parameter("@id", id));
            try
            {
                var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_FACTURA_POR_ID", param);

                Invoice invoice = new Invoice();
                invoice.Cod = Convert.ToInt32(dt.Rows[0]["nro_factura"]);
                invoice.date = Convert.ToDateTime(dt.Rows[0]["fecha"]);

                // Traer vendedor
                var codSeller = Convert.ToInt32(dt.Rows[0]["cod_vendedor"]);
                invoice.Seller = sellerServices.GetById(codSeller);

                // Traer Cliente
                var codVendedor = Convert.ToInt32(dt.Rows[0]["cod_cliente"]);
                invoice.Client = clientServices.GetById(codVendedor);

                var listDetail = GetDetailId(codVendedor);
                invoice.DetailInvoices = listDetail;

                return invoice;
            }
            catch (Exception)
            {

                return null ;
            }

        }

        public bool Save(Invoice invoice)
        {
            bool result = true;
            SqlConnection cnn = null;
            SqlTransaction t = null;

            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();
                t = cnn.BeginTransaction();

                var cmd = new SqlCommand("SP_INSERTAR_FACTURA", cnn, t);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cliente", invoice.Client.Cod);
                cmd.Parameters.AddWithValue("@vendedor", invoice.Seller.Cod);

                SqlParameter param = new SqlParameter("@id", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);

                cmd.ExecuteNonQuery();
                int InvoidId = (int)param.Value;

                // Recorro la lista de los detalles donde cargare uno a uno los detalles respectivamente
                foreach (var detail in invoice.DetailInvoices)
                {
                    var cmdDetail = new SqlCommand("SP_INSERTAR_DETALLE_FACTURA", cnn, t);
                    cmdDetail.CommandType = CommandType.StoredProcedure;

                    cmdDetail.Parameters.AddWithValue("@id_articulo", detail.Article.Cod);
                    cmdDetail.Parameters.AddWithValue("@id_factura", InvoidId);
                    cmdDetail.Parameters.AddWithValue("@cantidad", detail.Count);
                    cmdDetail.Parameters.AddWithValue("@precio", detail.UnitPrice);
                    cmdDetail.ExecuteNonQuery();
                }

                t.Commit();
            }
            catch (SqlException e)
            {
                if (t != null)
                {
                    t.Rollback();
                }
                result = false;
            }
            finally
            {
                // Si no es nula y esta abierta, la cerramos
                if (cnn != null && cnn.State == System.Data.ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return result;
        }
        private List<DetailInvoice> GetDetailId(int id)
        {
            ArticleServices articleServices = new ArticleServices();

            List<DetailInvoice> lst = new List<DetailInvoice>();

            List<Parameter> list = new List<Parameter>();
            list.Add(new Parameter("@ID", id));

            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_DETALLE_FACTURA", list);

            foreach (DataRow row in dt.Rows)
            {
                DetailInvoice Detail = new DetailInvoice();

                int idArticle = Convert.ToInt32(row["cod_articulo"]);
                Detail.Article = articleServices.GetById(idArticle);

                Detail.UnitPrice = Convert.ToInt32(row["pre_unitario"]);
                Detail.Count = Convert.ToInt32(row["cantidad"]);
                lst.Add(Detail);
            }
            return lst;
        }
    }
}
