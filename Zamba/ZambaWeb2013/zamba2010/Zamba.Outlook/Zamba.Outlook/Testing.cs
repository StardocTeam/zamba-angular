using System;
using System.Collections.Generic;
using System.Text;

namespace Zamba.Outlook
{
    public sealed class Testing
    {

        public static void Main(String[] args)
        {
            try
            {

                //#region Todos Los Contactos
                //Console.WriteLine("Listado de Contactos");
                //Console.WriteLine();

                //List<OutlookContact> Contacts = OutlookContact.GetContacts();

                //foreach (OutlookContact CurrentContact in Contacts)
                //{
                //    Console.WriteLine(CurrentContact.FullName);

                //    foreach (String CurrentEmail in CurrentContact.EmailAddresses)
                //        Console.WriteLine("  " + CurrentEmail);

                //    Console.WriteLine();
                //} 
                //#endregion

                //#region Contacts
                //List<AddressBook> MyBooks = AddressBook.GetAddressBooks();

                //Console.WriteLine("Cantidad de Libretas : " + MyBooks.Count.ToString());
                //Console.WriteLine();

                //foreach (AddressBook MyBook in MyBooks)
                //{
                //    Console.WriteLine("Libreta : " + MyBook.Name);

                //    foreach (OutlookContact MyContact in MyBook.Contacts)
                //    {
                //        Console.WriteLine();
                //        Console.WriteLine("Nombre: " + MyContact.FullName );

                //        foreach (String CurrentMail in MyContact.EmailAddresses)
                //            Console.WriteLine(CurrentMail);
                //    }
                //    Console.WriteLine();
                //}


                //Console.ReadLine();
                //#endregion

                //List<IAppointment> Appointments = Appointment.GetAppointments();

                //foreach (Appointment App in Appointments)
                //{
                //    Console.WriteLine(App.Body);
                //    Console.WriteLine("Empieza en " + App.Start.ToString());
                //    Console.WriteLine("Termina en " + App.End.ToString());
                //    Console.WriteLine("En " +App.Location);
                //    Console.WriteLine(App.Priority.ToString() );

                //    foreach (String ToAddress in App.ToAddresses)
                //        Console.WriteLine("     " + ToAddress);
                //}
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                Console.Read();
            }

            Console.ReadLine();
        }
    }
}
