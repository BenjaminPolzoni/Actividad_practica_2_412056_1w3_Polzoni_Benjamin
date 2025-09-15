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
    public class ArticleRepository : IArticleRepository
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Article> GetAll()
        {
            List<Article> lst = new List<Article>();

            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_ARTICULOS");
            if (dt != null) 
            {
                foreach (DataRow row in dt.Rows)
                {
                    Article p = new Article();
                    p.Cod = (int)row["cod_articulo"];
                    p.Description = (string)row["descripcion"];
                    p.MinStock = row["stock_minimo"] == DBNull.Value ? 0 : Convert.ToInt32(row["stock_minimo"]); ;
                    p.Stock = Convert.ToInt32(row["stock"]);
                    p.UnitPrice = Convert.ToDouble(row["pre_unitario"]);
                    p.Observation = row["observaciones"] == DBNull.Value ? "" : (string)row["observaciones"];
                    lst.Add(p);
                }
                return lst;
            }
            else
            {
                return lst;
            }
        }

        public Article GetById(int id)
        {
            var list = new List<Parameter>();
            list.Add(new Parameter("@ID", id));

            DataTable dt = DataHelper.GetInstance().ExecuteSPQuery("RECUPERAR_ARTICULO_POR_ID", list);

            Article Article = new Article();

            if (dt != null || dt.Rows.Count > 0)
            {
                Article.Cod = Convert.ToInt32(dt.Rows[0]["cod_articulo"]);
                Article.Description = dt.Rows[0]["descripcion"].ToString();
                Article.MinStock = dt.Rows[0]["stock_minimo"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["stock_minimo"]) : 0;
                Article.Stock = Convert.ToInt32(dt.Rows[0]["stock"]);
                Article.UnitPrice = Convert.ToDouble(dt.Rows[0]["pre_unitario"]);
                Article.Observation = dt.Rows[0]["observaciones"] != DBNull.Value ? dt.Rows[0]["observaciones"].ToString() : "";
            }
            return Article;
        }

        public bool Save(Article article)
        {
            throw new NotImplementedException();
        }
    }
}
