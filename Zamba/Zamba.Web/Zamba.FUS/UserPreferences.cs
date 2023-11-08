using System;
using System.Data;
using Zamba;
using Zamba.Servers;

public partial class UserPreferences
{
    public enum InitialSizes
    {
        Normal,
        Height,
        Width
    }

    ///Devuelve el valor de un item por el nombre
    public  string getValue(string name, UPSections Section, object DefaultValue)
    {
        try
        {
            if (Server.ConInitialized || Server.ConInitializing)
            {
                if (Zamba.Core.Cache.UsersAndGroups.hsUserPreferences.Contains(name))
                {
                    if (Zamba.Core.Cache.UsersAndGroups.hsUserPreferences[name] != null)
                        return Zamba.Core.Cache.UsersAndGroups.hsUserPreferences[name].ToString();
                    else
                        return string.Empty;
                }
                else
                {
                    string valor = null;

                    //Si no hay un usuario logueado, busco en la tabla de PC's zmachineconfig
                    if (Zamba.Membership.MembershipHelper.CurrentUser == null | name.ToLower() == "userid")
                    {
                        //busco el valor especifico de la PC
                        valor = UserPreferencesFactory.getValueDBByMachine(name, Section, Environment.MachineName);

                        if (valor == null)
                        {
                            //si no hay valor especifico de la PC busco el valor por default para las PC
                            valor = UserPreferencesFactory.getValueDBByMachine(name, Section, "default");

                            //Si no esta en la zmachineconfig el valor por defecto de las PC
                            if (valor == null)
                            {
                                //Guardo el valor por defecto del codigo en el valor por defecto de las PC
                                UserPreferencesFactory.setValueDBByMachine(name, DefaultValue.ToString(), Section, "default");
                                valor = DefaultValue.ToString();
                            }
                        }
                    }
                    else
                    {
                        //Hay usuario logueado, entonces vamos a usar la zuserconfig

                        //busco el valor especifico del usuario
                        valor = UserPreferencesFactory.getValueDB(name, Section, Zamba.Membership.MembershipHelper.CurrentUser.ID);

                        if (valor == null)
                        {
                            //Si no hay valor especifico del usuario  busco el valor por defecto Usuario 0
                            valor = UserPreferencesFactory.GetDefaultValueDB(name, Section);

                            //Si no tiene un valor por defecto de usuario 0 en la base
                            if (valor == null)
                            {
                                //guardo el valor por defecto del codigo en el usuario por defecto 0
                                UserPreferencesFactory.setValueDB(name, DefaultValue.ToString(), Section, 0);
                                valor = DefaultValue.ToString();
                            }
                        }
                    }

                    if (!Zamba.Core.Cache.UsersAndGroups.hsUserPreferences.ContainsKey(name))
                    {
                        Zamba.Core.Cache.UsersAndGroups.hsUserPreferences.Add(name, valor);
                    }

                    return valor;
                }
            }
            else
            {
                return DefaultValue.ToString();
            }
        }
        catch (System.Threading.ThreadAbortException)
        {
            return DefaultValue.ToString();
        }
        catch (ZConnectionException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(new Exception("Error al buscar el valor de " + name + " " + ex.ToString()));
            return DefaultValue.ToString();
        }
    }


    ///Devuelve el valor de un item por el nombre
    public static string getValueForMachine(string name, UPSections Section, object DefaultValue, bool UseCache = true)
    {
        return getValueForMachine(name, Section, DefaultValue, Environment.MachineName, UseCache);
    }

    public static string getValueForMachine(string name, UPSections Section, object DefaultValue, string machineName, bool UseCache = true)
    {
        try
        {
            if (Zamba.Core.Cache.UsersAndGroups.hsUserPreferences.Contains(name) && UseCache)
            {
                if (Zamba.Core.Cache.UsersAndGroups.hsUserPreferences[name] != null)
                    return Zamba.Core.Cache.UsersAndGroups.hsUserPreferences[name].ToString();
                else
                    return string.Empty;
            }
            else
            {
                string valor = UserPreferencesFactory.getValueDBByMachine(name, Section, machineName);

                if (valor == null)
                {
                    //si no hay valor especifico de la PC busco el valor por default para las PC
                    valor = UserPreferencesFactory.getValueDBByMachine(name, Section, "default");

                    //Si no esta en la zmachineconfig el valor por defecto de las PC
                    if (valor == null)
                    {
                        //Guardo el valor por defecto del codigo en el valor por defecto de las PC
                        UserPreferencesFactory.setValueDBByMachine(name, DefaultValue.ToString(), Section, "default");
                        valor = DefaultValue.ToString();
                    }
                }

                if (!Zamba.Core.Cache.UsersAndGroups.hsUserPreferences.ContainsKey(name))
                {

                    Zamba.Core.Cache.UsersAndGroups.hsUserPreferences.Add(name, valor);
                }
                else if (UseCache == false)
                    Zamba.Core.Cache.UsersAndGroups.hsUserPreferences[name] = valor;


                return valor;
            }
        }
        catch (System.Threading.ThreadAbortException)
        {
            return DefaultValue.ToString();
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(new Exception("Error al buscar el valor de " + name + " " + ex.ToString()));
            return DefaultValue.ToString();
        }
    }

