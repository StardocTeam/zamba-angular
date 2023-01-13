using Zamba.Servers;
using System.IO;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Threading;

namespace TestSPBoston
{
    /// <summary>
    /// Test que sirve para hacer un ABM (Alta - Baja - Modificaciòn) de usuarios
    /// </summary>
    [TestClass]
    public class ABM_USer
    {
        static IConnection  _con;

        #region Constants
        public const string SP_PA_USUARIOADD = "pa_UsuarioAdd";
        public const string SP_PA_USUARIOUPD = "pa_UsuarioUpd";
        public const string SP_PA_USUARIODEL = "pa_UsuarioDel";
        public const string ERROR_ID_USUARIO_MAYOR_A_CERO = "El id de usuario generado no es mayor a cero";
        public const string ERROR_COUNT_CERO = "La cantidad de registros modificadas debe ser mayor a cero";
        public const string ERROR_INSTANCIAS_NO_CONCUERDAN_ENTIDAD_ACTUAL = "Las instancias no concuerdan.\n entidad actual: ";
        public const string ERROR_INSTANCIAS_NO_CONCUERDAN_ENTIDAD_ESPERADA = "\n Entidad Esperada: ";
        public const string QUERY_FROM_USRTABLE = "select id, name, nombres,apellido,correo from usrtable where id = ";
        public const string QUERY_FROM_USRDATA = "select CoLegajo, CoNivelAutorizacion, NuAgencia from usrdata where UserId = ";
        public const string QUERY_GETLASTLEGAJO = "SELECT max(Colegajo) FROM UsrData";
        public const string QUERY_SELECT_FROM_ZMAILCONFIG = "Select Correo, Username from zmailconfig where userid = ";
        public const string QUERY_VERIFYCORRECTDELETE_USRDATA_SELECT_COLEGAJO_EQUALS = "Select * from usrdata where colegajo = ";
        public const string QUERY_VERIFYCORRECTDELETE_ZMAILCONFIG_SELECT_COLEGAJO_EQUALS = "Select * from zmailconfig where userid = ";
        public const string QUERY_VERIFYCORRECTDELETE_USRTABLE_SELECT_ID_EQUALS = "Select * from usrtable where id = ";
        public const string ERROR_SIN_ID_RETORNO = "el stored procedure no retorno id";
        public const string ERROR_GETPA_USUARIOADD_DUPLICADOS_USRTABLE = "La tabla usrtable posee registros duplicados";
        public const string ERROR_GETPA_USUARIOADD_NO_EXISTE_REGISTRO_USRTABLE = "Debe existir al menos un registro en la tabla usrtable";
        public const string ERROR_GETPA_USUARIOADD_DUPLICADOS_USRDATA = "La tabla usrdata posee registros duplicados";
        public const string ERROR_GETPA_USUARIOADD_NO_EXISTE_REGISTRO_USRDATA = "Debe existir al menos un registro en la tabla usrdata";
        public const string ERROR_MAS_DE_UN_REGISTRO_EN_ZMAILCONFIG = "Hay mas de un registro en ZMailConfig";
        public const string ERROR_SIN_REGISTROS_ZMAILCONFIG = "No hay registros en ZMailConfig";
        public const string ERROR_EL_REGISTRO_TODAVIA_EXISTE_USRDATA = "El registro todavia existe en UsrData";

        public const string ERROR_INICIALIZAR_TEST = "Error al inicializar el test. El arhivo no existe el arhivo";
        
        public const string COL_NAME = "name";
        public const string COL_NOMBRES = "nombres";
        public const string COL_APELLIDO = "apellido";
        public const string COL_CORREO = "correo";
        public const string COL_USERNAME = "username";
        public const string COL_COLEGAJO = "CoLegajo";
        public const string COL_NIVELAUTORIZACION = "CoNivelAutorizacion";
        public const string COL_AGENCIA = "NuAgencia";
        #endregion
        /// <summary>
        /// Para correr las pruebas de integracion se debe utilizar zambatst.
        /// </summary>
        public const bool DESPLIEGUE_ZAMBATST = false;


