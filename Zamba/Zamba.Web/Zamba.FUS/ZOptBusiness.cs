
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Zamba.AppBlock;

public class ZOptBusiness
{
    public static void Insert(string key, string value)
    {
        if (Zamba.Core.Cache.UsersAndGroups.hsOptions.ContainsKey(key) == false)
        {
            Zamba.Core.Cache.UsersAndGroups.hsOptions.Add(key, value);
        }
        else
        {
            Zamba.Core.Cache.UsersAndGroups.hsOptions[key] = value;
        }
        ZOptFactory.Insert(key, value);
    }

    public static void Update(string key, string value)
    {
        if (Zamba.Core.Cache.UsersAndGroups.hsOptions.ContainsKey(key))
        {
            Zamba.Core.Cache.UsersAndGroups.hsOptions[key] = value;
        }
        ZOptFactory.Update(key, value);
    }

    public static string GetValue(string key)
    {
        if (Zamba.Core.Cache.UsersAndGroups.hsOptions.ContainsKey(key))
        {
            if (Zamba.Core.Cache.UsersAndGroups.hsOptions[key] != null)
                return Zamba.Core.Cache.UsersAndGroups.hsOptions[key].ToString();
            else
                return string.Empty;
        }
        string Value = ZOptFactory.GetValue(key);
        if (Value == null)
        {
            return string.Empty;
        }

        if (Zamba.Core.Cache.UsersAndGroups.hsOptions.ContainsKey(key) == false)
            Zamba.Core.Cache.UsersAndGroups.hsOptions.Add(key, Value);
        return Value;
    }

    public static string GetValueOrDefault(string key, string DefaultValue)
    {
        if (Zamba.Core.Cache.UsersAndGroups.hsOptions.ContainsKey(key))
        {
            if (Zamba.Core.Cache.UsersAndGroups.hsOptions[key] != null)
                return Zamba.Core.Cache.UsersAndGroups.hsOptions[key].ToString();
            else
                return string.Empty;
        }
        string Value = ZOptFactory.GetValue(key);
        if (Value == null || Value.Length == 0)
        {
            Insert(key, DefaultValue);
            Value = DefaultValue;
        }
        if (Zamba.Core.Cache.UsersAndGroups.hsOptions.ContainsKey(key) == false)
            Zamba.Core.Cache.UsersAndGroups.hsOptions.Add(key, Value);
        return Value;
    }

    public static void GetAllValues()
    {

        DataSet values = null;
        try
        {
            values = ZOptFactory.GetValues();
            lock (Zamba.Core.Cache.UsersAndGroups.hsOptions)
            {
                foreach (DataRow Value in values.Tables[0].Rows)
                {
                    if (Zamba.Core.Cache.UsersAndGroups.hsOptions.ContainsKey(Value[1]) == false)
                        Zamba.Core.Cache.UsersAndGroups.hsOptions.Add(Value[1], Value[0]);
                }
            }
        }
        catch (Exception ex)
        {
            ZException.Log(new Exception("Error al cargar las preferencias de maquina" + ex.ToString()));
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

    public static void InsertUpdateValue(string key, string value)
    {
        if (Zamba.Core.Cache.UsersAndGroups.hsOptions.ContainsKey(key) == false)
        {
            Zamba.Core.Cache.UsersAndGroups.hsOptions.Add(key, value);
        }
        else
        {
            Zamba.Core.Cache.UsersAndGroups.hsOptions[key] = value;
        }
        ZOptFactory.InsertUpdateValue(key, value);
    }

}