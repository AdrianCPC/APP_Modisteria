using APP_Modisteria.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace APP_Modisteria.Data
{
    public class ReporteData
    {
        public static bool registrarReporte(Reporte oReporte)
        {
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_RegistrarReporte @fechaGeneracion, @contenido, @idReporteOut OUTPUT";

            objEst.AgregarParametro(ParameterDirection.Input, "@fechaGeneracion", SqlDbType.Date, 0, oReporte.fechaGeneracion);
            objEst.AgregarParametro(ParameterDirection.Input, "@descripcion", SqlDbType.VarChar, 200, oReporte.contenido);
            objEst.AgregarParametro(ParameterDirection.Output, "@idReporteOut", SqlDbType.Int, 0, 0);

            bool resultado = objEst.EjecutarSentencia(sentencia, true);

            if (resultado)
            {
                if (objEst.CmdBD.Parameters["@idReporteOut"].Value != DBNull.Value)
                {
                    oReporte.idReporte = Convert.ToInt32(objEst.CmdBD.Parameters["@idReporteOut"].Value);
                }
            }
            objEst.CmdBD.Parameters.Clear();
            objEst.CerrarConexion();
            return resultado;
        }

        public static bool actualizarReporte(Reporte oReporte)
        {
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_ActualizarReporte @idReporte, @fechaGeneracion, @contenido";

            objEst.AgregarParametro(ParameterDirection.Input, "@idReporte", SqlDbType.Int, 0, oReporte.idReporte);
            objEst.AgregarParametro(ParameterDirection.Input, "@fechaGeneracion", SqlDbType.Date, 0, oReporte.fechaGeneracion);
            objEst.AgregarParametro(ParameterDirection.Input, "@descripcion", SqlDbType.VarChar, 200, oReporte.contenido);

            bool resultado = objEst.EjecutarSentencia(sentencia, true);
            objEst.CmdBD.Parameters.Clear();
            objEst.CerrarConexion();
            return resultado;
        }

        public static bool eliminarReporte(int id)
        {
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_EliminarReporte @idReporte";
            objEst.AgregarParametro(ParameterDirection.Input, "@idReporte", SqlDbType.Int, 0, id);

            bool resultado = objEst.EjecutarSentencia(sentencia, true);
            objEst.CmdBD.Parameters.Clear();
            objEst.CerrarConexion();
            return resultado;
        }

        public static List<Reporte> ListarReportes()
        {
            List<Reporte> oListaReporte = new List<Reporte>();
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_ListarReportes";

            if (objEst.Consultar(sentencia, true))
            {
                SqlDataReader dr = objEst.Reader;
                while (dr.Read())
                {
                    Reporte reporte = new Reporte();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string propertyName = dr.GetName(i);
                        PropertyInfo propertyInfo = reporte.GetType().GetProperty(propertyName);
                        if (propertyInfo != null && !Convert.IsDBNull(dr.GetValue(i)))
                        {
                            propertyInfo.SetValue(reporte, Convert.ChangeType(dr.GetValue(i), propertyInfo.PropertyType));
                        }
                    }
                    oListaReporte.Add(reporte);
                }
                dr.Close();
            }
            objEst.CerrarConexion();
            return oListaReporte;
        }

        public static Reporte ObtenerReporte(int id)
        {
            Reporte oReporte = null;
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_ConsultarReportePorId @idReporte";
            objEst.AgregarParametro(ParameterDirection.Input, "@idReporte", SqlDbType.Int, 0, id);

            if (objEst.Consultar(sentencia, true))
            {
                SqlDataReader dr = objEst.Reader;
                if (dr.Read())
                {
                    oReporte = new Reporte();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string propertyName = dr.GetName(i);
                        PropertyInfo propertyInfo = oReporte.GetType().GetProperty(propertyName);
                        if (propertyInfo != null && !Convert.IsDBNull(dr.GetValue(i)))
                        {
                            propertyInfo.SetValue(oReporte, Convert.ChangeType(dr.GetValue(i), propertyInfo.PropertyType));
                        }
                    }
                }
                dr.Close();
            }
            objEst.CmdBD.Parameters.Clear();
            objEst.CerrarConexion();
            return oReporte;
        }
    }
}