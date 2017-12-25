using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HdisModels.Person
{
    /// <summary>
    /// [功能描述:登录人员实体]
    /// [创建者:yinbsh]
    /// [创建时间:2017-06-20]
    /// </summary>
   public class PersonModel : Object
    {
        /// <summary>
        ///人员ID
        /// </summary>
        public string PERSONID
        {
            get;
            set;
        }

        /// <summary>
        ///人员姓名
        /// </summary>
        public string PERSONNAME
        {
            get;
            set;
        }

        /// <summary>
        ///帐户名称
        /// </summary>
        public string LOGINNAME
        {
            get;
            set;
        }

        /// <summary>
        ///帐户密码
        /// </summary>
        public string LOGINPASS
        {
            get;
            set;
        }

        /// <summary>
        ///所属院区
        /// </summary>
        public string HOSPITALID
        {
            get;
            set;
        }

        /// <summary>
        ///院区名称
        /// </summary>
        public string HOSPITALNAME
        {
            get;
            set;
        }

        /// <summary>
        ///所属科室
        /// </summary>
        public string DEPTID
        {
            get;
            set;
        }

        /// <summary>
        ///科室名称
        /// </summary>
        public string DEPTNAME
        {
            get;
            set;
        }

        /// <summary>
        ///角色编码
        /// </summary>
        public string ROLEID
        {
            get;
            set;
        }

        /// <summary>
        ///角色标识(超级，主任，医生，护士长，护士等)
        /// </summary>
        public string ROLENAME
        {
            get;
            set;
        }

        /// <summary>
        ///可登陆的科室
        /// </summary>
        public string DEPTLOGIN
        {
            get;
            set;
        }

        /// <summary>
        ///(1)来自HIS等外部系统;(0)自行维护人员
        /// </summary>
        public string ISSOURCEHIS
        {
            get;
            set;
        }

        /// <summary>
        ///有效:1是0否
        /// </summary>
        public bool ISENABLED
        {
            get;
            set;
        }
       
    }
}
