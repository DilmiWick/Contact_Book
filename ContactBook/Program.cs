using System;
using System.Runtime.CompilerServices;

class Program
{
    static void Main()
    {
        // string filePath = "contactList.txt";
        // ContactBook contactBook = new ContactBook();
        // Contact obj1 = new Contact("Dilmi","Sri Lanka","0779878125","dilmi@gmail.com");
        // Contact obj2 = new Contact("Nadisha","Sweden","0779878126","nadisha@gmail.com");
        // contactBook.addContact(filePath, obj1);
        // contactBook.addContact(filePath, obj2);

        // string details = contactBook.viewContact("Dilmi", filePath);
        // Console.WriteLine(details);

        int option;
        Console.WriteLine("Welcome to the Contact Book");
        Console.WriteLine("1. Add contact");
        Console.WriteLine("2. View all contacts");
        Console.WriteLine("3. Search contact");
        Console.WriteLine("4. Edit contact");
        Console.WriteLine("5. Delete contact");
        Console.WriteLine("6. Exit");
        Console.Write("Choose an Option: ");

        try
        {
            string strOption = Console.ReadLine() ?? ""; // Replace null with an empty string
            option = int.Parse(strOption);

            if(option == 6)
            {
                return;
            }
            else if(option == 1 | option == 2 | option == 3 | option == 4 | option == 5)
            {
                string filePath = "contactList.txt";
                List<Contact> contactList = new List<Contact>();
                ContactBook contactBook = new ContactBook();

                contactList = contactBook.loadContacts(filePath);

                switch(option)
                {
                    case(1):    //Add new contacts
                    {
                        Console.Write("Type new name: ");
                        string newName = Console.ReadLine() ?? "";
                        Console.Write("Type new address: ");
                        string newAddress = Console.ReadLine() ?? "";
                        Console.Write("Type new phone number: ");
                        string newPhoneNumber = Console.ReadLine() ?? "";
                        Console.Write("Type new email: ");
                        string newEmail = Console.ReadLine() ?? "";
            
                        Contact newContact = new Contact(newName,newAddress,newPhoneNumber,newEmail);
                        contactBook.addContact(filePath, newContact);

                        break;
                    }
                    case(2):    //View all contacts
                    {
                        foreach (Contact contact in contactList)
                        {
                            Console.WriteLine("");
                            Console.WriteLine($"Name: {contact._name}");
                            Console.WriteLine($"Address: {contact._address}");
                            Console.WriteLine($"Phone Number: {contact._phoneNumber}");
                            Console.WriteLine($"Email: {contact._email}");
                        }
                        break;
                    }
                    case(3):    //Search contact by name
                    {
                        Console.Write("Enter name to search: ");
                        string neameToSearch = Console.ReadLine() ?? "";
                        foreach (Contact contact in contactList)
                        {
                            if(neameToSearch == contact._name)
                            {
                                Console.WriteLine("");
                                Console.WriteLine($"Name: {contact._name}");
                                Console.WriteLine($"Address: {contact._address}");
                                Console.WriteLine($"Phone Number: {contact._phoneNumber}");
                                Console.WriteLine($"Email: {contact._email}");
                                break;
                            }  
                        }
                        Console.WriteLine("");
                        Console.WriteLine("Unknown Contact!");
                        break;
                    }
                    case(4):    //Edit contact
                    {
                        Console.WriteLine("Your Option is 4");
                        break;
                    }
                    case(5):    //Delete contact
                    {
                        Console.WriteLine("Your Option is 5");
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid input! Please enter a valid number.");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input! Please enter a valid number.");
        }
        catch (OverflowException)
        {
            Console.WriteLine("The number is too large or too small!");
        }

        
        

    }


    public class Contact
    {
        public string _name,_address, _phoneNumber, _email;

        public Contact(string name, string address, string phoneNumber, string email)
        {
            _name = name;
            _address = address;
            _phoneNumber = phoneNumber;
            _email = email;
        }

        public string name
        {
            get {return _name;}
            set {_name = value;}
        }

        public string address
        {
            get {return _address;}
            set {_address = value;}
        }

        public string phoneNumber
        {
            get {return _phoneNumber;}
            set {_phoneNumber = value;}
        }

        public string email
        {
            get {return _email;}
            set {_email = value;}
        }

        
    }

    public class ContactBook
    {
        public void addContact(string filePath, Contact contact)
        {
            string content = $"{contact._name},{contact._address},{contact._phoneNumber},{contact._email}";
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, append: true))
                {
                    // writer.WriteLine("\n");
                    writer.WriteLine(content);
                    Console.WriteLine("Data written to file successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        // public string viewContact(string name, string filePath)
        // {
        //     int commaIndex = 0;
        //     string nameInFile, contactDetails = "Unknown Contact";

        //     try
        //     {
        //         using (StreamReader reader = new StreamReader(filePath))
        //         {
        //             string line;
        //             while ((line = reader.ReadLine()) != null)
        //             {
        //                 commaIndex = line.IndexOf(',');
        //                 if (commaIndex != -1)
        //                 {
        //                     nameInFile = line.Substring(0, commaIndex);
                            // if(nameInFile == name)
                            // {
        //                         Console.WriteLine("Name Found"); 
        //                         contactDetails = line;
        //                         return contactDetails;
        //                     }
        //                     else
        //                     {
        //                         Console.WriteLine("Unknown Contact"); 
        //                         contactDetails = "Unknown Contact";
        //                         return contactDetails;
        //                     }
        //                 }
        //                 else
        //                 {
        //                     Console.WriteLine("Comma not found!");
        //                     contactDetails = "Unknown Contact";
        //                     return contactDetails;
        //                 }
        //             }
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine("An error occurred: " + ex.Message);
        //         contactDetails = "Unknown Contact";
        //         return contactDetails;
        //     }
        //     return contactDetails;
        // }
    
        public List<Contact> loadContacts(string path)
        {
            List<Contact> contactData = new List<Contact>();
            // Read all lines from the file
            foreach (string line in File.ReadLines(path))
            {
                if (string.IsNullOrWhiteSpace(line))
                continue; // Skip empty lines

                // Split the line by commas
                string[] parts = line.Split(',');

                if (parts.Length == 4) // Ensure there are exactly 4 parts
                {
                    string name, address, phoneNumber, email;
                    {
                        name= parts[0].Trim();
                        address = parts[1].Trim();
                        phoneNumber = parts[2].Trim();
                        email = parts[3].Trim();
                    };
                    Contact contact = new Contact(name,address,phoneNumber,email);
                    contactData.Add(contact);
                }
                else
                {
                    Console.WriteLine($"Skipping invalid line: {line}");
                }
            }

            return contactData;
        }
    }
}