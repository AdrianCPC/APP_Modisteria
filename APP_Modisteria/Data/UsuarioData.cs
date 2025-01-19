
using APP_Modisteria.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection; // Para usar Reflection

namespace APP_Modisteria.Data
{
    public class UsuarioData
    {
        public static bool registrarUsuario(Usuario oUsuario)
        {
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_RegistrarUsuario @nombreUsuario, @contrasena, @tipoUsuario, @idUsuarioOut OUTPUT";

            SqlParameter[] parametros = new SqlParameter[4]
            {
                new SqlParameter("@nombreUsuario", oUsuario.nombreUsuario),
                new SqlParameter("@contrasena", oUsuario.contrasena),
                new SqlParameter("@tipoUsuario", oUsuario.tipoUsuario),
                new SqlParameter("@idUsuarioOut", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.Output }
            };

            bool resultado = objEst.EjecutarSentencia(sentencia, parametros);
            if (resultado)
            {
                oUsuario.IdUsuario = Convert.ToInt32(parametros[3].Value); // Obtener el ID de salida
            }
            return resultado;
        }

        public static bool actualizarUsuario(Usuario oUsuario)
        {
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_ActualizarUsuario @idUsuario, @nombreUsuario, @contrasena, @tipoUsuario";

            SqlParameter[] parametros = new SqlParameter[4]
            {
                new SqlParameter("@idUsuario", oUsuario.IdUsuario),
                new SqlParameter("@nombreUsuario", oUsuario.nombreUsuario),
                new SqlParameter("@contrasena", oUsuario.contrasena),
                new SqlParameter("@tipoUsuario", oUsuario.tipoUsuario)
            };

            return objEst.EjecutarSentencia(sentencia, parametros);
        }

        public static bool eliminarUsuario(int id)
        {
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_EliminarUsuario @idUsuario";
            SqlParameter parametro = new SqlParameter("@idUsuario", id);

            return objEst.EjecutarSentencia(sentencia, parametro);
        }

        public static List<Usuario> ListarUsuarios()
        {
            List<Usuario> oListaUsuario = new List<Usuario>();
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_ListarUsuarios";

            if (objEst.Consultar(sentencia, false))
            {
                SqlDataReader dr = objEst.Reader;
                while (dr.Read())
                {
                    Usuario usuario = new Usuario();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string propertyName = dr.GetName(i);
                        // Asegurarse que la propiedad exista en el modelo Usuario
                        PropertyInfo propertyInfo = usuario.GetType().GetProperty(propertyName);
                        if (propertyInfo != null && !Convert.IsDBNull(dr.GetValue(i)))
                        {
                            propertyInfo.SetValue(usuario, Convert.ChangeType(dr.GetValue(i), propertyInfo.PropertyType));
                        }
                    }
                    oListaUsuario.Add(usuario);
                }
                dr.Close(); // Cerrar el lector es importante
            }
            return oListaUsuario;
        }

        public static Usuario ObtenerUsuario(int id)
        {
            Usuario oUsuario = null;
            ConexionBD objEst = new ConexionBD();
            string sentencia = "EXEC usp_ConsultarUsuarioPorId @idUsuario";
            SqlParameter parametro = new SqlParameter("@idUsuario", id);

            if (objEst.Consultar(sentencia, parametro))
            {
                SqlDataReader dr = objEst.Reader;
                if (dr.Read()) // Solo un registro se espera
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
            return oUsuario;
        }
    }
}