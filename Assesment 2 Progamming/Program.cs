﻿/*
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
using System.Runtime.CompilerServices;

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
        public string CustomerName { get; set; } = string.Empty;
    }

    // Custom Class - RoomAllocation
    public class RoomAllocation
    {
        public int AllocatedRoomNo { get; set; }
        public Customer AllocatedCustomer { get; set; } = new Customer();
    }

    // Custom Main Class - Program
    class Program
    {
        // Variables declaration and initialization
        public static List<Room> listofRooms = new List<Room>();
        public static List<RoomAllocation> listofRoomAllocations = new List<RoomAllocation>();
        public static string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "lhms_studentid.txt");
        public static string backupFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "lhms_studentid_backup.txt");

        // Main function
        static void Main(string[] args)
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            filePath = Path.Combine(folderPath, "lhms_850004123.txt");
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
                        AllocateRoom();
                        break;
                    case 4:
                        // De-Allocate Room From Customer function
                        DeAllocateRoom();
                        break;
                    case 5:
                        // display Room Alocations function;
                        DisplayRoomAllocations();
                        break;
                    case 6:
                        Console.WriteLine("Billing Feature is Under Construction and will be added soon...!!!");
                        break;
                    case 7:
                        // SaveRoomAllocationsToFile
                        SaveRoomAllocationsToFile();
                        break;
                    case 8:
                        //Show Room Allocations From File
                        ShowRoomAllocationsFromFile();
                        break;
                    case 9:
                        // Exit Application
                        Exit();
                        break;
                    case 0:
                        // Backup
                        Backup();
                        break;
                    default:
                        Console.WriteLine("Invalid Input. Please Enter a Valid Number");
                        break;
                }
                       Console.Write("Do you want to continue? (y/n): ");
                        ans = Convert.ToChar(Console.ReadLine() ?? "n");

            }          while (ans == 'y' || ans == 'Y');
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

                    Console.WriteLine("Room Added Successfully");
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
            // if room count ==0 write message
            if (listofRooms.Count == 0)
            {
                Console.WriteLine("No Rooms have been added yet");
                return;
            }

            var availableRooms = listofRooms.Where(x => x.IsAllocated == false).ToList();

            if (availableRooms.Count != 0)
            {
                //Display available rooms
                Console.WriteLine(availableRooms.Count + " Rooms are available for allocation");

                foreach (var Room in availableRooms)
                {
                    Console.WriteLine(Room.RoomNo);
                }
                // Enter room number to allocate
                Console.WriteLine("Enter the Room Number to Allocate");
                int roomNo = Convert.ToInt32(Console.ReadLine());
                if (!int.TryParse(roomNo.ToString(), out roomNo))
                {
                    Console.WriteLine("Invalid Input. Please Enter a Valid Number");
                    return;
                }

                if (listofRooms.Any(x => x.RoomNo == roomNo && x.IsAllocated == true))
                {
                    Console.WriteLine("Room is already allocated");
                    return;
                }

                // Enter customer name
                Console.WriteLine("Enter the Customer Name");
                string customerName = Console.ReadLine();
                if (string.IsNullOrEmpty(customerName))
                {
                    Console.WriteLine("Customer Name cannot be empty");
                    return;
                }

                // Allocate Room to the customer
                RoomAllocation roomAllocation = new RoomAllocation();
                roomAllocation.AllocatedRoomNo = roomNo;
                roomAllocation.AllocatedCustomer = new Customer() { CustomerName = customerName };
                listofRoomAllocations.Add(roomAllocation);

                // Update Room status and add allocation
                var room = listofRooms.FirstOrDefault(x => x.RoomNo == roomNo);
                if (room != null)
                {
                    room.IsAllocated = true;
                }

                Console.WriteLine("Room Allocated Successfully");
                Console.ReadLine();
            }
            else
            {
                throw new InvalidOperationException("No Rooms are available for allocation.");
            }
        }

        // De-Allocate Room From Customer method
        private static void DeAllocateRoom()
        {
            if (listofRooms.Count == 0)
            {
                Console.WriteLine("No Rooms have been added yet");
                return;
            }
            if (listofRoomAllocations.Count == 0)
            {
                Console.WriteLine("No Rooms have been allocated yet");
                return;
            }
            Console.WriteLine("Enter the Room Number to De-Allocate");
            int roomNo = Convert.ToInt32(Console.ReadLine());
            if (!int.TryParse(roomNo.ToString(), out roomNo))
            {
                Console.WriteLine("Invalid Input. Please Enter a Valid Number");
                return;
            }
            if (listofRooms.Any(x => x.RoomNo == roomNo && x.IsAllocated == false))
            {
                Console.WriteLine("Room is not allocated");
                return;
            }
            var room = listofRooms.FirstOrDefault(x => x.RoomNo == roomNo);
            if (room != null)
            {
                room.IsAllocated = false;
            }
            var roomAllocation = listofRoomAllocations.FirstOrDefault(x => x.AllocatedRoomNo == roomNo);
            if (roomAllocation != null)
            {
                listofRoomAllocations.Remove(roomAllocation);
            }
            Console.WriteLine("Room De-Allocated Successfully");
            Console.ReadLine();
        }

        // Display room allocation details
        private static void DisplayRoomAllocations()
        {
            if (listofRoomAllocations.Count == 0)
            {
                Console.WriteLine("No Rooms have been allocated yet");
                return;
            }
            Console.WriteLine("Room No\t\tCustomer Name");
            foreach (var roomAllocation in listofRoomAllocations)
            {
                Console.WriteLine(roomAllocation.AllocatedRoomNo + "\t\t" + roomAllocation.AllocatedCustomer.CustomerName);
            }
        }

        private static void Exit()
        {
            // Ask the user if they would like to exit the application
            Console.WriteLine("Are you sure you want to exit the application? (y/n)");
            string exit = Console.ReadLine();
            if (exit == "y")
            {
                Console.WriteLine("Thank you for using the Langham Hotel Management System");
                Environment.Exit(0);
            }
        }

        private static void SaveRoomAllocationsToFile()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "lhms_850004123.txt");

            Console.WriteLine("Saving Room Allocations to a file...");
            if (listofRoomAllocations.Count == 0)
            {
                Console.WriteLine("No Rooms have been allocated yet");
                return;
            }

            // Ensure file exists
            if (!File.Exists(filePath)) // Check if the file exists
            {
                // Create the file if it does not exist
                File.Create(filePath).Close();
            }

            // Write the Room Allocations to a file
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach (var roomAllocation in listofRoomAllocations)
                {
                    sw.WriteLine(roomAllocation.AllocatedRoomNo + "\t\t" + roomAllocation.AllocatedCustomer.CustomerName);
                    // Add timestamp to the file
                    sw.WriteLine(DateTime.Now);
                }
            }
            Console.WriteLine("Room Allocations have been saved to a file successfully");

            // unathorized access exception handling
            try
            {
                // Check if the file is accessible
                using (FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    Console.WriteLine("File is accessible");
                }
            }// exception handling
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Unauthorized access to the file: " + ex.Message);
            }
        }

        private static void ShowRoomAllocationsFromFile()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "lhms_850004123.txt");
            Console.WriteLine("Showing Room Allocations from a file...");

            // File not found exception handling
            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("File not found");
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            // Show the Room Allocations from a file
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
            Console.WriteLine("Room Allocations have been shown from a file successfully");

        }

        private static void Backup()
        {
            try
            {
                Console.WriteLine("Backing up the file...");
                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                backupFilePath = Path.Combine(folderPath, "lhms_850004123_backup.txt");
                File.Copy(filePath, backupFilePath, true);
                Console.WriteLine("File has been backed up successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while backing up the file: " + ex.Message);
            }

            //Clear the original file
            try
            {
                Console.WriteLine("Clearing the original file...");
                File.WriteAllText(filePath, string.Empty);
                Console.WriteLine("Original file has been cleared successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while clearing the original file: " + ex.Message);
            }
        }
    }

}





















