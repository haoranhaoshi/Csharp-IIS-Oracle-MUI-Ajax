using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HdisBizLogic.SystemConifg
{
   public class AreaLogic : ManagementBizlogic
    {

        /// <summary>
        /// 根据院区编码和科室编码区域信息
        /// 添加全部节点
        /// </summary>
        /// <param name="strHosptalId">院区编码</param>
        /// <param name="strDeptId">科室</param>
        /// <returns></returns>
        public List<HdisModels.SystemConfig.AreaModel> QueryAreaList(string strHosptalId, string strDeptId)
        {
            List<HdisModels.SystemConfig.AreaModel> infoList = new List<HdisModels.SystemConfig.AreaModel>();
            string sql = string.Empty;
            try
            {
                sql = @"select * from hdis_com_area a where a.hospitalid = '{0}'  and a.deptid = '{1}' and a.isavailable='1'  order by a.hospitalid, a.deptid, a.areaid";
                sql = string.Format(sql, strHosptalId, strDeptId);
                if (this.ExecSQLQuery(sql) < 0)
                {
                    return null;
                }
                HdisModels.SystemConfig.AreaModel obj;
                obj = new HdisModels.SystemConfig.AreaModel();
                obj.AreaID = "ALL";
                obj.AreaName = "全部";
                infoList.Add(obj);
                while (this.Reader.Read())
                {
                    obj = new HdisModels.SystemConfig.AreaModel();
                    obj.DEPTID = Reader["DEPTID"].ToString();
                    obj.DEPTNAME = Reader["DEPTNAME"].ToString();
                    obj.HOSPITALID = Reader["HOSPITALID"].ToString();
                    obj.HOSPITALNAME = Reader["HOSPITALNAME"].ToString();
                    obj.AreaID = Reader["AreaID"].ToString();
                    obj.AreaName = Reader["AreaName"].ToString();
                    obj.LANDSCAPECOUNT = Convert.ToInt32(Reader["LANDSCAPECOUNT"].ToString());
                    obj.AREATYPE = Reader["AREATYPE"].ToString();
                    infoList.Add(obj);
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                this.oracleDb.CloseReader();
                this.oracleDb.CloseDB();
            }
            return infoList;
        }
    }
}
