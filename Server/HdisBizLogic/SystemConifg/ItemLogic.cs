using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HdisBizLogic.SystemConifg
{
    public class ItemLogic : ManagementBizlogic
    {


        /// <summary>
        /// 查询透析项目列表
        /// </summary>
        /// <returns></returns>
        public List<HdisModels.SystemConfig.ItemModel> QueryDialysisItemList()
        {
            List<HdisModels.SystemConfig.ItemModel> list = new List<HdisModels.SystemConfig.ItemModel>();
            HdisModels.SystemConfig.ItemModel model = null;
            string sql = string.Empty;
            sql = @"select * from hdis_com_item a where a.isuse='1' order by to_number(a.showseq)";
            try
            {
                if (this.ExecSQLQuery(sql) < 0)
                {
                    return list;
                }
                while (this.Reader.Read())
                {
                    model = new  HdisModels.SystemConfig.ItemModel();
                    model.ITEMID = Reader["ITEMID"].ToString();
                    model.ITEMNAME = Reader["ITEMNAME"].ToString();
                    model.CHANNEL = Reader["CHANNEL"].ToString();
                    model.ENGNAME = Reader["ENGNAME"].ToString();
                    model.UNIT = Reader["UNIT"].ToString();
                    model.DOTNUM = Reader["DOTNUM"].ToString();
                    model.SHOWSEQ = int.Parse(Reader["SHOWSEQ"].ToString());
                    list.Add(model);
                }
                return list;
            }
            catch
            {
                return list;
            }
            finally
            {
                this.oracleDb.CloseReader();
                this.oracleDb.CloseDB();
            }
        }

    }
}
