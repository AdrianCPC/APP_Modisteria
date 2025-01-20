using APP_Modisteria.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace APP_Modisteria.Data
{
    public class ClienteData
    {
        public static bool registrarCliente(Cliente oCliente)
        {
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_RegistrarCliente @nombre, @direccion, @telefono, @email, @idClienteOut OUTPUT";

            objEst.AgregarParametro(ParameterDirection.Input, "@nombre", SqlDbType.VarChar, 100, oCliente.nombre);
            objEst.AgregarParametro(ParameterDirection.Input, "@direccion", SqlDbType.VarChar, 200, oCliente.direccion);
            objEst.AgregarParametro(ParameterDirection.Input, "@telefono", SqlDbType.VarChar, 20, oCliente.telefono);
            objEst.AgregarParametro(ParameterDirection.Input, "@email", SqlDbType.VarChar, 50, oCliente.email);
            objEst.AgregarParametro(ParameterDirection.Output, "@idClienteOut", SqlDbType.Int, 0, 0);

            bool resultado = objEst.EjecutarSentencia(sentencia, true);

            if (resultado)
            {
                if (objEst.CmdBD.Parameters["@idClienteOut"].Value != DBNull.Value)
                {
                    oCliente.idCliente = Convert.ToInt32(objEst.CmdBD.Parameters["@idClienteOut"].Value);
                }
            }
            objEst.CmdBD.Parameters.Clear();
            objEst.CerrarConexion();
            return resultado;
        }

        public static bool actualizarCliente(Cliente oCliente)
        {
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_ActualizarCliente @idCliente, @nombre, @direccion, @telefono, @email";

            objEst.AgregarParametro(ParameterDirection.Input, "@idCliente", SqlDbType.Int, 0, oCliente.idCliente);
            objEst.AgregarParametro(ParameterDirection.Input, "@nombre", SqlDbType.VarChar, 100, oCliente.nombre);
            objEst.AgregarParametro(ParameterDirection.Input, "@direccion", SqlDbType.VarChar, 200, oCliente.direccion);
            objEst.AgregarParametro(ParameterDirection.Input, "@telefono", SqlDbType.VarChar, 20, oCliente.telefono);
            objEst.AgregarParametro(ParameterDirection.Input, "@email", SqlDbType.VarChar, 50, oCliente.email);

            bool resultado = objEst.EjecutarSentencia(sentencia, true);
            objEst.CmdBD.Parameters.Clear();
            objEst.CerrarConexion();
            return resultado;
        }

        public static bool eliminarCliente(int id)
        {
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_EliminarCliente @idCliente";
            objEst.AgregarParametro(ParameterDirection.Input, "@idCliente", SqlDbType.Int, 0, id);

            bool resultado = objEst.EjecutarSentencia(sentencia, true);
            objEst.CmdBD.Parameters.Clear();
            objEst.CerrarConexion();
            return resultado;
        }

        public static List<Cliente> ListarClientes()
        {
            List<Cliente> oListaCliente = new List<Cliente>();
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_ListarClientes";

            if (objEst.Consultar(sentencia, true))
            {
                SqlDataReader dr = objEst.Reader;
                while (dr.Read())
                {
                    Cliente cliente = new Cliente();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string propertyName = dr.GetName(i);
                        PropertyInfo propertyInfo = cliente.GetType().GetProperty(propertyName);
                        if (propertyInfo != null && !Convert.IsDBNull(dr.GetValue(i)))
                        {
                            propertyInfo.SetValue(cliente, Convert.ChangeType(dr.GetValue(i), propertyInfo.PropertyType));
                        }
                    }
                    oListaCliente.Add(cliente);
                }
                dr.Close();
            }
            objEst.CerrarConexion();
            return oListaCliente;
        }

        public static Cliente ObtenerCliente(int id)
        {
            Cliente oCliente = null;
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_ConsultarClientePorId @idCliente";
            objEst.AgregarParametro(ParameterDirection.Input, "@idCliente", SqlDbType.Int, 0, id);

            if (objEst.Consultar(sentencia, true))
            {
                SqlDataReader dr = objEst.Reader;
                if (dr.Read())
                {
                    oCliente = new Cliente();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string propertyName = dr.GetName(i);
                        PropertyInfo propertyInfo = oCliente.GetType().GetProperty(propertyName);
                        if (propertyInfo != null && !Convert.IsDBNull(dr.GetValue(i)))
                        {
                            propertyInfo.SetValue(oCliente, Convert.ChangeType(dr.GetValue(i), propertyInfo.PropertyType));
                        }
                    }
                }
                dr.Close();
            }
            objEst.CmdBD.Parameters.Clear();
            objEst.CerrarConexion();
            return oCliente;
        }
    }
}