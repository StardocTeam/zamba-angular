using System;
using System.Collections.Generic;
using System.Text;

public class Usuario
{
    private string _nombre;

    public string Nombre
    {
        get
        {
            return _nombre;
        }
        set
        {
            _nombre = value;
        }
    }

    public Usuario(string nombre)
    {
        _nombre = nombre;
    }
}