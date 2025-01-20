using APP_Modisteria.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace APP_Modisteria.Data
{
    public class InventarioData
    {
        public static bool registrarInventario(Inventario oInventario)
        {
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_RegistrarInventario @idMaterial, @cantidad, @idInventarioOut OUTPUT";

            objEst.AgregarParametro(ParameterDirection.Input, "@idMaterial", SqlDbType.Int, 0, oInventario.idMaterial);
            objEst.AgregarParametro(ParameterDirection.Input, "@cantidad", SqlDbType.Int, 0, oInventario.cantidad);
            objEst.AgregarParametro(ParameterDirection.Output, "@idInventarioOut", SqlDbType.Int, 0, 0);

            bool resultado = objEst.EjecutarSentencia(sentencia, true);

            if (resultado)
            {
                if (objEst.CmdBD.Parameters["@idInventarioOut"].Value != DBNull.Value)
                {
                    oInventario.idInventario = Convert.ToInt32(objEst.CmdBD.Parameters["@idInventarioOut"].Value);
                }
            }
            objEst.CmdBD.Parameters.Clear();
            objEst.CerrarConexion();
            return resultado;
        }

        public static bool actualizarInventario(Inventario oInventario)
        {
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_ActualizarInventario @idInventario, @idMaterial, @cantidad";

            objEst.AgregarParametro(ParameterDirection.Input, "@idInventario", SqlDbType.Int, 0, oInventario.idInventario);
            objEst.AgregarParametro(ParameterDirection.Input, "@idMaterial", SqlDbType.Int, 0, oInventario.idMaterial);
            objEst.AgregarParametro(ParameterDirection.Input, "@cantidad", SqlDbType.Int, 0, oInventario.cantidad);

            bool resultado = objEst.EjecutarSentencia(sentencia, true);
            objEst.CmdBD.Parameters.Clear();
            objEst.CerrarConexion();
            return resultado;
        }

        public static bool eliminarInventario(int id)
        {
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_EliminarInventario @idInventario";
            objEst.AgregarParametro(ParameterDirection.Input, "@idInventario", SqlDbType.Int, 0, id);

            bool resultado = objEst.EjecutarSentencia(sentencia, true);
            objEst.CmdBD.Parameters.Clear();
            objEst.CerrarConexion();
            return resultado;
        }

        public static List<Inventario> ListarInventarios()
        {
            List<Inventario> oListaInventario = new List<Inventario>();
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_ListarInventarios";

            if (objEst.Consultar(sentencia, true))
            {
                SqlDataReader dr = objEst.Reader;
                while (dr.Read())
                {
                    Inventario inventario = new Inventario();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string propertyName = dr.GetName(i);
                        PropertyInfo propertyInfo = inventario.GetType().GetProperty(propertyName);
                        if (propertyInfo != null && !Convert.IsDBNull(dr.GetValue(i)))
                        {
                            propertyInfo.SetValue(inventario, Convert.ChangeType(dr.GetValue(i), propertyInfo.PropertyType));
                        }
                    }
                    oListaInventario.Add(inventario);
                }
                dr.Close();
            }
            objEst.CerrarConexion();
            return oListaInventario;
        }

        public static Inventario ObtenerInventario(int id)
        {
            Inventario oInventario = null;
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_ConsultarInventarioPorId @idInventario";
            objEst.AgregarParametro(ParameterDirection.Input, "@idInventario", SqlDbType.Int, 0, id);

            if (objEst.Consultar(sentencia, true))
            {
                SqlDataReader dr = objEst.Reader;
                if (dr.Read())
                {
                    oInventario = new Inventario();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string propertyName = dr.GetName(i);
                        PropertyInfo propertyInfo = oInventario.GetType().GetProperty(propertyName);
                        if (propertyInfo != null && !Convert.IsDBNull(dr.GetValue(i)))
                        {
                            propertyInfo.SetValue(oInventario, Convert.ChangeType(dr.GetValue(i), propertyInfo.PropertyType));
                        }
                    }
                }
                dr.Close();
            }
            objEst.CmdBD.Parameters.Clear();
            objEst.CerrarConexion();
            return oInventario;
        }

        public static Inventario ObtenerInventarioPorIdMaterial(int idMaterial)
        {
            Inventario oInventario = null;
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_ConsultarInventarioPorIdMaterial @idMaterial";
            objEst.AgregarParametro(ParameterDirection.Input, "@idMaterial", SqlDbType.Int, 0, idMaterial);

            if (objEst.Consultar(sentencia, true))
            {
                SqlDataReader dr = objEst.Reader;
                if (dr.Read())
                {
                    oInventario = new Inventario();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string propertyName = dr.GetName(i);
                        PropertyInfo propertyInfo = oInventario.GetType().GetProperty(propertyName);
                        if (propertyInfo != null && !Convert.IsDBNull(dr.GetValue(i)))
                        {
                            propertyInfo.SetValue(oInventario, Convert.ChangeType(dr.GetValue(i), propertyInfo.PropertyType));
                        }
                    }
                }
                dr.Close();
            }
            objEst.CmdBD.Parameters.Clear();
            objEst.CerrarConexion();
            return oInventario;
        }
    }
}