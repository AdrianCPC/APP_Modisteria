using APP_Modisteria.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace APP_Modisteria.Data
{
    public class DetallePedidoData
    {
        public static bool registrarDetallePedido(DetallePedido oDetallePedido)
        {
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_RegistrarDetallePedido @idPedido, @idMaterial, @cantidad, @idDetallePedidoOut OUTPUT";

            objEst.AgregarParametro(ParameterDirection.Input, "@idPedido", SqlDbType.Int, 0, oDetallePedido.idPedido);
            objEst.AgregarParametro(ParameterDirection.Input, "@idMaterial", SqlDbType.Int, 0, oDetallePedido.idMaterial);
            objEst.AgregarParametro(ParameterDirection.Input, "@cantidad", SqlDbType.Int, 0, oDetallePedido.cantidad);
            objEst.AgregarParametro(ParameterDirection.Output, "@idDetallePedidoOut", SqlDbType.Int, 0, 0);

            bool resultado = objEst.EjecutarSentencia(sentencia, true);

            if (resultado)
            {
                if (objEst.CmdBD.Parameters["@idDetallePedidoOut"].Value != DBNull.Value)
                {
                    oDetallePedido.idDetallePedido = Convert.ToInt32(objEst.CmdBD.Parameters["@idDetallePedidoOut"].Value);
                }
            }
            objEst.CmdBD.Parameters.Clear();
            objEst.CerrarConexion();
            return resultado;
        }

        public static bool actualizarDetallePedido(DetallePedido oDetallePedido)
        {
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_ActualizarDetallePedido @idDetallePedido, @idPedido, @idMaterial, @cantidad";

            objEst.AgregarParametro(ParameterDirection.Input, "@idDetallePedido", SqlDbType.Int, 0, oDetallePedido.idDetallePedido);
            objEst.AgregarParametro(ParameterDirection.Input, "@idPedido", SqlDbType.Int, 0, oDetallePedido.idPedido);
            objEst.AgregarParametro(ParameterDirection.Input, "@idMaterial", SqlDbType.Int, 0, oDetallePedido.idMaterial);
            objEst.AgregarParametro(ParameterDirection.Input, "@cantidad", SqlDbType.Int, 0, oDetallePedido.cantidad);

            bool resultado = objEst.EjecutarSentencia(sentencia, true);
            objEst.CmdBD.Parameters.Clear();
            objEst.CerrarConexion();
            return resultado;
        }

        public static bool eliminarDetallePedido(int id)
        {
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_EliminarDetallePedido @idDetallePedido";
            objEst.AgregarParametro(ParameterDirection.Input, "@idDetallePedido", SqlDbType.Int, 0, id);

            bool resultado = objEst.EjecutarSentencia(sentencia, true);
            objEst.CmdBD.Parameters.Clear();
            objEst.CerrarConexion();
            return resultado;
        }

        public static List<DetallePedido> ListarDetallesPedido()
        {
            List<DetallePedido> oListaDetallePedido = new List<DetallePedido>();
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_ListarDetallesPedido";

            if (objEst.Consultar(sentencia, true))
            {
                SqlDataReader dr = objEst.Reader;
                while (dr.Read())
                {
                    DetallePedido detallePedido = new DetallePedido();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string propertyName = dr.GetName(i);
                        PropertyInfo propertyInfo = detallePedido.GetType().GetProperty(propertyName);
                        if (propertyInfo != null && !Convert.IsDBNull(dr.GetValue(i)))
                        {
                            propertyInfo.SetValue(detallePedido, Convert.ChangeType(dr.GetValue(i), propertyInfo.PropertyType));
                        }
                    }
                    oListaDetallePedido.Add(detallePedido);
                }
                dr.Close();
            }
            objEst.CerrarConexion();
            return oListaDetallePedido;
        }

        public static DetallePedido ObtenerDetallePedido(int id)
        {
            DetallePedido oDetallePedido = null;
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_ConsultarDetallePedidoPorId @idDetallePedido"; // Nombre corregido
            objEst.AgregarParametro(ParameterDirection.Input, "@idDetallePedido", SqlDbType.Int, 0, id);

            if (objEst.Consultar(sentencia, true))
            {
                SqlDataReader dr = objEst.Reader;
                if (dr.Read())
                {
                    oDetallePedido = new DetallePedido();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string propertyName = dr.GetName(i);
                        PropertyInfo propertyInfo = oDetallePedido.GetType().GetProperty(propertyName);
                        if (propertyInfo != null && !Convert.IsDBNull(dr.GetValue(i)))
                        {
                            propertyInfo.SetValue(oDetallePedido, Convert.ChangeType(dr.GetValue(i), propertyInfo.PropertyType));
                        }
                    }
                }
                dr.Close();
            }
            objEst.CmdBD.Parameters.Clear();
            objEst.CerrarConexion();
            return oDetallePedido;
        }

        public static List<DetallePedido> ListarDetallesPedidoPorPedidoId(int idPedido)
        {
            List<DetallePedido> oListaDetallePedido = new List<DetallePedido>();
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_ListarDetallesPedidoPorPedidoId @idPedido";
            objEst.AgregarParametro(ParameterDirection.Input, "@idPedido", SqlDbType.Int, 0, idPedido);

            if (objEst.Consultar(sentencia, true))
            {
                SqlDataReader dr = objEst.Reader;
                while (dr.Read())
                {
                    DetallePedido detallePedido = new DetallePedido();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string propertyName = dr.GetName(i);
                        PropertyInfo propertyInfo = detallePedido.GetType().GetProperty(propertyName);
                        if (propertyInfo != null && !Convert.IsDBNull(dr.GetValue(i)))
                        {
                            propertyInfo.SetValue(detallePedido, Convert.ChangeType(dr.GetValue(i), propertyInfo.PropertyType));
                        }
                    }
                    oListaDetallePedido.Add(detallePedido);
                }
                dr.Close();
            }
            objEst.CmdBD.Parameters.Clear();
            objEst.CerrarConexion();
            return oListaDetallePedido;
        }
    }
}