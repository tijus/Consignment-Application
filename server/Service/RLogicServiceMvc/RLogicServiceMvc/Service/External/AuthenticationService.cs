using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RLogicServiceMvc.Models;
using RLogicServiceMvc.Service.Internal;
using RLogicServiceMvc.Service.DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace RLogicServiceMvc.Service.External
{
    public class AuthenticationService
    {
        public MessageModel ValidateUser(string con, string UserName, string Password)
        {
            SqlDataAceessLayer _dal = new SqlDataAceessLayer(con);
            int YearCode = CompanyConfigService.getYearCode(DateTime.Now);
            DataSet _ds = null;
            SqlParameter[] param ={
                                    _dal.MakeOutParams("@Error_Code", SqlDbType.Int, 0),
			                        _dal.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                    _dal.MakeInParams("@YearCode",SqlDbType.Int,0,YearCode),
                                    _dal.MakeInParams("@UserName",SqlDbType.VarChar,25,UserName),
                                    _dal.MakeInParams("@Password",SqlDbType.VarChar,25,Password),
                                    _dal.MakeInParams("@DivisionID",SqlDbType.Int, 0,1)                              
                              };

            _dal.ExecuteStoredProcedure("[System].[spLogin_Check]", param, ref _ds);
            MessageModel objMessage = new MessageModel();
            objMessage.MessageId = Convert.ToInt32(param[0].Value);
            objMessage.Message = Convert.ToString(param[1].Value);
            return objMessage;
        }
    }
}