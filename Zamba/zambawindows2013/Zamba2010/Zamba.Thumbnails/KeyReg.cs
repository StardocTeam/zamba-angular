using System;
using System.Windows.Forms;

namespace Zamba
{
    namespace Thumbnails
    {

        /// <summary>
        /// Clave usada para indexar 
        /// datos en un ListaImagen
        /// </summary>
        public class KeyReg : IComparable
        {
            protected string    key;
            protected object    dato;
            protected string    text;
            protected  string   path;

            public string Key { get { return this.key; } }
            public object Dato
            {
                get
                {
                    if (null == dato)
                        return key;

                    return dato;
                }
            }
            public string Text
            {
                get
                {
                    if (null == text)
                        return key;

                    return text;
                }
            }

            public string Path
            {
                get
                {
                    if (null == path)
                        return key;

                    return path;
                }
            }

            public KeyReg(string key,
                            object dato,
                            string text,
                            string path)
            {
                this.key  = key;
                this.dato = dato;
                this.text = text;
                this.path = path;
            }

            public object Clone()
            {
                return new KeyReg(this.Key,
                                     this.Dato,
                                     this.Text,this.path);
            }

            public override bool Equals(object obj)
            {
                if (0 == this.key.CompareTo(((KeyReg)obj).key))
                    return true;

                return false;
            }


            public override int GetHashCode()
            {
                return base.GetHashCode();
            }


            public int CompareTo(object obj)
            {
                return this.key.CompareTo(((KeyReg)obj).key);
            }
        }

    }
}
