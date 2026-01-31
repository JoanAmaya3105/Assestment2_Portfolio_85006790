/*
* Project Name: Langham Hotel Management System
* Author Name:
* Date:
* Application Purpose:
* Console based Hotel Management System for managing rooms and allocations.
*/

using System;

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

    class Program
    {
        // Variables declaration and initialization
        public static Room[] listofRooms = new Room[50];
        public static RoomAllocation[] listOfRoomAllocations = new RoomAllocation[50];

        public static int roomCount = 0;
        public static int allocationCount = 0;

        static string mainFile = "lhms_studentid.txt";
        static string backupFile = "lhms_studentid_backup.txt";

        static void Main(string[] args)
        {
            char ans = 'Y';   // Inicializado para evitar el error

            do
            {
                Console.Clear();
                Console.WriteLine("***********************************************************************************");
                Console.WriteLine("                  LANGHAM HOTEL MANAGEMENT SYSTEM");
                Console.WriteLine("                                MENU");
                Console.WriteLine("***********************************************************************************");
                Console.WriteLine("0. Backup File");
                Console.WriteLine("1. Add Rooms");
                Console.WriteLine("2. Display Rooms");
                Console.WriteLine("3. Allocate Rooms");
                Console.WriteLine("4. De-Allocate Rooms");
                Console.WriteLine("5. Display Room Allocation Details");
                Console.WriteLine("6. Billing");
                Console.WriteLine("7. Save the Room Allocations To a File");
                Console.WriteLine("8. Show the Room Allocations From a File");
                Console.WriteLine("9. Exit");
                Console.WriteLine("***********************************************************************************");
                Console.Write("Enter Your Choice Number Here: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddRooms();
                        break;

                    case 2:
                        DisplayRooms();
                        break;

                    case 3:
                        AllocateRoom();
                        break;

                    case 4:
                        DeAllocateRoom();
                        break;

                    case 5:
                        DisplayRoomAllocations();
                        break;

                    case 6:
                        Console.WriteLine("\nBilling Feature is Under Construction and will be added soon…!!!");
                        break;

                    case 7:
                        SaveRoomAllocationsToFile();
                        break;

                    case 8:
                        ShowRoomAllocationsFromFile();
                        break;

                    case 0:
                        BackupFile();
                        break;

                    case 9:
                        Console.WriteLine("\nThank you for using LANGHAM Hotel Management System.");
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("\nInvalid choice! Please try again.");
                        break;
                }

                Console.Write("\nWould You Like To Continue (Y/N): ");
                ans = Convert.ToChar(Console.ReadLine());

            } while (ans == 'y' || ans == 'Y');
        }

        // Option 1 - Add Rooms
        static void AddRooms()
        {
            Console.Write("\nHow many rooms do you want to add? ");
            int count = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < count; i++)
            {
                if (roomCount >= listofRooms.Length)
                {
                    Console.WriteLine("Room limit reached!");
                    break;
                }

                Room room = new Room();

                Console.Write("Enter Room Number: ");
                room.RoomNo = Convert.ToInt32(Console.ReadLine());
                room.IsAllocated = false;

                listofRooms[roomCount] = room;
                roomCount++;

                Console.WriteLine("Room added successfully!\n");
            }
        }

        // Option 2 - Display Rooms
        static void DisplayRooms()
        {
            Console.WriteLine("\nRoom No\tStatus");
            Console.WriteLine("------------------------");

            if (roomCount == 0)
            {
                Console.WriteLine("No rooms available.");
                return;
            }

            for (int i = 0; i < roomCount; i++)
            {
                string status = listofRooms[i].IsAllocated ? "Allocated" : "Available";
                Console.WriteLine($"{listofRooms[i].RoomNo}\t{status}");
            }
        }

        // Option 3 - Allocate Room
        static void AllocateRoom()
        {
            Console.Write("\nEnter Room Number to Allocate: ");
            int roomNo = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < roomCount; i++)
            {
                if (listofRooms[i].RoomNo == roomNo)
                {
                    if (listofRooms[i].IsAllocated)
                    {
                        Console.WriteLine("Room is already allocated.");
                        return;
                    }

                    Customer cust = new Customer();

                    Console.Write("Enter Customer Number: ");
                    cust.CustomerNo = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Enter Customer Name: ");
                    cust.CustomerName = Console.ReadLine();

                    listofRooms[i].IsAllocated = true;

                    RoomAllocation alloc = new RoomAllocation();
                    alloc.AllocatedRoomNo = roomNo;
                    alloc.AllocatedCustomer = cust;

                    listOfRoomAllocations[allocationCount] = alloc;
                    allocationCount++;

                    Console.WriteLine("Room allocated successfully!");
                    return;
                }
            }

            Console.WriteLine("Room not found.");
        }

        // Option 4 - De-Allocate Room
        static void DeAllocateRoom()
        {
            Console.Write("\nEnter Room Number to De-Allocate: ");
            int roomNo = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < allocationCount; i++)
            {
                if (listOfRoomAllocations[i].AllocatedRoomNo == roomNo)
                {
                    // Mark room as available again
                    for (int j = 0; j < roomCount; j++)
                    {
                        if (listofRooms[j].RoomNo == roomNo)
                        {
                            listofRooms[j].IsAllocated = false;
                            break;
                        }
                    }

                    // Remove allocation (shift array)
                    for (int k = i; k < allocationCount - 1; k++)
                    {
                        listOfRoomAllocations[k] = listOfRoomAllocations[k + 1];
                    }

                    allocationCount--;
                    Console.WriteLine("Room de-allocated successfully!");
                    return;
                }
            }

            Console.WriteLine("Room allocation not found.");
        }

        // Option 5 - Display Room Allocation Details
        static void DisplayRoomAllocations()
        {
            Console.WriteLine("\nRoom No\tCustomer No\tCustomer Name");
            Console.WriteLine("--------------------------------------------");

            if (allocationCount == 0)
            {
                Console.WriteLine("No rooms are currently allocated.");
                return;
            }

            for (int i = 0; i < allocationCount; i++)
            {
                Console.WriteLine(
                    $"{listOfRoomAllocations[i].AllocatedRoomNo}\t" +
                    $"{listOfRoomAllocations[i].AllocatedCustomer.CustomerNo}\t\t" +
                    $"{listOfRoomAllocations[i].AllocatedCustomer.CustomerName}");
            }
        }
        static void SaveRoomAllocationsToFile()
        {
            using (StreamWriter sw = new StreamWriter(mainFile, true))
            {
                sw.WriteLine("----- " + DateTime.Now + " -----");
                for (int i = 0; i < allocationCount; i++)
                {
                    sw.WriteLine(
                        listOfRoomAllocations[i].AllocatedRoomNo + "," +
                        listOfRoomAllocations[i].AllocatedCustomer.CustomerNo + "," +
                        listOfRoomAllocations[i].AllocatedCustomer.CustomerName);
                }
            }
            Console.WriteLine("Data saved to file.");
        }

        static void ShowRoomAllocationsFromFile()
        {
            if (!File.Exists(mainFile))
            {
                Console.WriteLine("File does not exist.");
                return;
            }

            using (StreamReader sr = new StreamReader(mainFile))
            {
                Console.WriteLine("\nFILE CONTENT:");
                Console.WriteLine(sr.ReadToEnd());
            }
        }

        static void BackupFile()
        {
            if (!File.Exists(mainFile))
            {
                Console.WriteLine("No data to backup.");
                return;
            }

            string content;
            using (StreamReader sr = new StreamReader(mainFile))
            {
                content = sr.ReadToEnd();
            }

            using (StreamWriter sw = new StreamWriter(backupFile, true))
            {
                sw.WriteLine("BACKUP - " + DateTime.Now);
                sw.WriteLine(content);
            }

            File.WriteAllText(mainFile, string.Empty);
            Console.WriteLine("Backup completed and original file cleared.");
        }
    }
}