        #region ctor
        public ABM_USer()
        {

        }
        #endregion


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes       
      
        //
        // Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            _con.dispose();
        }

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            string filename = Path.Combine(@"D:\Zamba2008\compilado", "App.ini");

            if (File.Exists(filename))
            {
                File.Copy(filename, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App.ini"),true);
            }
            else
                Assert.Fail(ERROR_INICIALIZAR_TEST + filename);

            if (DESPLIEGUE_ZAMBATST)
                _con = Server.get_Con(Server.DBTYPES.MSSQLServer7Up, "StardocZ", "Zambatst", "sa", "doc", true, false);
            else
                _con = Server.get_Con(Server.DBTYPES.MSSQLServer7Up, "BueSrvBpm01", "dtbBPM", "sa", "doc", true, false);
        }

        #endregion

        /// <summary>
        /// Agrega un usuario en la tabla usrtable 
        /// y sus datos en la tabla usrdata.
        /// </summary>
        [TestMethod]
        public void pa_UsuarioABMTest()
        {
            //Insert
            pa_UsuarioAddUser user1 = CreateUser();
            pa_UsuarioAddTest(user1);
            pa_UsuarioAddUser user2 = CreateUser();
            pa_UsuarioAddTest(user2);
            pa_UsuarioAddUser user3 = CreateUser();
            pa_UsuarioAddTest(user3);

            //Update
            user1.firstname = "prueba udp";
            user2.firstname = "prueba udp2";
            user3.firstname = "prueba udp3";

            pa_UsuarioUpdTest(user1);
            pa_UsuarioUpdTest(user2);
            pa_UsuarioUpdTest(user3);


            //Delete
            pa_UsuarioDelTest(user1);
            pa_UsuarioDelTest(user2);
            pa_UsuarioDelTest(user3);



            //Ejecutar 3 veces este test

            //-------------------------------
            //Seleccion del usuario agregado

            //Modificacion
            //Baja

            //-------------------------------
        }

        #region Helper
        /// <summary>
        /// Crea una instancia pa_UsuarioAddUser para el stored pa_usuarioadd
        /// </summary>
        /// <returns></returns>
        private pa_UsuarioAddUser CreateUser()
        {
            pa_UsuarioAddUser user = new pa_UsuarioAddUser();

            //Se agrega un retardo para producir simpre una clave distinta si es que 
            //la pc es muy rapida
            Thread.Sleep(1000);
            user.uName = "User" + DateTime.Now.ToString("yyyyMMddhhmmssff");
            user.firstname = "Prueba Sp Boston12";
            user.lastName = "Prueba Sp Boston12";
            user.mail = "boston@boston.com.ar12";
            user.ZmailConfig_Correo = user.mail;
            user.ZmailConfig_UserName = user.mail;
            user.colegajo = GetLastColegajo();
            user.nuNivel = 13;
            user.nuAgencia = 13;
            return user;
        }
        #endregion

