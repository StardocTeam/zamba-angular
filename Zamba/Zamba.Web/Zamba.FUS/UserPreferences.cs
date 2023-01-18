using System;
using System.Data;
using Zamba;
using Zamba.Servers;

//public partial class UserPreferences
//{
    

    ///agrega un item por el nombre y le asigna el valor
    //public static void setValue(string name, string valor, UPSections Section)
    //{
    //    try
    //    {
    //        //Si no hay un usuario logueado, busco en la tabla de PC's
    //        if (Zamba.Membership.MembershipHelper.CurrentUser == null)
    //        {
    //            UserPreferencesFactory.setValueDBByMachine(name, valor, Section, Environment.MachineName);
    //        }
    //        else
    //        {
    //            UserPreferencesFactory.setValueDB(name, valor, Section, Zamba.Membership.MembershipHelper.CurrentUser.ID);
    //        }
    //        if ((Zamba.Core.Cache.UsersAndGroups.hsUserPreferences.ContainsKey(name)))
    //        {
    //            Zamba.Core.Cache.UsersAndGroups.hsUserPreferences[name] = valor;
    //        }
    //        else
    //        {
    //            Zamba.Core.Cache.UsersAndGroups.hsUserPreferences.Add(name, valor);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Zamba.AppBlock.ZException.Log(ex);
    //    }
    //}

    ///agrega un item por el nombre y le asigna el valor
    //public static void setValueForMachine(string name, string valor, UPSections Section)
    //{
    //    setValueForMachine(name, valor, Section, Environment.MachineName);
    //}

    //public static void setValueForMachine(string name, string valor, UPSections Section, string MachineName)
    //{
    //    try
    //    {
    //        UserPreferencesFactory.setValueDBByMachine(name, valor, Section, MachineName);
    //        if ((Zamba.Core.Cache.UsersAndGroups.hsUserPreferences.ContainsKey(name)))
    //        {
    //            Zamba.Core.Cache.UsersAndGroups.hsUserPreferences[name] = valor;
    //        }
    //        else
    //        {
    //            Zamba.Core.Cache.UsersAndGroups.hsUserPreferences.Add(name, valor);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Zamba.AppBlock.ZException.Log(ex);
    //    }
    //}

    ///// agrega un item por el nombre y le asigna el valor para un userId especificado.
    //public static void setValueForUsersId(string name, string value, UPSections section, long userId)
    //{
    //    UserPreferencesFactory.setValueDB(name, value, section, userId);
    //}


    //public static void LoadAllMachineConfigValues()
    //{
    //    DataTable values = null;
    //    try
    //    {
    //        values = UserPreferencesFactory.getAllValuesDBByMachine(Environment.MachineName);
    //        lock (Zamba.Core.Cache.UsersAndGroups.hsUserPreferences)
    //        {
    //            foreach (DataRow Value in values.Rows)
    //            {
    //                if (Zamba.Core.Cache.UsersAndGroups.hsUserPreferences.ContainsKey(Value["name"]) == false)
    //                    Zamba.Core.Cache.UsersAndGroups.hsUserPreferences.Add(Value["name"], Value["value"]);
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Zamba.AppBlock.ZException.Log(new Exception("Error al cargar las preferencias de maquina" + ex.ToString()));
    //    }
    //    finally
    //    {
    //        if (values != null)
    //        {
    //            values.Dispose();
    //            values = null;
    //        }
    //    }
    //}
    //public static void LoadAllUserConfigValues()
    //{
    //    if (Zamba.Membership.MembershipHelper.CurrentUser != null)
    //    {
    //        DataTable values = null;
    //        try
    //        {
    //            values = UserPreferencesFactory.getAllValuesDB(Zamba.Membership.MembershipHelper.CurrentUser.ID);
    //            lock (Zamba.Core.Cache.UsersAndGroups.hsUserPreferences)
    //            {
    //                foreach (DataRow Value in values.Rows)
    //                {
    //                    if (Zamba.Core.Cache.UsersAndGroups.hsUserPreferences.ContainsKey(Value["name"]) == false)
    //                        Zamba.Core.Cache.UsersAndGroups.hsUserPreferences.Add(Value["name"], Value["value"]);
    //                    else
    //                        Zamba.Core.Cache.UsersAndGroups.hsUserPreferences[Value["name"]] = Value["value"];
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Zamba.AppBlock.ZException.Log(new Exception("Error al cargar las preferencias del usuario" + ex.ToString()));
    //        }
    //        finally
    //        {
    //            if (values != null)
    //            {
    //                values.Dispose();
    //                values = null;
    //            }
    //        }
    //    }
    //}

    //public static void RemoveValueByUsersID(string userID, string settingName)
    //{
    //    UserPreferencesFactory.DeleteUserSetting(userID, settingName);
    //}
//}