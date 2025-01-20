using APP_Modisteria.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace APP_Modisteria.Data
{
    public class PedidoData
    {
        public static bool registrarPedido(Pedido oPedido)
        {
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_RegistrarPedido @fecha, @estado, @cantidad, @total, @idCliente, @idPedidoOut OUTPUT";

            objEst.AgregarParametro(ParameterDirection.Input, "@fecha", SqlDbType.Date, 0, oPedido.fecha);
            objEst.AgregarParametro(ParameterDirection.Input, "@estado", SqlDbType.VarChar, 20, oPedido.estado);
            objEst.AgregarParametro(ParameterDirection.Input, "@cantidad", SqlDbType.Int, 0, oPedido.cantidad);
            objEst.AgregarParametro(ParameterDirection.Input, "@total", SqlDbType.Decimal, 0, oPedido.total);
            objEst.AgregarParametro(ParameterDirection.Input, "@idCliente", SqlDbType.Int, 0, oPedido.idCliente);
            objEst.AgregarParametro(ParameterDirection.Output, "@idPedidoOut", SqlDbType.Int, 0, 0);

            bool resultado = objEst.EjecutarSentencia(sentencia, true);

            if (resultado)
            {
                if (objEst.CmdBD.Parameters["@idPedidoOut"].Value != DBNull.Value)
                {
                    oPedido.idPedido = Convert.ToInt32(objEst.CmdBD.Parameters["@idPedidoOut"].Value);
                }
            }
            objEst.CmdBD.Parameters.Clear();
            objEst.CerrarConexion();
            return resultado;
        }

        public static bool actualizarPedido(Pedido oPedido)
        {
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_ActualizarPedido @idPedido, @fecha, @estado, @cantidad, @total, @idCliente";

            objEst.AgregarParametro(ParameterDirection.Input, "@idPedido", SqlDbType.Int, 0, oPedido.idPedido);
            objEst.AgregarParametro(ParameterDirection.Input, "@fecha", SqlDbType.Date, 0, oPedido.fecha);
            objEst.AgregarParametro(ParameterDirection.Input, "@estado", SqlDbType.VarChar, 20, oPedido.estado);
            objEst.AgregarParametro(ParameterDirection.Input, "@cantidad", SqlDbType.Int, 0, oPedido.cantidad);
            objEst.AgregarParametro(ParameterDirection.Input, "@total", SqlDbType.Decimal, 0, oPedido.total);
            objEst.AgregarParametro(ParameterDirection.Input, "@idCliente", SqlDbType.Int, 0, oPedido.idCliente);

            bool resultado = objEst.EjecutarSentencia(sentencia, true);
            objEst.CmdBD.Parameters.Clear();
            objEst.CerrarConexion();
            return resultado;
        }

        public static bool eliminarPedido(int id)
        {
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_EliminarPedido @idPedido";
            objEst.AgregarParametro(ParameterDirection.Input, "@idPedido", SqlDbType.Int, 0, id);

            bool resultado = objEst.EjecutarSentencia(sentencia, true);
            objEst.CmdBD.Parameters.Clear();
            objEst.CerrarConexion();
            return resultado;
        }

        public static List<Pedido> ListarPedidos()
        {
            List<Pedido> oListaPedido = new List<Pedido>();
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_ListarPedidos";

            if (objEst.Consultar(sentencia, true))
            {
                SqlDataReader dr = objEst.Reader;
                while (dr.Read())
                {
                    Pedido pedido = new Pedido();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string propertyName = dr.GetName(i);
                        PropertyInfo propertyInfo = pedido.GetType().GetProperty(propertyName);
                        if (propertyInfo != null && !Convert.IsDBNull(dr.GetValue(i)))
                        {
                            propertyInfo.SetValue(pedido, Convert.ChangeType(dr.GetValue(i), propertyInfo.PropertyType));
                        }
                    }
                    oListaPedido.Add(pedido);
                }
                dr.Close();
            }
            objEst.CerrarConexion();
            return oListaPedido;
        }

        public static Pedido ObtenerPedido(int id)
        {
            Pedido oPedido = null;
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_ConsultarPedidoPorId @idPedido";
            objEst.AgregarParametro(ParameterDirection.Input, "@idPedido", SqlDbType.Int, 0, id);

            if (objEst.Consultar(sentencia, true))
            {
                SqlDataReader dr = objEst.Reader;
                if (dr.Read())
                {
                    oPedido = new Pedido();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string propertyName = dr.GetName(i);
                        PropertyInfo propertyInfo = oPedido.GetType().GetProperty(propertyName);
                        if (propertyInfo != null && !Convert.IsDBNull(dr.GetValue(i)))
                        {
                            propertyInfo.SetValue(oPedido, Convert.ChangeType(dr.GetValue(i), propertyInfo.PropertyType));
                        }
                    }
                }
                dr.Close();
            }
            objEst.CmdBD.Parameters.Clear();
            objEst.CerrarConexion();
            return oPedido;
        }
    }
}