
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Zamba.AppBlock;
using Zamba.Tools;

public class ZOptBusiness
{
    #region Private Properties
    /*
     * [Claudia - 27/11/19]En esta lista, están todas las key que deben ser desencriptadas al momento de leerlas de la BD.
     * Y como se utilizan en el método GetValueOrDefault, también se van a encriptar.
    */
    private static readonly string _wordConventionEncryption = "Encrypted";
    private static readonly byte[] _key = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6 };
    private static readonly byte[] _iv = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6 };
    #endregion

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
            string _propertyName = GetSpecificString(key);
            //verifico que _propertyName contenga la _wordConventionEncryption
            if (string.IsNullOrEmpty(_propertyName)) 
            {
                Insert(key, DefaultValue);
                Value = DefaultValue;
            }
            else
            {
                //OBTENGO EL VIEJO VALOR, Y LO GUARDO CON UN NUEVO NOMBRE Y EL VALOR ENCRIPTADO
                string _value = ZOptFactory.GetValue(_propertyName);
                string _encrypValue = Encryption.EncryptString(_value, _key, _iv);
                Insert(key, _encrypValue);
                Value = _encrypValue;
            }
        }
        if (!string.IsNullOrEmpty(Value) && !string.IsNullOrEmpty(GetSpecificString(key)))
        {
            //desencripto
            Value = Encryption.DecryptString(Value, _key, _iv);
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

    #region Private Methods
    private static string GetSpecificString(string stringToSplit) 
    {
        try
        {
            if (stringToSplit.Contains(_wordConventionEncryption))
            {
                return stringToSplit.Substring(0, (stringToSplit.Length - _wordConventionEncryption.Length));
            }
            else
            {
                return string.Empty;
            }
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
    #endregion

}