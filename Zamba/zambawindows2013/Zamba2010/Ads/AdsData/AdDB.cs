using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AdsData
{
    public class AdDB
    {
        private const String SP_INSERT_IMAGE = "sp_InsertAdImage";
        private const String SP_DELETE_IMAGE = "sp_DeleteAdImage";
        private const String SP_DELETE_IMAGE_BY_NAME = "sp_DeleteAdImageByName";
        private const String SP_GET_IMAGES = "sp_GetAdImages";
        private const String SP_EXISTS_IMAGE_IN_AD = "sp_ExistsImageInAd";

        private static String ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["MainConnection"].ToString(); }
        }

        public static void Insert(Int64 adId, String fileName, String fileExtension, byte[] imageBinary, bool isMainPreview, DateTime creationDate, char imageSize)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(ConnectionString);
                cmd = new SqlCommand(SP_INSERT_IMAGE, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@adId", adId));
                cmd.Parameters.Add(new SqlParameter("@fileName", fileName));
                cmd.Parameters.Add(new SqlParameter("@fileExtension", fileExtension));
                cmd.Parameters.Add(new SqlParameter("@bynary", imageBinary));
                cmd.Parameters.Add(new SqlParameter("@isMain", isMainPreview ? 1 : 0));
                cmd.Parameters.Add(new SqlParameter("@creationDate", creationDate));
                cmd.Parameters.Add(new SqlParameter("@imageSize", imageSize));

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != conn)
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();

                    conn.Dispose();
                }

                if (null != cmd)
                    cmd.Dispose();
            }
        }
        public static void Delete(Int64 imageId)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(ConnectionString);
                cmd = new SqlCommand(SP_DELETE_IMAGE, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", imageId));

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != conn)
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();

                    conn.Dispose();
                }

                if (null != cmd)
                    cmd.Dispose();
            }
        }
        public static void Delete(Int64 adId, String fileName, String fileExtension)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(ConnectionString);
                cmd = new SqlCommand(SP_DELETE_IMAGE_BY_NAME, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@adId", adId));
                cmd.Parameters.Add(new SqlParameter("@fileName", fileName));
                cmd.Parameters.Add(new SqlParameter("@fileExtension", fileExtension));

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != conn)
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();

                    conn.Dispose();
                }

                if (null != cmd)
                    cmd.Dispose();
            }
        }
        public static DataTable Get(Int64 adId)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;

            try
            {
                conn = new SqlConnection(ConnectionString);
                cmd = new SqlCommand(SP_GET_IMAGES, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@adID", adId));

                da = new SqlDataAdapter(cmd);

                conn.Open();
                da.Fill(dt);
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != conn)
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();

                    conn.Dispose();
                }

                if (null != cmd)
                    cmd.Dispose();

                if (null != da)
                    da.Dispose();
            }

            return dt;
        }
        public static Int32 Exists(Int64 adId, String fileName, String fileExtension)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            Int32 ImageCount = 0;
            try
            {
                conn = new SqlConnection(ConnectionString);
                cmd = new SqlCommand(SP_EXISTS_IMAGE_IN_AD, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@adID", adId));
                cmd.Parameters.Add(new SqlParameter("@fileName", fileName));
                cmd.Parameters.Add(new SqlParameter("@fileExtension", fileExtension));

                conn.Open();
                ImageCount = (Int32)cmd.ExecuteScalar();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != conn)
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();

                    conn.Dispose();
                }

                if (null != cmd)
                    cmd.Dispose();
            }

            return ImageCount;
        }
    }
}