    ///agrega un item por el nombre y le asigna el valor
    public static void setValue(string name, string valor, UPSections Section)
    {
        try
        {
            //Si no hay un usuario logueado, busco en la tabla de PC's
            if (Zamba.Membership.MembershipHelper.CurrentUser == null)
            {
                UserPreferencesFactory.setValueDBByMachine(name, valor, Section, Environment.MachineName);
            }
            else
            {
                UserPreferencesFactory.setValueDB(name, valor, Section, Zamba.Membership.MembershipHelper.CurrentUser.ID);
            }
            if ((Zamba.Core.Cache.UsersAndGroups.hsUserPreferences.ContainsKey(name)))
            {
                Zamba.Core.Cache.UsersAndGroups.hsUserPreferences[name] = valor;
            }
            else
            {
                Zamba.Core.Cache.UsersAndGroups.hsUserPreferences.Add(name, valor);
            }
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }

    ///agrega un item por el nombre y le asigna el valor
    public static void setValueForMachine(string name, string valor, UPSections Section)
    {
        setValueForMachine(name, valor, Section, Environment.MachineName);
    }

    public static void setValueForMachine(string name, string valor, UPSections Section, string MachineName)
    {
        try
        {
            UserPreferencesFactory.setValueDBByMachine(name, valor, Section, MachineName);
            if ((Zamba.Core.Cache.UsersAndGroups.hsUserPreferences.ContainsKey(name)))
            {
                Zamba.Core.Cache.UsersAndGroups.hsUserPreferences[name] = valor;
            }
            else
            {
                Zamba.Core.Cache.UsersAndGroups.hsUserPreferences.Add(name, valor);
            }
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }

    /// agrega un item por el nombre y le asigna el valor para un userId especificado.
    public static void setValueForUsersId(string name, string value, UPSections section, long userId)
    {
        UserPreferencesFactory.setValueDB(name, value, section, userId);
    }


    public static void LoadAllMachineConfigValues()
    {
        DataTable values = null;
        try
        {
            values = UserPreferencesFactory.getAllValuesDBByMachine(Environment.MachineName);
            lock (Zamba.Core.Cache.UsersAndGroups.hsUserPreferences)
            {
                foreach (DataRow Value in values.Rows)
                {
                    if (Zamba.Core.Cache.UsersAndGroups.hsUserPreferences.ContainsKey(Value["name"]) == false)
                        Zamba.Core.Cache.UsersAndGroups.hsUserPreferences.Add(Value["name"], Value["value"]);
                }
            }
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(new Exception("Error al cargar las preferencias de maquina" + ex.ToString()));
        }
        finally
        {
            if (values != null)
            {
                values.Dispose();
                values = null;
            }
        }
    }
    public static void LoadAllUserConfigValues()
    {
        if (Zamba.Membership.MembershipHelper.CurrentUser != null)
        {
            DataTable values = null;
            try
            {
                values = UserPreferencesFactory.getAllValuesDB(Zamba.Membership.MembershipHelper.CurrentUser.ID);
                lock (Zamba.Core.Cache.UsersAndGroups.hsUserPreferences)
                {
                    foreach (DataRow Value in values.Rows)
                    {
                        if (Zamba.Core.Cache.UsersAndGroups.hsUserPreferences.ContainsKey(Value["name"]) == false)
                            Zamba.Core.Cache.UsersAndGroups.hsUserPreferences.Add(Value["name"], Value["value"]);
                        else
                            Zamba.Core.Cache.UsersAndGroups.hsUserPreferences[Value["name"]] = Value["value"];
                    }
                }
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(new Exception("Error al cargar las preferencias del usuario" + ex.ToString()));
            }
            finally
            {
                if (values != null)
                {
                    values.Dispose();
                    values = null;
                }
            }
        }
    }

    public static void RemoveValueByUsersID(string userID, string settingName)
    {
        UserPreferencesFactory.DeleteUserSetting(userID, settingName);
    }
}