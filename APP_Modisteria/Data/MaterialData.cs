using APP_Modisteria.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace APP_Modisteria.Data
{
    public class MaterialData
    {
        public static bool registrarMaterial(Material oMaterial)
        {
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_RegistrarMaterial @nombre, @tipo, @cantidadDisponible, @precioUnitario, @idMaterialOut OUTPUT";

            objEst.AgregarParametro(ParameterDirection.Input, "@nombre", SqlDbType.VarChar, 50, oMaterial.nombre);
            objEst.AgregarParametro(ParameterDirection.Input, "@tipo", SqlDbType.VarChar, 50, oMaterial.tipo);
            objEst.AgregarParametro(ParameterDirection.Input, "@cantidadDisponible", SqlDbType.Int, 0, oMaterial.cantidadDisponible);
            objEst.AgregarParametro(ParameterDirection.Input, "@precioUnitario", SqlDbType.Decimal, 0, oMaterial.precioUnitario);
            objEst.AgregarParametro(ParameterDirection.Output, "@idMaterialOut", SqlDbType.Int, 0, 0);

            bool resultado = objEst.EjecutarSentencia(sentencia, true);

            if (resultado)
            {
                if (objEst.CmdBD.Parameters["@idMaterialOut"].Value != DBNull.Value)
                {
                    oMaterial.idMaterial = Convert.ToInt32(objEst.CmdBD.Parameters["@idMaterialOut"].Value);
                }
            }
            objEst.CmdBD.Parameters.Clear();
            objEst.CerrarConexion();
            return resultado;
        }

        public static bool actualizarMaterial(Material oMaterial)
        {
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_ActualizarMaterial @idMaterial, @nombre, @tipo, @cantidadDisponible, @precioUnitario";

            objEst.AgregarParametro(ParameterDirection.Input, "@idMaterial", SqlDbType.Int, 0, oMaterial.idMaterial);
            objEst.AgregarParametro(ParameterDirection.Input, "@nombre", SqlDbType.VarChar, 50, oMaterial.nombre);
            objEst.AgregarParametro(ParameterDirection.Input, "@tipo", SqlDbType.VarChar, 50, oMaterial.tipo);
            objEst.AgregarParametro(ParameterDirection.Input, "@cantidadDisponible", SqlDbType.Int, 0, oMaterial.cantidadDisponible);
            objEst.AgregarParametro(ParameterDirection.Input, "@precioUnitario", SqlDbType.Decimal, 0, oMaterial.precioUnitario);

            bool resultado = objEst.EjecutarSentencia(sentencia, true);
            objEst.CmdBD.Parameters.Clear();
            objEst.CerrarConexion();
            return resultado;
        }

        public static bool eliminarMaterial(int id)
        {
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_EliminarMaterial @idMaterial";
            objEst.AgregarParametro(ParameterDirection.Input, "@idMaterial", SqlDbType.Int, 0, id);

            bool resultado = objEst.EjecutarSentencia(sentencia, true);
            objEst.CmdBD.Parameters.Clear();
            objEst.CerrarConexion();
            return resultado;
        }

        public static List<Material> ListarMateriales()
        {
            List<Material> oListaMaterial = new List<Material>();
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_ListarMateriales";

            if (objEst.Consultar(sentencia, true))
            {
                SqlDataReader dr = objEst.Reader;
                while (dr.Read())
                {
                    Material material = new Material();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string propertyName = dr.GetName(i);
                        PropertyInfo propertyInfo = material.GetType().GetProperty(propertyName);
                        if (propertyInfo != null && !Convert.IsDBNull(dr.GetValue(i)))
                        {
                            propertyInfo.SetValue(material, Convert.ChangeType(dr.GetValue(i), propertyInfo.PropertyType));
                        }
                    }
                    oListaMaterial.Add(material);
                }
                dr.Close();
            }
            objEst.CerrarConexion();
            return oListaMaterial;
        }

        public static Material ObtenerMaterial(int id)
        {
            Material oMaterial = null;
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_ConsultarMaterialPorId @idMaterial";
            objEst.AgregarParametro(ParameterDirection.Input, "@idMaterial", SqlDbType.Int, 0, id);

            if (objEst.Consultar(sentencia, true))
            {
                SqlDataReader dr = objEst.Reader;
                if (dr.Read())
                {
                    oMaterial = new Material();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string propertyName = dr.GetName(i);
                        PropertyInfo propertyInfo = oMaterial.GetType().GetProperty(propertyName);
                        if (propertyInfo != null && !Convert.IsDBNull(dr.GetValue(i)))
                        {
                            propertyInfo.SetValue(oMaterial, Convert.ChangeType(dr.GetValue(i), propertyInfo.PropertyType));
                        }
                    }
                }
                dr.Close();
            }
            objEst.CmdBD.Parameters.Clear();
            objEst.CerrarConexion();
            return oMaterial;
        }
    }
}