using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RLogicServiceMvc.Service.DataAccess;

namespace RLogicServiceMvc.Service.External
{
    public class SerachServices
    {
        public DataSet getVehicle(string con, string SearchFor)
        {
            SqlDataAceessLayer _dal = new SqlDataAceessLayer(con);
            string Query = "Select Top 20 Vehicle_ID,Vehicle_No From Fleet.Master_Vehicle where Is_Active=1 AND " +
                           "Vehicle_Category_ID in(1,2,3,4) and Vehicle_No like '%" + SearchFor + "%' order by Vehicle_No";

            DataSet Ds = _dal.ExecuteSql(Query);

            return Ds;
        }

        public DataSet getLocation(string con, string SearchFor)
        {
            SqlDataAceessLayer _dal = new SqlDataAceessLayer(con);
            string Query = "Select Top 20 Service_Network_ID, Service_Network_Name FROM Common.Master_Service_Network WHERE Is_Active=1 AND " +
                           "Service_Network_Name like '%" + SearchFor + "%' order by Service_Network_Name";

            DataSet Ds = _dal.ExecuteSql(Query);

            return Ds;
        }

        public DataSet getClient(string con, string SearchFor)
        {
            SqlDataAceessLayer _dal = new SqlDataAceessLayer(con);
            string Query = "SELECT Top 20 Client_ID, Client_Name FROM Common.Master_Client WHERE Client_Type=2 and Is_Active=1 AND " +
                           "Client_Name like '%" + SearchFor + "%' order by Client_Name";

            DataSet Ds = _dal.ExecuteSql(Query);

            return Ds;
        }

        public DataSet getConsignorConsignee(string con, string SearchFor)
        {
            SqlDataAceessLayer _dal = new SqlDataAceessLayer(con);
            string Query = "SELECT Top 20 Client_ID, Client_Name FROM Common.Master_Client WHERE Is_Active=1 AND " +
                           "Client_Name like '%" + SearchFor + "%' order by Client_Name";

            DataSet Ds = _dal.ExecuteSql(Query);

            return Ds;
        }
    }
}