        #region Metodos Test
        /// <summary>
        /// Método que sirve para agregar un usuario
        /// </summary>
        private void pa_UsuarioAddTest(pa_UsuarioAddUser _pa_usuarioadduser)
        {
            //Obtiene una coleccion de parametros
            object[] parms = CreateParamsUsuarioAdd(_pa_usuarioadduser);

            decimal id = 0;

            try
            {
                //Ejecuta el stored procedure
                id = (decimal)_con.ExecuteScalar(SP_PA_USUARIOADD, parms);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            // Se evalua que el id sea mayor a cero
            int notexpectedId = 0;
            Assert.AreNotEqual(notexpectedId, id, ERROR_ID_USUARIO_MAYOR_A_CERO);

            //Se mapea el id al objeto usuario
            _pa_usuarioadduser.userID = id;

            VerifyEntities(_pa_usuarioadduser);
        }

        private void pa_UsuarioUpdTest(pa_UsuarioAddUser _pa_usuarioadduser)
        {
            //Obtiene una coleccion de parametros
            object[] parms = CreateParamsUsuarioUpd(_pa_usuarioadduser);

            int count = 0;

            try
            {
                //Ejecuta el stored procedure
                count = _con.ExecuteNonQuery(SP_PA_USUARIOUPD, parms);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            // Se evalua el registro modificado sea uno
            int expectedCount = 1;
            Assert.AreEqual(expectedCount, count, ERROR_COUNT_CERO);

            VerifyEntities(_pa_usuarioadduser);
        }

        private void pa_UsuarioDelTest(pa_UsuarioAddUser _pa_usuarioadduser)
        {
            //Obtiene una coleccion de parametros
            object[] parms = CreateParamsUsuarioDel(_pa_usuarioadduser);

            int count = 0;

            try
            {
                //Ejecuta el stored procedure
                count = _con.ExecuteNonQuery(SP_PA_USUARIODEL, parms);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            // Se evalua el registro modificado sea uno
            int expectedCount = 1;
            Assert.AreEqual(expectedCount, count, ERROR_COUNT_CERO);

            VerifyCorrectDelete(_pa_usuarioadduser);
        }
        /// <summary>
        /// Verifica la correcta eliminacion del registro
        /// </summary>
        /// <param name="_pa_usuarioadduser">pa_UsuarioAddUser</param>
        private void VerifyCorrectDelete(pa_UsuarioAddUser _pa_usuarioadduser)
        {
            IDataReader dr;
            dr = _con.ExecuteReader(CommandType.Text, QUERY_VERIFYCORRECTDELETE_USRDATA_SELECT_COLEGAJO_EQUALS + _pa_usuarioadduser.colegajo);
            using (dr)
            {
                if (dr.Read())
                    Assert.Fail(ERROR_EL_REGISTRO_TODAVIA_EXISTE_USRDATA);
            }
            dr = _con.ExecuteReader(CommandType.Text, QUERY_VERIFYCORRECTDELETE_USRTABLE_SELECT_ID_EQUALS + _pa_usuarioadduser.userID);
            using (dr)
            {
                if (dr.Read())
                    Assert.Fail(ERROR_EL_REGISTRO_TODAVIA_EXISTE_USRDATA);
            }
            dr = _con.ExecuteReader(CommandType.Text, QUERY_VERIFYCORRECTDELETE_ZMAILCONFIG_SELECT_COLEGAJO_EQUALS + _pa_usuarioadduser.userID);
            using (dr)
            {
                if (dr.Read())
                    Assert.Fail(ERROR_EL_REGISTRO_TODAVIA_EXISTE_USRDATA);
            }
        }

        #endregion

        #region Metodos Base

        /// <summary>
        /// Obtiene el ultimo colegajo
        /// </summary>
        /// <returns>ultimo colegajo</returns>
        private int GetLastColegajo()
        {
            object colegajo = _con.ExecuteScalar(CommandType.Text, QUERY_GETLASTLEGAJO);
            if (colegajo != DBNull.Value)
            {
                return Convert.ToInt32(colegajo) + 1;
            }
            else
            {
                return 1;
            }

        }


        /// <summary>
        /// Obtiene una coleccion de parametros para input del stored procedure
        /// </summary>
        /// <param name="_pa_usuarioadduser"></param>
        /// <returns>coleccion de parametros</returns>
        private static object[] CreateParamsUsuarioAdd(pa_UsuarioAddUser _pa_usuarioadduser)
        {
            object[] parms = {_pa_usuarioadduser.uName, _pa_usuarioadduser.firstname, 
                                _pa_usuarioadduser.lastName, _pa_usuarioadduser.mail, 
                                _pa_usuarioadduser.colegajo, _pa_usuarioadduser.nuNivel, 
                                _pa_usuarioadduser.nuAgencia };
            return parms;
        }

        /// <summary>
        /// Obtiene una coleccion de parametros para input del stored procedure
        /// </summary>
        /// <param name="_pa_usuarioadduser"></param>
        /// <returns>coleccion de parametros</returns>
        private static object[] CreateParamsUsuarioUpd(pa_UsuarioAddUser _pa_usuarioadduser)
        {
            object[] parms = {  _pa_usuarioadduser.firstname, 
                                _pa_usuarioadduser.lastName, _pa_usuarioadduser.mail, 
                                _pa_usuarioadduser.colegajo, _pa_usuarioadduser.nuNivel, 
                                _pa_usuarioadduser.nuAgencia };
            return parms;
        }


        /// <summary>
        /// Obtiene una coleccion de parametros para input del stored procedure
        /// </summary>
        /// <param name="_pa_usuarioadduser"></param>
        /// <returns>coleccion de parametros</returns>
        private static object[] CreateParamsUsuarioDel(pa_UsuarioAddUser _pa_usuarioadduser)
        {
            object[] parms = { _pa_usuarioadduser.colegajo };
            return parms;
        }


        /// <summary>
        /// Verifica que la instancia de la entidad usuario sean iguales con respecto a la existente en base de datos
        /// </summary>
        /// <param name="_pa_usuarioadduser">pa_UsuarioAddUser</param>
        private void VerifyEntities(pa_UsuarioAddUser _pa_usuarioadduser)
        {
            pa_UsuarioAddUser expectedUser = buildEntity(_pa_usuarioadduser.userID);

            // Se evalua que las instancias sean iguales
            Assert.AreEqual(expectedUser, _pa_usuarioadduser, ERROR_INSTANCIAS_NO_CONCUERDAN_ENTIDAD_ACTUAL +
                                                                                    _pa_usuarioadduser.ToString() +
                                                                                    ERROR_INSTANCIAS_NO_CONCUERDAN_ENTIDAD_ESPERADA +
                                                                                    expectedUser.ToString());
        }

        /// <summary>
        /// Método que sirve para construir la entidad 
        /// </summary>
        /// <returns>pa_UsuarioAddUser</returns>
        private pa_UsuarioAddUser buildEntity(decimal id)
        {
            // Se crea una instancia de tipo pa_UsuarioABMTest que se va a comparar con la instancia original
            pa_UsuarioAddUser expectedUser = new pa_UsuarioAddUser();

            try
            {
                getPA_UsuarioAdd(id, expectedUser);
                return (expectedUser);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
                return (expectedUser);
            }
        }

        /// <summary>
        /// Método que sirve para ejecutar las consultas necesarias para obtener los datos que va a tener la instancia
        /// </summary>
        /// <param name="id">Id de usuario</param>
        /// <param name="expectedUser">Instancia en donde se van a colocar los datos</param>
        private void getPA_UsuarioAdd(decimal id, pa_UsuarioAddUser expectedUser)
        {
            expectedUser.userID = id;
            using (IDataReader reader = _con.ExecuteReader(CommandType.Text, QUERY_FROM_USRTABLE + id))
            {
                if (reader.Read())
                {
                    // Se colocan datos sobre la instancia
                    expectedUser.uName = reader[COL_NAME].ToString();
                    expectedUser.firstname = reader[COL_NOMBRES].ToString();
                    expectedUser.lastName = reader[COL_APELLIDO].ToString();
                    expectedUser.mail = reader[COL_CORREO].ToString();
                    if (reader.Read())
                    {
                        Assert.Fail(ERROR_GETPA_USUARIOADD_DUPLICADOS_USRTABLE);
                    }
                }
                else
                {
                    Assert.Fail(ERROR_GETPA_USUARIOADD_NO_EXISTE_REGISTRO_USRTABLE);
                }
            }

            using (IDataReader reader = _con.ExecuteReader(CommandType.Text, QUERY_FROM_USRDATA + id))
            {
                if (reader.Read())
                {
                    // Se colocan los restantes datos en la instancia
                    expectedUser.colegajo = Convert.ToInt32(reader[COL_COLEGAJO]);
                    expectedUser.nuNivel = Convert.ToInt16(reader[COL_NIVELAUTORIZACION]);
                    expectedUser.nuAgencia = Convert.ToInt16(reader[COL_AGENCIA]);

                    if (reader.Read())
                    {
                        Assert.Fail(ERROR_GETPA_USUARIOADD_DUPLICADOS_USRDATA);
                    }
                }
                else
                {
                    Assert.Fail(ERROR_GETPA_USUARIOADD_NO_EXISTE_REGISTRO_USRDATA);
                }
            }
            using (IDataReader reader = _con.ExecuteReader(CommandType.Text, QUERY_SELECT_FROM_ZMAILCONFIG + id))
            {
                if (reader.Read())
                {
                    // Se colocan los restantes datos en la instancia
                    expectedUser.ZmailConfig_Correo = reader[COL_CORREO].ToString();
                    expectedUser.ZmailConfig_UserName = reader[COL_USERNAME].ToString();

                    if (reader.Read())
                    {
                        Assert.Fail(ERROR_MAS_DE_UN_REGISTRO_EN_ZMAILCONFIG);
                    }
                }
                else
                {
                    Assert.Fail(ERROR_SIN_REGISTROS_ZMAILCONFIG);
                }
            }
        }
        #endregion
    }

    #region entities
    /// <summary>
    /// Clase Usuario Utilizada como transporte en el stored pa_UsuarioAdd
    /// </summary>
    class pa_UsuarioAddUser
    {
        #region fields

        public string uName;
        public string firstname;
        public string lastName;
        public string ZmailConfig_Correo;
        public string ZmailConfig_UserName;
        public string mail;
        public int colegajo;
        public short nuNivel;
        public short nuAgencia;
        public decimal userID;
        #endregion

        #region ctor
        public pa_UsuarioAddUser() { }

        public pa_UsuarioAddUser(string _uName, string _firstname, string _lastName,
                                    string _mail, int _colegajo, short _nuNivel, short _nuAgencia)
        {
            uName = _uName;
            firstname = _firstname;
            lastName = _lastName;
            mail = _mail;
            colegajo = _colegajo;
            nuNivel = _nuNivel;
            nuAgencia = _nuAgencia;
        }


        public pa_UsuarioAddUser(string _uName, string _firstname, string _lastName,
                                    string _mail, int _colegajo, short _nuNivel,
                                    short _nuAgencia, decimal _UserID)
            : this(_uName, _firstname, _lastName,
                    _mail, _colegajo, _nuNivel,
                    _nuAgencia)
        {
            userID = _UserID;
        }
        #endregion

        #region methods
        public override string ToString()
        {
            StringBuilder message = new StringBuilder();
            message.Append("userID: " + userID.ToString());
            message.AppendLine(" UName: " + uName.ToString());
            message.AppendLine(" firstName: " + firstname);
            message.AppendLine(" lastName: " + lastName);
            message.AppendLine(" mail: " + mail);
            message.AppendLine(" ZMailConfig_Correo: " + ZmailConfig_Correo);
            message.AppendLine(" ZMailConfig_UserName: " + ZmailConfig_UserName);
            message.AppendLine(" colegajo: " + colegajo);
            message.AppendLine(" nuNivel: " + nuNivel);
            message.AppendLine(" nuAgencia: " + nuAgencia);

            return (message.ToString());
        }

        public override bool Equals(object obj)
        {
            pa_UsuarioAddUser other = (pa_UsuarioAddUser)obj;
            if (
                this.userID == other.userID &&
                this.uName == other.uName &&
                this.firstname == other.firstname &&
                this.lastName == other.lastName &&
                this.mail == other.mail &&
                this.ZmailConfig_Correo == other.ZmailConfig_Correo &&
                this.ZmailConfig_UserName == other.ZmailConfig_UserName &&
                this.colegajo == other.colegajo &&
                this.nuNivel == other.nuNivel &&
                this.nuAgencia == other.nuAgencia
                )
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion

    }
    #endregion
}
