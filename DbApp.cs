using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EmpDB
{
    internal class DbApp
    {
        private List<Employee> employees = new List<Employee>();

        public void ReadEmployeeDataFromInputFile()
        {
            try
            {
                // read the JSON file as a string and deserialize it back
                // into Employee sub-type objects and place them into 
                // the employee List
                string jsonFromFile = File.ReadAllText(EmployeeOutputJSONFile);
                // null coalesce
                employees = JsonConvert.DeserializeObject<List<Employee>>(jsonFromFile) ?? new List<Employee>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading from file: {ex.Message}");
            }

        }

        public void RunDatabaseApp()
        {
            while (true)
            {
                // First, display the main menu
                DisplayMainMenu();
                char selection = GetUserSelection();
                Console.WriteLine();
                // Switch for user selection
                switch (char.ToLower(selection))
                {
                    case 'a':
                        // Add a record
                        AddEmployeeRecord();
                        break;
                    case 'f':
                        // Find a record
                        FindEmployeeRecord();
                        break;
                    case 'm':
                        // MOdify a record
                        ModifyEmployeeRecord();
                        break;
                    case 'd':
                        // Delete a record
                        DeleteStudentRecord();
                        break;
                    case 'p':
                        // Print all records
                        PrintAllEmployeeRecords();
                        break;
                    case 'k':
                        // Print emails only
                        PrintAllEmployeeRecordKeys();
                        break;
                    case 's':
                        // Save and exit
                        SaveEmployeeDataAndExit();
                        break;
                    case 'e':
                        // Exit without saving
                        // TODO: Add confirmation of exit
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine($"\nERROR: {selection} is not a vaild input. Select again.\n");
                        break;
                }
            }
        }


        private void DisplayMainMenu()
        {
            EmployeeDb();
            string menu = string.Empty;
            menu += "*****************************************************\n";
            menu += "*********** Employee Payroll Database App ***********\n";
            menu += "*****************************************************\n";
            menu += "[A]dd a employee record (C in CRUD - Create)\n";
            menu += "[F]ind a employee record (R in CRUD - Read)\n";
            menu += "[M]odify a employee record (U in CRUD - Update)\n";
            menu += "[D]elete a employee record (D in CRUD - Delete)\n";
            menu += "[P]rint all records in current db storage\n";
            menu += "Print all primary [K]eys (social security number\n";
            menu += "[S]ave data to file and exit app\n";
            menu += "[E]xit app without saving changes\n";
            menu += "\n";
            menu += "User Key Selection: ";

            Console.Write(menu);
        }

        private const string EmployeeOutputJSONFile = "employees.json";

        private void WriteDataToOutputFile()
        {
            // Creates a JSON style string of alll Employee types in employees List
            // and saves the file
            string json = JsonConvert.SerializeObject(employees, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(EmployeeOutputJSONFile, json);
        }

        public void EmployeeDb()
        {
            
            SalariedEmployee emp1 = new SalariedEmployee(
                firstName: "John",
                lastName: "Smith",
                socialSecurityNumber: "111-11-1111",
                weeklySalary: 800.00m
                );

            SalariedEmployee emp2 = new SalariedEmployee(
                firstName: "Samantha",
                lastName: "Johnson",
                socialSecurityNumber: "555-55-5555",
                weeklySalary: 900.00m
                );

            HourlyEmployee emp3 = new HourlyEmployee(
                firstName: "Karen",
                lastName: "Price",
                socialSecurityNumber: "222-22-2222",
                hourlyWage: 16.75m,
                hoursWorked: 40.0m
                );

            HourlyEmployee emp4 = new HourlyEmployee(
                firstName: "Brian",
                lastName: "Brown",
                socialSecurityNumber: "666-66-6666",
                hourlyWage: 18.20m,
                hoursWorked: 38.0m
                );

            CommissionEmployee emp5 = new CommissionEmployee(
                firstName: "Sue",
                lastName: "Jones",
                socialSecurityNumber: "333-33-3333",
                grossSales: 10000.00m,
                commissionRate: 0.06m
                );

            CommissionEmployee emp6 = new CommissionEmployee(
                firstName: "Tom",
                lastName: "Williams",
                socialSecurityNumber: "777-77-7777",
                grossSales: 9500.00m,
                commissionRate: 0.05m
                );

            BasePlusCommissionEmployee emp7 = new BasePlusCommissionEmployee(
                firstName: "Bob",
                lastName: "Lewis",
                socialSecurityNumber: "444-44-4444",
                grossSales: 5000.00m,
                commissionRate: 0.04m,
                baseSalary: 300.00m
                );

            BasePlusCommissionEmployee emp8 = new BasePlusCommissionEmployee(
                firstName: "Jane",
                lastName: "Deer",
                socialSecurityNumber: "888-88-8888",
                grossSales: 4500.00m,
                commissionRate: 0.04m,
                baseSalary: 290.00m
               );


        }
        private char GetUserSelection()
        {
            ConsoleKeyInfo key = Console.ReadKey();
            return key.KeyChar;
        }

        private Employee CheckIfSsnExists(string ssn)
        {
            var emp = employees.FirstOrDefault(employee => employee.SocialSecurityNumber == ssn);
            if (emp == null)
            {
                //if (ssn.Length != 11)
                //{
                //    // Invalid SSN length
                //    Console.WriteLine($"ERROR: SSN {ssn} is invalid. Must be 11 characters long (including dashes).\n");
                //    Console.Write("Please enter a valid SSN in format of '###-##-####'.");
                //    ssn = Console.ReadLine();
                //    return CheckIfSsnExists(ssn);
               // }

                // Did not find a record
                Console.WriteLine($"Employee with SSN {ssn} does not exist.\n");
                return null;
            }
            else
            {
                // Found it!
                Console.WriteLine($"FOUND SSN: {ssn}\n");
                return emp;
            }
        }

        private void SaveEmployeeDataAndExit()
        {
            Console.WriteLine("Saving data and exiting.");
            WriteDataToOutputFile();
            Environment.Exit(0);
        }

        private void PrintAllEmployeeRecords()
        {
            Console.WriteLine("***** Printing all employee records in file *****");
            Console.WriteLine();

            foreach (var employee in employees)
            {
                Console.WriteLine(employee);
            }
            Console.WriteLine("\n***** Done all employee records in file *****");
            Console.WriteLine();
        }

        private void ModifyEmployeeRecord()
        {
            throw new NotImplementedException();
        }

        private void FindEmployeeRecord()
        {
            // Ask user for SSN to search for
            Console.Write("Please enter the employee's SSN to search for: ");
            string ssn = Console.ReadLine();
            var employee = CheckIfSsnExists(ssn);
            if (employee != null)
            {
                Console.WriteLine(employee);
            }
        }

        private void AddEmployeeRecord()
        {
            // First, search if employee already exists
            Console.Write("ENTER SSN to add: ");
            string ssn = Console.ReadLine();
            Employee emp = CheckIfSsnExists(ssn);
            if (emp == null)
            {
                // Record was NOT FOUND -- go ahead and add
                Console.WriteLine($"Adding new employee w/ SSN: {ssn}");

                // Gather initial employee data

                // Get first name
                Console.Write("ENTER first name: ");
                string first = Console.ReadLine();

                // Get last name
                Console.Write("ENTER last name: ");
                string last = Console.ReadLine();

                // Already have SSN

                // Get employee type
                Console.Write("[H]ourly, [S]alaried, [C]ommission, or [B]ase plus commission? ");
                string employeeType = Console.ReadLine();

                // Branch out to employee type
                if (employeeType.ToLower() == "h")
                {
                    // Hourly employee
                    Console.Write("ENTER hourly wage: ");
                    decimal hrlyWage = Convert.ToDecimal(Console.ReadLine());

                    Console.Write("ENTER hours worked: ");
                    decimal hrsWorked = Convert.ToDecimal(Console.ReadLine());

                    HourlyEmployee hourlyEmp = new HourlyEmployee(
                       firstName: first,
                       lastName: last,
                       socialSecurityNumber: ssn,
                       hourlyWage: hrlyWage,
                       hoursWorked: hrsWorked
                       );
                    employees.Add(hourlyEmp);
                }

                else if (employeeType.ToLower() == "s")
                {
                    // Salaried employee
                    Console.Write("ENTER weekly salary: ");
                    decimal weeklySal = Convert.ToDecimal(Console.ReadLine());

                    SalariedEmployee salariedEmp = new SalariedEmployee(
                      firstName: first,
                      lastName: last,
                      socialSecurityNumber: ssn,
                      weeklySalary: weeklySal
                      );
                    employees.Add(salariedEmp);
                }

                else if (employeeType.ToLower() == "c")
                {
                    // Commission employee
                    Console.Write("ENTER gross sales: ");
                    decimal gSales = Convert.ToDecimal(Console.ReadLine());

                    Console.Write("ENTER commission rate: ");
                    decimal comRate = Convert.ToDecimal(Console.ReadLine());

                    CommissionEmployee commissionEmp = new CommissionEmployee(
                      firstName: first,
                      lastName: last,
                      socialSecurityNumber: ssn,
                      grossSales: gSales,
                      commissionRate: comRate
                      );
                    employees.Add(commissionEmp);
                }

                else if (employeeType.ToLower() == "b")
                {
                    // Base plus commission employee
                    Console.Write("ENTER gross sales: ");
                    decimal gSales = Convert.ToDecimal(Console.ReadLine());

                    Console.Write("ENTER commission rate: ");
                    decimal comRate = Convert.ToDecimal(Console.ReadLine());

                    Console.Write("ENTER base salary: ");
                    decimal baseSal = Convert.ToDecimal(Console.ReadLine());

                    BasePlusCommissionEmployee basecommissionEmp = new BasePlusCommissionEmployee(
                      firstName: first,
                      lastName: last,
                      socialSecurityNumber: ssn,
                      grossSales: gSales,
                      commissionRate: comRate,
                      baseSalary: baseSal
                      );
                    employees.Add(basecommissionEmp);
                }
            }
        }
        private void PrintAllEmployeeRecordKeys()
        {
            Console.WriteLine("***** Printing all employee social security numbers in file *****");
            Console.WriteLine();
            foreach (var employee in employees)
            {
                Console.WriteLine(employee.SocialSecurityNumber);
            }
            Console.WriteLine("***** Done all employee social security numbers in file *****");
            Console.WriteLine();
        }

        private void DeleteStudentRecord()
        {
            Console.WriteLine("***** Delete a employee record *****");
            Console.WriteLine();
            bool remove = false;
            while (!remove)
            {
                Console.Write("\nPlease enter the social security number of employee to delete: ");
                string ssn = Console.ReadLine();
                var employee = CheckIfSsnExists(ssn);
                if (employee != null)
                {
                    Console.WriteLine(employee);
                    Console.Write("\nIf this is the employee record you wish to delete, please" +
                        "enter 'C' to confirm: ");

                    string confirm = Console.ReadLine();

                    if (confirm.ToLower() == "c")
                    {
                        employees.Remove(employee);

                        remove = true;
                    }
                    else
                    {
                        Console.WriteLine("Delete cancelled. No record removed.");
                        Console.Write("\n To search for another employee record to delete, press [D]." +
                            "To return to the main menu, press any other key.");
                        string choice = Console.ReadLine();
                        if (choice.ToLower() == "d")
                        {
                            remove = false;
                        }
                        else
                        {
                            remove = true;
                        }
                    }
                }
            }
        }
    }
}
