/*
* Project Name:
* Author Name:
* Date:
* Application Purpose:
*
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace Assessment2Task2
{
    // Custom Class - Room
    public class Room
    {
        public int RoomNo { get; set; }
        public bool IsAllocated { get; set; }
    }

    // Custom Class - Customer
    public class Customer
    {
        public int CustomerNo { get; set; }
        public string CustomerName { get; set; }
    }

    // Custom Class - RoomAllocation
    public class RoomAllocation
    {
        public int AllocatedRoomNo { get; set; }
        public Customer AllocatedCustomer { get; set; }
    }

    // Custom Main Class - Program
    class Program
    {
        // Variables declaration and initialization
        public static List<Room> listofRooms = new List<Room>();
        public static List<RoomAllocation> listofRoomAllocations = new List<RoomAllocation>();
        public static string filePath;
        public static string backupFilePath;

        // Main function
        static void Main(string[] args)
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            filePath = Path.Combine(folderPath, "HotelManagement.txt");
            char ans;
            do
            {
                Console.Clear();
                Console.WriteLine("****************************************************************");
                Console.WriteLine("******************");
                Console.WriteLine(" LANGHAM HOTEL MANAGEMENT SYSTEM");
                Console.WriteLine(" MENU");
                Console.WriteLine("****************************************************************");
                Console.WriteLine("******************");
                Console.WriteLine("1. Add Rooms");
                Console.WriteLine("2. Display Rooms");
                Console.WriteLine("3. Allocate Rooms");
                Console.WriteLine("4. De-Allocate Rooms");
                Console.WriteLine("5. Display Room Allocation Details");
                Console.WriteLine("6. Billing");
                Console.WriteLine("7. Save the Room Allocations To a File");
                Console.WriteLine("8. Show the Room Allocations From a File");
                Console.WriteLine("9. Exit");
                Console.WriteLine("0. Backup");
                Console.WriteLine("****************************************************************");
                Console.WriteLine("******************");
                Console.Write("Enter Your Choice Number Here:");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AddRooms();
                        break;
                    case 2:
                        // display Rooms function;
                        DisplayRooms();
                        break;
                    case 3:
                        // allocate Room To Customer function
                        break;
                    case 4:
                        // De-Allocate Room From Customer function
                        break;
                    case 5:
                        // display Room Alocations function;
                        break;
                    case 6:
                        Console.WriteLine("Billing Feature is Under Construction and will be added soon...!!!");
                        break;
                    case 7:
                        // SaveRoomAllocationsToFile
                        break;
                    case 8:
                        //Show Room Allocations From File
                        break;
                    case 9:
                        // Exit Application
                        break;
                    case 0:
                        // Backup
                        break;
                    default:
                        Console.WriteLine("Invalid Input. Please Enter a Valid Number");
                        break;
                }
                Console.Write("Do you want to continue? (y/n): ");
                ans = Convert.ToChar(Console.ReadLine() ?? "n");

            } while (ans == 'y' || ans == 'Y');
        }

        private static void AddRooms()

        {

            try
            {
                Console.WriteLine("Enter the total numbers of rooms to add");
                int totalRooms = Convert.ToInt32(Console.ReadLine());

                for (int i = 1; i <= totalRooms; i++)
                {
                    Room room = new Room();
                    room.RoomNo = i;
                    room.IsAllocated = false;
                    listofRooms.Add(room);

                    {
                        Console.WriteLine("Room Added Successfully");

                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid Input. Please Enter a Valid Number");
            }
        }

        private static void DisplayRooms()
        {
            if (listofRooms.Count == 0)
            {
                Console.WriteLine("No Rooms have been added yet");
                return;
            }

            Console.WriteLine("Room No\t\tIsAllocated");
            foreach (var room in listofRooms)
            {
                Console.WriteLine(room.RoomNo + "\t\t" + room.IsAllocated);
            }


        }

        private static void AllocateRoom()
        {
            if (listofRooms.Count == 0)
            {
                Console.WriteLine("No Rooms have been added yet");
                return;
            }
        }
    }
}







