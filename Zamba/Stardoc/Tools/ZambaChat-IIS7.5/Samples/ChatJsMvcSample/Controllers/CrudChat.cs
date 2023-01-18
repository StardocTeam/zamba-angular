using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
//using ChatJs.Net;// quitar
using ChatJsMvcSample.Code;
using ChatJsMvcSample.Models; //Colocar
using System.Management;
using System.Linq;


namespace ChatJsMvcSample.Controllers
{

    public class CrudChat //: Controller
    {
        public CrudChat()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.Chat");
            }
            db = new ChatEntities(Zamba.Servers.Server.get_Con().CN.ConnectionString);
        }


        public static ChatEntities db;
        private string defaultAvatar { get; } = "/9j/4AAQSkZJRgABAQEASABIAAD/4QAWRXhpZgAATU0AKgAAAAgAAAAAAAD/7AARRHVja3kAAQAEAAAAUAAA/9sAQwACAgICAgECAgICAwICAwMGBAMDAwMHBQUEBggHCQgIBwgICQoNCwkKDAoICAsPCwwNDg4PDgkLEBEQDhENDg4O/9sAQwECAwMDAwMHBAQHDgkICQ4ODg4ODg4ODg4ODg4ODg4ODg4ODg4ODg4ODg4ODg4ODg4ODg4ODg4ODg4ODg4ODg4O/8AAEQgAIAAgAwERAAIRAQMRAf/EABsAAAIBBQAAAAAAAAAAAAAAAAcIAAECAwQJ/8QAKRAAAgEDBAEDAwUAAAAAAAAAAQIDBAURAAYHITESE0FSYYEIFFFxsf/EABkBAAIDAQAAAAAAAAAAAAAAAAAHAQUGAv/EACYRAAAFBAEDBQEAAAAAAAAAAAABAgQRAwUSIRMGIkFRYXGx0TH/2gAMAwEAAhEDEQA/AO/mgAGO9OSbZs+5R29aV7lcWjDtCkgjWNT49TEHBPwMePxq6ZWyq9TnMJFE+ulJmrCJUL9l8kWzeFbLRLTSW65JGZPYeQOrqCMlWAGSPkED/dQ9ttZmWZnKRLG50nh4RCvQEvVMLwTQAJfzP7tNzvXtIjIk9PDJGT4YBAhI/KkaZtkxWwKPBn9hXXuUvznyRfQ2OElkqub4pEQskFHLJIR4XICDP9k65vuKGPyZDux5Lf8AwRhyNLQM4Y3kSKJndgiKCWZjgAfyToIpEGcBSP1Abg2vcmsSWu50txvFO8iz/tJRIEjIHTFes5HQzkd6Y3Trd1T5OVBkk4idbC36hcNqvHxrI1FMxvQpwDuXbFrW/U92udPbLrUSRCFqqURiRAD0rE4zk9jPfXnR1C1dVeNVNBmkp/m9g6edNqXImosiUcRPoG3jkSaFZI3WSNgCrKcgj7HS5Mo0YY5HOyCEc578vdz5cu+2lrpIrFb5ViSljb0rI4UFmf6jknGegPHzpvWG30KLNDiO9W5/An79cK9V2tvPYnUfoBXv/ca1+IyOQnv/AHGjEGQPXA2+LxbuYbXtl6+SSxXFnjalkbKxv6GZWX6SSoBx5z34Gsff2NCoxXXjvRG/afI1/T7+vTeooT2KnXvGoH//2Q==";
        public string Delete(int Id)
        {
            string result;
            try
            {
               
                    var cu = db.ChatUser.Where(c => c.Id == Id).FirstOrDefault();
                    if (cu != null && cu.Id == Id)
                    {
                        db.ChatUser.Remove(cu);
                        db.SaveChanges();
                        result = "Usuario " + Id + " eliminado con éxito";
                    }
                    else
                        result = "Usuario " + Id + " no encontrado";
               
            }

            catch (Exception ex)
            {
                return "Error en la eliminación del usuario (" + Id + "): " + ex.ToString();
            }
            return result;
        }

        public string Create(string Name)
        {
            string result;
            if (Name.Length <= 2)
            {
                result = "No se puede crear un usuario con nombre " + Name + ". Ingrese nombre y apellido";
            }
            else
            {
                try
                {
                   
                        ChatUser cu = new ChatUser()
                        {
                            Name = Name,
                            Avatar = defaultAvatar,
                            Status = 0,//Disconnected
                            LastActiveOn = DateTime.Now,
                            RoomId = ChatController.ROOM_ID_STUB,//Zamba-Chat
                            Role = 0//Activo
                        };
                        db.ChatUser.Add(cu);
                        db.SaveChanges();
                        result = "Se añadio a " + Name + " correctamente. Id: " + cu.Id;
                    
                }

                catch (Exception ex)
                {
                    return "Error en la inserción del usuario (" + Name + "): " + ex.ToString();
                }
            }
            return result;
        }
        //0: Standard. 1: Base64. 2: Path
        public string UpdateAvatar(int Id, int Type, string Avatar)
        {
            string result;
            var avatar = "";
            try
            {
                switch (Type)
                {
                    case 0:
                        avatar = defaultAvatar;
                        break;
                    case 1:
                        avatar = Avatar;
                        break;
                    case 2:
                        avatar = ImageController.PathToBase64(Avatar);
                        break;
                }

              
                    var cu = db.ChatUser.Where(c => c.Id == Id).FirstOrDefault();
                    if (cu != null && cu.Id == Id)
                    {
                        cu.Avatar = avatar;
                        db.SaveChanges();
                        result = "Usuario " + Id + " eliminado con éxito";
                    }
                    else
                        result = "Usuario " + Id + " no encontrado";
                
            }

            catch (Exception ex)
            {
                return "Error en la actualización de Avatar del usuario (" + Id + "): " + ex.ToString();
            }
            return result;
        }

        public string BlockUser(int Id, int Block)
        {
            string result;

            var role = new ChatUser.RoleType();
            if (Block == 0)
                role = ChatUser.RoleType.Blocked;
            else
                role = ChatUser.RoleType.Active;

            try
            {
               
                    var cu = db.ChatUser.Where(c => c.Id == Id).FirstOrDefault();
                    if (cu != null && cu.Id == Id)
                    {
                        cu.Role = role;
                        db.SaveChanges();
                        result = "Usuario " + Id + ". Estado: " + role.ToString();
                    }
                    else
                        result = "Usuario " + Id + " no encontrado";
                
            }

            catch (Exception ex)
            {
                return "Error en la actualización de Avatar del usuario (" + Id + "): " + ex.ToString();
            }
            return result;
        }

        public string UpdateUser(int Id, string Name, int Status, int Role)
        {
            string result;
            try
            {
               
                    var cu = db.ChatUser.Where(c => c.Id == Id).FirstOrDefault();
                    if (cu != null && cu.Id == Id)
                    {
                        if (!String.IsNullOrEmpty(Name))
                            cu.Name = Name;
                        //Avatar
                        switch (Status)
                        {
                            case 0:
                                cu.Status = ChatUser.StatusType.Offline;
                                break;
                            case 1:
                                cu.Status = ChatUser.StatusType.Online;
                                break;
                            case 2:
                                cu.Status = ChatUser.StatusType.Busy;
                                break;
                        }
                        //LastActiveOn 
                        //RoomId = ChatController.ROOM_ID_STUB,//Zamba-Chat
                        switch (Role)
                        {
                            case 0:
                                cu.Role = ChatUser.RoleType.Active;
                                break;
                            case 1:
                                cu.Role = ChatUser.RoleType.Listener;
                                break;
                            case 2:
                                cu.Role = ChatUser.RoleType.Blocked;
                                break;
                        }
                        db.SaveChanges();
                        result = "Usuario " + Id + ". Modificado";
                    }
                    else
                        result = "Usuario " + Id + " no encontrado";
               
            }
            catch (Exception ex)
            {
                return "Error en la Modificación del usuario (" + Id + "): " + ex.ToString();
            }
            return result;
        }

    }
}