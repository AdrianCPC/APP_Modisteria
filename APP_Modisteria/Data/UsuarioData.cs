using APP_Modisteria.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;

namespace APP_Modisteria.Data
{
    public class UsuarioData
    {
        public static bool registrarUsuario(Usuario oUsuario)
        {
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_RegistrarUsuario @nombreUsuario, @contrasena, @tipoUsuario, @idUsuarioOut OUTPUT";

            objEst.AgregarParametro(System.Data.ParameterDirection.Input, "@nombreUsuario", System.Data.SqlDbType.VarChar, 50, oUsuario.nombreUsuario);
            objEst.AgregarParametro(System.Data.ParameterDirection.Input, "@contrasena", System.Data.SqlDbType.VarChar, 50, oUsuario.contrasena);
            objEst.AgregarParametro(System.Data.ParameterDirection.Input, "@tipoUsuario", System.Data.SqlDbType.Char, 1, oUsuario.tipoUsuario);
            objEst.AgregarParametro(System.Data.ParameterDirection.Output, "@idUsuarioOut", System.Data.SqlDbType.Int, 0, 0);

            bool resultado = objEst.EjecutarSentencia(sentencia, true); // Usar parámetros

            if (resultado)
            {
                // Obtener el valor del parámetro de salida
                if (objEst.CmdBD.Parameters["@idUsuarioOut"].Value != DBNull.Value)
                {
                    oUsuario.IdUsuario = Convert.ToInt32(objEst.CmdBD.Parameters["@idUsuarioOut"].Value);
                }
            }
            objEst.CmdBD.Parameters.Clear();//Limpiar los parametros
            objEst.CerrarConexion();
            return resultado;
        }

        public static bool actualizarUsuario(Usuario oUsuario)
        {
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_ActualizarUsuario @idUsuario, @nombreUsuario, @contrasena, @tipoUsuario";

            objEst.AgregarParametro(System.Data.ParameterDirection.Input, "@idUsuario", System.Data.SqlDbType.Int, 0, oUsuario.IdUsuario);
            objEst.AgregarParametro(System.Data.ParameterDirection.Input, "@nombreUsuario", System.Data.SqlDbType.VarChar, 50, oUsuario.nombreUsuario);
            objEst.AgregarParametro(System.Data.ParameterDirection.Input, "@contrasena", System.Data.SqlDbType.VarChar, 50, oUsuario.contrasena);
            objEst.AgregarParametro(System.Data.ParameterDirection.Input, "@tipoUsuario", System.Data.SqlDbType.Char, 1, oUsuario.tipoUsuario);

            bool resultado = objEst.EjecutarSentencia(sentencia, true);
            objEst.CmdBD.Parameters.Clear();
            objEst.CerrarConexion();
            return resultado;
        }

        public static bool eliminarUsuario(int id)
        {
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_EliminarUsuario @idUsuario";
            objEst.AgregarParametro(System.Data.ParameterDirection.Input, "@idUsuario", System.Data.SqlDbType.Int, 0, id);
            bool resultado = objEst.EjecutarSentencia(sentencia, true);
            objEst.CmdBD.Parameters.Clear();
            objEst.CerrarConexion();
            return resultado;
        }

        public static List<Usuario> ListarUsuarios()
        {
            List<Usuario> oListaUsuario = new List<Usuario>();
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_ListarUsuarios";

            if (objEst.Consultar(sentencia, true)) //Usar parametros aunque no los tenga
            {
                SqlDataReader dr = objEst.Reader;
                while (dr.Read())
                {
                    Usuario usuario = new Usuario();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string propertyName = dr.GetName(i);
                        PropertyInfo propertyInfo = usuario.GetType().GetProperty(propertyName);
                        if (propertyInfo != null && !Convert.IsDBNull(dr.GetValue(i)))
                        {
                            propertyInfo.SetValue(usuario, Convert.ChangeType(dr.GetValue(i), propertyInfo.PropertyType));
                        }
                    }
                    oListaUsuario.Add(usuario);
                }
                dr.Close();
            }
            objEst.CerrarConexion();//cerrar la conexion
            return oListaUsuario;
        }

        public static Usuario ObtenerUsuario(int id)
        {
            Usuario oUsuario = null;
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_ConsultarUsuarioPorId @idUsuario";
            objEst.AgregarParametro(System.Data.ParameterDirection.Input, "@idUsuario", System.Data.SqlDbType.Int, 0, id);
            if (objEst.Consultar(sentencia, true)) //Usar parametros aunque no los tenga
            {
                SqlDataReader dr = objEst.Reader;
                if (dr.Read())
                {
                    oUsuario = new Usuario();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string propertyName = dr.GetName(i);
                        PropertyInfo propertyInfo = oUsuario.GetType().GetProperty(propertyName);
                        if (propertyInfo != null && !Convert.IsDBNull(dr.GetValue(i)))
                        {
                            propertyInfo.SetValue(oUsuario, Convert.ChangeType(dr.GetValue(i), propertyInfo.PropertyType));
                        }
                    }
                }
                dr.Close();
            }
            objEst.CmdBD.Parameters.Clear();//Limpiar los parametros
            objEst.CerrarConexion();//cerrar la conexion
            return oUsuario;
        }
    }
}