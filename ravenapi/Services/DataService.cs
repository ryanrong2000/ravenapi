using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using ravenapi.Entities;

namespace ravenapi.Services
{
    
    public static class DataService
    {
        public static string constr = "Server=35.223.100.164;Database=;Uid=root;Pwd=w6FtFC819JH8jd77";
        public static User UserLogin(string username, string password)
        {
            User user = new User();
            MySqlConnection conn = new MySqlConnection(constr);
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from `goraven`.`cust` where `UserName` = '" + username + "' and `Password` = '" + password + "' and `Status`=1 limit 1;";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                user.Id = Convert.ToInt32(reader.GetString("CustId"));
                user.Username = reader.GetString("UserName");
            }
            reader.Close();
            reader.Dispose();
            conn.Close();
            conn.Dispose();

            return user;
        }

        public static BurnAddress GetBurnAddress(int custId, string burnAddress)
        {
            BurnAddress address = new BurnAddress();
            MySqlConnection conn = new MySqlConnection(constr);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                //cmd.CommandText = "select * from `goraven`.`burn_address` where `CustId` = " + custId.ToString() + " and `BurnAddress` = '" + burnAddress + "' limit 1;";
                cmd.CommandText = "select * from `goraven`.`burn_address` where `BurnAddress` = '" + burnAddress + "' and (`CustId` =  " + custId.ToString() + " or `CustId` in (select `parentCustId` from `goraven`.`cust` where `CustId`=  " + custId.ToString() + ")) limit 1;";
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    address.burnAddress = reader.GetString("BurnAddress");
                    address.custId = Convert.ToInt32(reader.GetString("CustId"));
                    if (!reader.IsDBNull(3))
                    {
                        address.burnAddressRef = reader.GetString("BurnAddressRef");
                    }
                    if (!reader.IsDBNull(4))
                    {
                        address.burnAddressTag = reader.GetString("Tag");
                    }
                    if (!reader.IsDBNull(5))
                    {
                        address.updateDateTime = Convert.ToDateTime(reader.GetString("UpdateDateTime"));
                    }
                    if (!reader.IsDBNull(6))
                    {
                        address.createDateTime = Convert.ToDateTime(reader.GetString("CreateDateTime"));
                    }
                }
                reader.Close();
                reader.Dispose();
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return address;
        }

        public static BurnAddress GetBurnAddressByRef(int custId, string burnAddressRef)
        {
            BurnAddress address = new BurnAddress();
            MySqlConnection conn = new MySqlConnection(constr);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from `goraven`.`burn_address` where `BurnAddressRef` = '" + burnAddressRef + "' and (`CustId` =  " + custId.ToString() + " or `CustId` in (select `parentCustId` from `goraven`.`cust` where `CustId`=  " + custId.ToString() + ")) limit 1;";
                //cmd.CommandText = "select * from `goraven`.`burn_address` where `CustId` = " + custId.ToString() + " and `BurnAddressRef` = '" + burnAddressRef + "' limit 1;";
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    address.burnAddress = reader.GetString("BurnAddress");
                    address.custId = Convert.ToInt32(reader.GetString("CustId"));
                    if (!reader.IsDBNull(3))
                    {
                        address.burnAddressRef = reader.GetString("BurnAddressRef");
                    }
                    if (!reader.IsDBNull(4))
                    {
                        address.burnAddressTag = reader.GetString("Tag");
                    }
                    if (!reader.IsDBNull(5))
                    {
                        address.updateDateTime = Convert.ToDateTime(reader.GetString("UpdateDateTime"));
                    }
                    if (!reader.IsDBNull(6))
                    {
                        address.createDateTime = Convert.ToDateTime(reader.GetString("CreateDateTime"));
                    }
                }
                reader.Close();
                reader.Dispose();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return address;
        }

        public static List<BurnAddress> GetBurnAddressList(int custId)
        {
            List<BurnAddress> burnAddressList = new List<BurnAddress>();
            MySqlConnection conn = new MySqlConnection(constr);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from `goraven`.`burn_address` where `CustId` =  " + custId.ToString() + " or `CustId` in (select `parentCustId` from `goraven`.`cust` where `CustId`=  " + custId.ToString() + ");";
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    BurnAddress address = new BurnAddress();
                    address.burnAddress = reader.GetString("BurnAddress");
                    address.custId = Convert.ToInt32(reader.GetString("CustId"));
                    if (!reader.IsDBNull(3))
                    {
                        address.burnAddressRef = reader.GetString("BurnAddressRef");
                    }
                    if (!reader.IsDBNull(4))
                    {
                        address.burnAddressTag = reader.GetString("Tag");
                    }
                    if (!reader.IsDBNull(5))
                    {
                        address.updateDateTime = Convert.ToDateTime(reader.GetString("UpdateDateTime"));
                    }
                    if (!reader.IsDBNull(6))
                    {
                        address.createDateTime = Convert.ToDateTime(reader.GetString("CreateDateTime"));
                    }
                    burnAddressList.Add(address);

                }
                reader.Close();
                reader.Dispose();
            }
            catch (Exception ex)
            {
                //burnAddress = ex.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return burnAddressList;
        }

        public static Asset GetAsset(int custId, string strAsset)
        {
            Asset asset = new Asset();
            MySqlConnection conn = new MySqlConnection(constr);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                //cmd.CommandText = "select * from `goraven`.`asset` where `CustId` = " + custId.ToString() + " and `asset` = '" + strAsset + "' limit 1;";
                cmd.CommandText = "select * from `goraven`.`asset` where `Asset`='" + strAsset + "' and (`CustId` = " + custId.ToString() + " or `CustId`in (select `CustId` from `goraven`.`cust` where `parentCustId` in (select coalesce(`ParentCustId`,`CustId`) from `goraven`.`cust` where `CustId` =" + custId.ToString() + ")) or `CustId` in (select `ParentCustId` from `goraven`.`cust` where `CustId` =" + custId.ToString() + "));";

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    asset.asset = reader.GetString("asset");
                    asset.custId = Convert.ToInt32(reader.GetString("CustId"));
                    if (!reader.IsDBNull(2))
                    {
                         asset.assetRef = reader.GetString("AssetRef");
                    }
                    if (!reader.IsDBNull(3))
                    {
                        asset.tag = reader.GetString("Tag");
                    }
                    if (!reader.IsDBNull(4))
                    {
                        asset.createDateTime = Convert.ToDateTime(reader.GetString("CreateDateTime"));
                    }
                    if (!reader.IsDBNull(5))
                    {
                        asset.status = Convert.ToInt32(reader.GetString("Status"));
                    }
                }
                reader.Close();
                reader.Dispose();
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return asset;
        }

        public static Asset GetAssetByRef(int custId, string strAssetRef)
        {
            Asset asset = new Asset();
            MySqlConnection conn = new MySqlConnection(constr);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                //cmd.CommandText = "select * from `goraven`.`asset` where `CustId` = " + custId.ToString() + " and `AssetRef` = '" + strAssetRef + "' limit 1;";
                cmd.CommandText = "select * from `goraven`.`asset` where `AssetRef`='" + strAssetRef + "' and (`CustId` = " + custId.ToString() + " or `CustId`in (select `CustId` from `goraven`.`cust` where `parentCustId` in (select coalesce(`ParentCustId`,`CustId`) from `goraven`.`cust` where `CustId` =" + custId.ToString() + ")) or `CustId` in (select `ParentCustId` from `goraven`.`cust` where `CustId` =" + custId.ToString() + "));";
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    asset.asset = reader.GetString("asset");
                    asset.custId = Convert.ToInt32(reader.GetString("CustId"));
                    if (!reader.IsDBNull(2))
                    {
                        asset.assetRef = reader.GetString("AssetRef");
                    }
                    if (!reader.IsDBNull(3))
                    {
                        asset.tag = reader.GetString("Tag");
                    }
                    asset.createDateTime = Convert.ToDateTime(reader.GetString("CreateDateTime"));
                    asset.status = Convert.ToInt32(reader.GetString("Status"));

                }
                reader.Close();
                reader.Dispose();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return asset;
        }

        public static List<Asset> GetAssetList(int custId)
        {
            List<Asset> assetList = new List<Asset>();
            MySqlConnection conn = new MySqlConnection(constr);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from `goraven`.`asset` where `CustId` = " + custId.ToString() + " or `CustId`in (select `CustId` from `goraven`.`cust` where `parentCustId` in (select coalesce(`ParentCustId`,`CustId`) from `goraven`.`cust` where `CustId` =" + custId.ToString() + ")) or `CustId` in (select `ParentCustId` from `goraven`.`cust` where `CustId` =" + custId.ToString() + ");";
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Asset asset = new Asset();
                    asset.asset = reader.GetString("asset");
                    asset.custId = Convert.ToInt32(reader.GetString("CustId"));
                    if (!reader.IsDBNull(2))
                    {
                        asset.assetRef = reader.GetString("AssetRef");
                    }
                    if (!reader.IsDBNull(3))
                    {
                        asset.tag = reader.GetString("Tag");
                    }
                    asset.createDateTime = Convert.ToDateTime(reader.GetString("CreateDateTime"));
                    asset.status = Convert.ToInt32(reader.GetString("Status"));
                    assetList.Add(asset);
                }
                reader.Close();
                reader.Dispose();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return assetList;
        }

        public static bool CheckIsParentCust(int custId)
        {
            //true is correct
            MySqlConnection conn = new MySqlConnection(constr);
            long exist;
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select count(*) from `goraven`.`cust` where `custId` = " + custId.ToString() + " and `ParentCustId` is null;";
                exist = (long)cmd.ExecuteScalar();

                if (exist == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            /*
            catch (Exception ex)
            {
                //return true;
            }
            */
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public static bool CheckBurnAddress(int custId, string burnAddressRef)
        {
            //false is correct
            MySqlConnection conn = new MySqlConnection(constr);
            long exist;
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select count(*) from `goraven`.`burn_address` where `CustId` = " + custId.ToString() + " and `BurnAddressRef` = '" + burnAddressRef + "';";
                exist = (long)cmd.ExecuteScalar();

                if (exist == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            /*
            catch (Exception ex)
            {
                //return true;
            }
            */
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public static BurnAddress GetBurnAddressNew(int custId, string burnAddressRef, string tag)
        {
            BurnAddress address = new BurnAddress();
            MySqlConnection conn = new MySqlConnection(constr);
            try
            {
               conn.Open();
               MySqlCommand cmd = conn.CreateCommand();
               cmd.CommandText = "update `goraven`.`burn_address` na inner join(select min(`BurnAddressId`) as BurnAddressId from `goraven`.`burn_address` where `CustId` is null) mn on na.`BurnAddressId` = mn.`BurnAddressId` set na.`CustId`= " + custId.ToString() + ", na.`BurnAddressRef`= '" + burnAddressRef + "', na.`Tag`= '" + tag + "', na.`UpdateDateTime` = now()";
               cmd.ExecuteNonQuery();

               MySqlCommand cmd2 = conn.CreateCommand();
               cmd2.CommandText = "select * from `goraven`.`burn_address` where `CustId` = " + custId.ToString() + " order by BurnAddressId desc limit 1;";
               MySqlDataReader reader = cmd2.ExecuteReader();

               while (reader.Read())
               {
                   address.burnAddress = reader.GetString("BurnAddress");
                   address.custId = Convert.ToInt32(reader.GetString("CustId"));
                   if (!reader.IsDBNull(3))
                   {
                        address.burnAddressRef = reader.GetString("BurnAddressRef");
                   }
                   if (!reader.IsDBNull(4))
                   {
                        address.burnAddressTag = reader.GetString("Tag");
                   }
                   if (!reader.IsDBNull(5))
                   {
                        address.updateDateTime = Convert.ToDateTime(reader.GetString("UpdateDateTime"));
                   }
                   if (!reader.IsDBNull(6))
                   {
                        address.createDateTime = Convert.ToDateTime(reader.GetString("CreateDateTime"));
                   }
                }
                reader.Close();
                reader.Dispose();
                
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return address;

        }

        public static String SetProcess(int custId, string address, string asset)
        {
            Guid id = Guid.NewGuid();
            MySqlConnection conn = new MySqlConnection(constr);
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "insert into `goraven`.`process`(`ProcessId`,`CustId`,`BurnAddress`,`Asset`,`CreateDateTime`) values('" + id + "'," + custId + ",'" + address + "','" + asset + "',now())";
                cmd.ExecuteNonQuery();
                return id.ToString();
            }
            catch(Exception ex)
            {
                return null;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public static Process GetProcess(int custId, string processId)
        {
            Process process = new Process();
            MySqlConnection conn = new MySqlConnection(constr);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                //cmd.CommandText = "select * from `goraven`.`process` where `CustId` = " + custId.ToString() + " and `ProcessId` = '" + processId + "' limit 1;";
                cmd.CommandText = "select * from `goraven`.`process` where `ProcessId` = '" + processId + "' and (`CustId` = "+ custId.ToString() +" or `CustId`in (select `CustId` from `goraven`.`cust` where `parentCustId` in (select coalesce(`ParentCustId`,`CustId`) from `goraven`.`cust` where `CustId` =" + custId.ToString() + ")) or `CustId` in (select `ParentCustId` from `goraven`.`cust` where `CustId` =" + custId.ToString() +"))";
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    process.processId = reader.GetString("ProcessId");
                    process.custId = Convert.ToInt32(reader.GetString("CustId"));
                    if (!reader.IsDBNull(2))
                    {
                        process.burnAddress = reader.GetString("BurnAddress");
                    }
                    if (!reader.IsDBNull(3))
                    {
                        process.asset = reader.GetString("Asset");
                    }
                    if (!reader.IsDBNull(4))
                    {
                        process.createDateTime = Convert.ToDateTime(reader.GetString("CreateDateTime"));
                    }
                    if (!reader.IsDBNull(5))
                    {
                        process.processed = Convert.ToBoolean(reader.GetString("Processed"));
                    }
                    if (!reader.IsDBNull(6))
                    {
                        process.processedDateTime = Convert.ToDateTime(reader.GetString("ProcessedDateTime"));
                    }
                    if (!reader.IsDBNull(7))
                    {
                        process.error = reader.GetString("Error");
                    }
                }
                reader.Close();
                reader.Dispose();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return process;
        }

        public static List<Process> GetProcessList(int custId)
        {
            List<Process> processList = new List<Process>();
            MySqlConnection conn = new MySqlConnection(constr);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                //cmd.CommandText = "select * from `goraven`.`process` where `CustId` = " + custId.ToString() + ";";
                cmd.CommandText = "select * from `goraven`.`process` where `CustId` = " + custId.ToString() + " or `CustId`in (select `CustId` from `goraven`.`cust` where `parentCustId` in (select coalesce(`ParentCustId`,`CustId`) from `goraven`.`cust` where `CustId` =" + custId.ToString() + ")) or `CustId` in (select `ParentCustId` from `goraven`.`cust` where `CustId` =" + custId.ToString() + ")";
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Process process = new Process();
                    process.processId = reader.GetString("ProcessId");
                    process.custId = Convert.ToInt32(reader.GetString("CustId"));
                    if (!reader.IsDBNull(2))
                    {
                        process.burnAddress = reader.GetString("BurnAddress");
                    }
                    if (!reader.IsDBNull(3))
                    {
                        process.asset = reader.GetString("Asset");
                    }
                    if (!reader.IsDBNull(4))
                    {
                        process.createDateTime = Convert.ToDateTime(reader.GetString("CreateDateTime"));
                    }
                    if (!reader.IsDBNull(5))
                    {
                        process.processed = Convert.ToBoolean(reader.GetString("Processed"));
                    }
                    if (!reader.IsDBNull(6))
                    {
                        process.processedDateTime = Convert.ToDateTime(reader.GetString("ProcessedDateTime"));
                    }
                    if (!reader.IsDBNull(7))
                    {
                        process.error = reader.GetString("Error");
                    }
                    processList.Add(process);
                }
                reader.Close();
                reader.Dispose();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return processList;
        }

        public static bool CheckProcessExist(string address, string asset)
        {
            //false is correct
            MySqlConnection conn = new MySqlConnection(constr);
            long exist;
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select count(*) from `goraven`.`process` where `BurnAddress` = '" + address + "' and `Asset` = '" + asset+ "';";
                exist = (long)cmd.ExecuteScalar();

                if (exist == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            /*
            catch (Exception ex)
            {
                //return true;
            }
            */
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public static bool CheckProcessAddress(int custId, string address)
        {
            //true is correct
            MySqlConnection conn = new MySqlConnection(constr);
            long exist;
            //get it and use it in query
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select count(*) from `goraven`.`burn_address` where `BurnAddress`= '" + address + "' and (`CustId` = " + custId.ToString() + " or `CustId`in (select `CustId` from `goraven`.`cust` where `parentCustId` in (select coalesce(`ParentCustId`,`CustId`) from `goraven`.`cust` where `CustId` =" + custId.ToString() + ")) or `CustId` in (select `ParentCustId` from `goraven`.`cust` where `CustId` =" + custId.ToString() + "))";
                exist = (long)cmd.ExecuteScalar();

                if (exist == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            /*
            catch (Exception ex)
            {
                //return true;
            }
            */
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public static bool CheckProcessAsset(int custId, string asset)
        {
            //true is correct
            MySqlConnection conn = new MySqlConnection(constr);
            long exist;
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select count(*) from `goraven`.`asset` where `Asset`='" + asset + "' and `CustId` = " + custId.ToString() + " and `Status` = 1;";
                exist = (long)cmd.ExecuteScalar();

                if (exist == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            /*
            catch (Exception ex)
            {
                //return true;
            }
            */
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public static Tx GetTx(int custId, string strTx)
        {
            Tx tx = new Tx();
            MySqlConnection conn = new MySqlConnection(constr);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from `goraven`.`tx` where `tx` = '" + strTx + "' and (`CustId` = " + custId.ToString() + " or `CustId`in (select `CustId` from `goraven`.`cust` where `parentCustId` in (select coalesce(`ParentCustId`,`CustId`) from `goraven`.`cust` where `CustId` =" + custId.ToString() + ")) or `CustId` in (select `ParentCustId` from `goraven`.`cust` where `CustId` =" + custId.ToString() + "))";
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tx.tx = reader.GetString("tx");
                    tx.custId = Convert.ToInt32(reader.GetString("CustId"));
                    if (!reader.IsDBNull(2))
                    {
                        tx.processId = reader.GetString("ProcessId");
                    }
                    if (!reader.IsDBNull(3))
                    {
                        tx.createDateTime = Convert.ToDateTime(reader.GetString("CreateDateTime"));
                    }
                }
                reader.Close();
                reader.Dispose();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return tx;
        }

        public static Tx GetTxByProcessId(int custId, string processId)
        {
            Tx tx = new Tx();
            MySqlConnection conn = new MySqlConnection(constr);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from `goraven`.`tx` where `ProcessId` = '" + processId + "' and (`CustId` = " + custId.ToString() + " or `CustId`in (select `CustId` from `goraven`.`cust` where `parentCustId` in (select coalesce(`ParentCustId`,`CustId`) from `goraven`.`cust` where `CustId` =" + custId.ToString() + ")) or `CustId` in (select `ParentCustId` from `goraven`.`cust` where `CustId` =" + custId.ToString() + "))";
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tx.tx = reader.GetString("tx");
                    tx.custId = Convert.ToInt32(reader.GetString("CustId"));
                    if (!reader.IsDBNull(2))
                    {
                        tx.processId = reader.GetString("ProcessId");
                    }
                    if (!reader.IsDBNull(3))
                    {
                        tx.createDateTime = Convert.ToDateTime(reader.GetString("CreateDateTime"));
                    }
                }
                reader.Close();
                reader.Dispose();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return tx;
        }

        public static List<Tx> GetTxList(int custId)
        {
            List<Tx> txList = new List<Tx>();
            MySqlConnection conn = new MySqlConnection(constr);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from `goraven`.`tx` where `CustId` = " + custId.ToString() + " or `CustId`in (select `CustId` from `goraven`.`cust` where `parentCustId` in (select coalesce(`ParentCustId`,`CustId`) from `goraven`.`cust` where `CustId` =" + custId.ToString() + ")) or `CustId` in (select `ParentCustId` from `goraven`.`cust` where `CustId` =" + custId.ToString() + ")";
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Tx tx = new Tx();
                    tx.tx = reader.GetString("tx");
                    tx.custId = Convert.ToInt32(reader.GetString("CustId"));
                    if (!reader.IsDBNull(2))
                    {
                        tx.processId = reader.GetString("ProcessId");
                    }
                    if (!reader.IsDBNull(3))
                    {
                        tx.createDateTime = Convert.ToDateTime(reader.GetString("CreateDateTime"));
                    }
                    txList.Add(tx);
                }
                reader.Close();
                reader.Dispose();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return txList;
        }

        public static void InsertLog(int custId, string IPAddress, string action, int error, string errorMessage)
        {
            MySqlConnection conn = new MySqlConnection(constr);
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "insert into `goraven`.`log`(`CustId`,`IPAddress`,`Action`,`Error`,`ErrorMessage`,`CreateDateTime`) values(" + custId.ToString() + ",'" + IPAddress + "','" + action + "'," + error.ToString() + ",'" + errorMessage + "',now());";
                cmd.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public static List<Processed> GetProcessedByAsset(int custId, string asset)
        {
            List<Processed> processedList = new List<Processed>();
            MySqlConnection conn = new MySqlConnection(constr);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "select ps.`ProcessId`, ps.`CustId`, ps.`BurnAddress`, ba.`BurnAddressRef`, ba.`Tag` as BurnAddressTag, ps.`Asset`, ae.`AssetRef`,ae.`Tag` as AssetTag,ae.`Status` as AssetStatus,ps.`ProcessedDateTime`,tx.`tx` " +
                "from `goraven`.`process` ps inner join " +
                "`goraven`.`burn_address` ba on ps.`BurnAddress` = ba.`BurnAddress` inner join " +
                "`goraven`.`asset` ae on ps.`Asset` = ae.`Asset` left join " +
                "`goraven`.`tx` tx on ps.`ProcessId` = tx.`ProcessId` " +
                "where " +
                "ps.`Processed` =1 " +
                "ps.`Asset` = '" + asset + "' and " +
                "(ps.`CustId` = " + custId.ToString() + " or " +
                "ps.`CustId`in (select `CustId` from `goraven`.`cust` where `parentCustId` in (select coalesce(`ParentCustId`,`CustId`) from `goraven`.`cust` where `CustId` =  " + custId.ToString() + ")) or " +
                "ps.`CustId` in (select `ParentCustId` from `goraven`.`cust` where `CustId` = " + custId.ToString() + "))";

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Processed processed = new Processed();
                    processed.processId = reader.GetString("ProcessId");
                    processed.custId = Convert.ToInt32(reader.GetString("CustId"));

                    if (!reader.IsDBNull(2))
                    {
                        processed.burnAddress = reader.GetString("BurnAddress");
                    }
                    if (!reader.IsDBNull(3))
                    {
                        processed.burnAddressRef = reader.GetString("BurnAddressRef");
                    }
                    if (!reader.IsDBNull(4))
                    {
                        processed.burnAddressTag = reader.GetString("BurnAddressTag");
                    }
                    if (!reader.IsDBNull(5))
                    {
                        processed.asset = reader.GetString("Asset");
                    }
                    if (!reader.IsDBNull(6))
                    {
                        processed.assetRef = reader.GetString("AssetRef");
                    }
                    if (!reader.IsDBNull(7))
                    {
                        processed.assetTag = reader.GetString("AssetTag");
                    }
                    if (!reader.IsDBNull(8))
                    {
                        processed.assetStatus = Convert.ToInt32(reader.GetString("AssetStatus"));
                    }
                    if (!reader.IsDBNull(9))
                    {
                        processed.processedDateTime = Convert.ToDateTime(reader.GetString("ProcessedDateTime"));
                    }
                    if (!reader.IsDBNull(10))
                    {
                        processed.tx = reader.GetString("tx");
                    }
                    processedList.Add(processed);
                }
                reader.Close();
                reader.Dispose();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return processedList;
            
        }

        public static List<Processed> GetProcessedByAssetRef(int custId, string assetRef)
        {
            List<Processed> processedList = new List<Processed>();
            MySqlConnection conn = new MySqlConnection(constr);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "select ps.`ProcessId`, ps.`CustId`, ps.`BurnAddress`, ba.`BurnAddressRef`, ba.`Tag` as BurnAddressTag, ps.`Asset`, ae.`AssetRef`,ae.`Tag` as AssetTag,ae.`Status` as AssetStatus,ps.`ProcessedDateTime`,tx.`tx` " +
                "from `goraven`.`process` ps inner join " +
                "`goraven`.`burn_address` ba on ps.`BurnAddress` = ba.`BurnAddress` inner join " +
                "`goraven`.`asset` ae on ps.`Asset` = ae.`Asset` left join " +
                "`goraven`.`tx` tx on ps.`ProcessId` = tx.`ProcessId` " +
                "where " +
                "ps.`Processed` =1 " +
                "ae.`AssetRef` = '" + assetRef + "' and " +
                "(ps.`CustId` = " + custId.ToString() + " or " +
                "ps.`CustId`in (select `CustId` from `goraven`.`cust` where `parentCustId` in (select coalesce(`ParentCustId`,`CustId`) from `goraven`.`cust` where `CustId` =  " + custId.ToString() + ")) or " +
                "ps.`CustId` in (select `ParentCustId` from `goraven`.`cust` where `CustId` = " + custId.ToString() + "))";

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Processed processed = new Processed();
                    processed.processId = reader.GetString("ProcessId");
                    processed.custId = Convert.ToInt32(reader.GetString("CustId"));

                    if (!reader.IsDBNull(2))
                    {
                        processed.burnAddress = reader.GetString("BurnAddress");
                    }
                    if (!reader.IsDBNull(3))
                    {
                        processed.burnAddressRef = reader.GetString("BurnAddressRef");
                    }
                    if (!reader.IsDBNull(4))
                    {
                        processed.burnAddressTag = reader.GetString("BurnAddressTag");
                    }
                    if (!reader.IsDBNull(5))
                    {
                        processed.asset = reader.GetString("Asset");
                    }
                    if (!reader.IsDBNull(6))
                    {
                        processed.assetRef = reader.GetString("AssetRef");
                    }
                    if (!reader.IsDBNull(7))
                    {
                        processed.assetTag = reader.GetString("AssetTag");
                    }
                    if (!reader.IsDBNull(8))
                    {
                        processed.assetStatus = Convert.ToInt32(reader.GetString("AssetStatus"));
                    }
                    if (!reader.IsDBNull(9))
                    {
                        processed.processedDateTime = Convert.ToDateTime(reader.GetString("ProcessedDateTime"));
                    }
                    if (!reader.IsDBNull(10))
                    {
                        processed.tx = reader.GetString("tx");
                    }
                    processedList.Add(processed);
                }
                reader.Close();
                reader.Dispose();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return processedList;
        }

        public static List<Processed> GetProcessedByBurnAddress(int custId, string burnAddress)
        {
            List<Processed> processedList = new List<Processed>();
            MySqlConnection conn = new MySqlConnection(constr);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "select ps.`ProcessId`, ps.`CustId`, ps.`BurnAddress`, ba.`BurnAddressRef`, ba.`Tag` as BurnAddressTag, ps.`Asset`, ae.`AssetRef`,ae.`Tag` as AssetTag,ae.`Status` as AssetStatus,ps.`ProcessedDateTime`,tx.`tx` " +
                "from `goraven`.`process` ps inner join " +
                "`goraven`.`burn_address` ba on ps.`BurnAddress` = ba.`BurnAddress` inner join " +
                "`goraven`.`asset` ae on ps.`Asset` = ae.`Asset` left join " +
                "`goraven`.`tx` tx on ps.`ProcessId` = tx.`ProcessId` " +
                "where " +
                "ps.`Processed` =1 " +
                "ps.`BurnAddress` = '" + burnAddress + "' and " +
                "(ps.`CustId` = " + custId.ToString() + " or " +
                "ps.`CustId`in (select `CustId` from `goraven`.`cust` where `parentCustId` in (select coalesce(`ParentCustId`,`CustId`) from `goraven`.`cust` where `CustId` =  " + custId.ToString() + ")) or " +
                "ps.`CustId` in (select `ParentCustId` from `goraven`.`cust` where `CustId` = " + custId.ToString() + "))";

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Processed processed = new Processed();
                    processed.processId = reader.GetString("ProcessId");
                    processed.custId = Convert.ToInt32(reader.GetString("CustId"));

                    if (!reader.IsDBNull(2))
                    {
                        processed.burnAddress = reader.GetString("BurnAddress");
                    }
                    if (!reader.IsDBNull(3))
                    {
                        processed.burnAddressRef = reader.GetString("BurnAddressRef");
                    }
                    if (!reader.IsDBNull(4))
                    {
                        processed.burnAddressTag = reader.GetString("BurnAddressTag");
                    }
                    if (!reader.IsDBNull(5))
                    {
                        processed.asset = reader.GetString("Asset");
                    }
                    if (!reader.IsDBNull(6))
                    {
                        processed.assetRef = reader.GetString("AssetRef");
                    }
                    if (!reader.IsDBNull(7))
                    {
                        processed.assetTag = reader.GetString("AssetTag");
                    }
                    if (!reader.IsDBNull(8))
                    {
                        processed.assetStatus = Convert.ToInt32(reader.GetString("AssetStatus"));
                    }
                    if (!reader.IsDBNull(9))
                    {
                        processed.processedDateTime = Convert.ToDateTime(reader.GetString("ProcessedDateTime"));
                    }
                    if (!reader.IsDBNull(10))
                    {
                        processed.tx = reader.GetString("tx");
                    }
                    processedList.Add(processed);
                }
                reader.Close();
                reader.Dispose();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return processedList;
        }

        public static List<Processed> GetProcessedByBurnAddressRef(int custId, string burnAddressRef)
        {
            List<Processed> processedList = new List<Processed>();
            MySqlConnection conn = new MySqlConnection(constr);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "select ps.`ProcessId`, ps.`CustId`, ps.`BurnAddress`, ba.`BurnAddressRef`, ba.`Tag` as BurnAddressTag, ps.`Asset`, ae.`AssetRef`,ae.`Tag` as AssetTag,ae.`Status` as AssetStatus,ps.`ProcessedDateTime`,tx.`tx` " +
                "from `goraven`.`process` ps inner join " +
                "`goraven`.`burn_address` ba on ps.`BurnAddress` = ba.`BurnAddress` inner join " +
                "`goraven`.`asset` ae on ps.`Asset` = ae.`Asset` left join " +
                "`goraven`.`tx` tx on ps.`ProcessId` = tx.`ProcessId` " +
                "where " +
                "ps.`Processed` =1 " +
                "ba.`BurnAddressRef` = '" + burnAddressRef + "' and " +
                "(ps.`CustId` = " + custId.ToString() + " or " +
                "ps.`CustId`in (select `CustId` from `goraven`.`cust` where `parentCustId` in (select coalesce(`ParentCustId`,`CustId`) from `goraven`.`cust` where `CustId` =  " + custId.ToString() + ")) or " +
                "ps.`CustId` in (select `ParentCustId` from `goraven`.`cust` where `CustId` = " + custId.ToString() + "))";

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Processed processed = new Processed();
                    processed.processId = reader.GetString("ProcessId");
                    processed.custId = Convert.ToInt32(reader.GetString("CustId"));

                    if (!reader.IsDBNull(2))
                    {
                        processed.burnAddress = reader.GetString("BurnAddress");
                    }
                    if (!reader.IsDBNull(3))
                    {
                        processed.burnAddressRef = reader.GetString("BurnAddressRef");
                    }
                    if (!reader.IsDBNull(4))
                    {
                        processed.burnAddressTag = reader.GetString("BurnAddressTag");
                    }
                    if (!reader.IsDBNull(5))
                    {
                        processed.asset = reader.GetString("Asset");
                    }
                    if (!reader.IsDBNull(6))
                    {
                        processed.assetRef = reader.GetString("AssetRef");
                    }
                    if (!reader.IsDBNull(7))
                    {
                        processed.assetTag = reader.GetString("AssetTag");
                    }
                    if (!reader.IsDBNull(8))
                    {
                        processed.assetStatus = Convert.ToInt32(reader.GetString("AssetStatus"));
                    }
                    if (!reader.IsDBNull(9))
                    {
                        processed.processedDateTime = Convert.ToDateTime(reader.GetString("ProcessedDateTime"));
                    }
                    if (!reader.IsDBNull(10))
                    {
                        processed.tx = reader.GetString("tx");
                    }
                    processedList.Add(processed);
                }
                reader.Close();
                reader.Dispose();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return processedList;
        }
    }


}
