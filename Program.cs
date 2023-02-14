// See https://aka.ms/new-console-template for more information
using System.Data.SqlClient;
using System;
using System.Data;
using targil_3;

Library library= new Library();
library.DropDatabase();//every run of the code will drop and recreate the database and the tables so if you'd like you can cooment this out
library.CreateDatabaseAndTables();
int num;
Console.WriteLine("please enter a number of the action you would like to do: 1- add book, 2-add subscriber, 3- borrow book, " +
    "4- return book, 5- print book details, 6- print books by genre, 7-print book by sub, 8-exist");
num = 0;
while (!int.TryParse(Console.ReadLine(), out num) || num>8 || num<1)
{
    Console.WriteLine("please enter a number between 1-8");
}
while (num != 8)
{
    switch (num)
    {
        case 1:
            library.AddBook();
            break;
        case 2:
            library.AddSubscriber();
            break;
        case 3:
            library.BorrowBook();
            break;
        case 4:
            library.ReturnBook();
            break;
        case 5:
            library.PrintBookDetails();
            break;
        case 6:
            library.PrintBooksByGenre();
            break;
        case 7:
            library.PrintBooksBySub();
            break;
        case 8:
            Console.WriteLine("goodbye!");
            break;
    }
    Console.WriteLine("please enter a number of the action you would like to do: 1- add book, 2-add subscriber, 3- borrow book, " +
    "4- return book, 5- print book details, 6- print books by genre, 7-print book by sub, 8-exist");
    while (!int.TryParse(Console.ReadLine(), out num) || num > 8 || num < 1)
    {
        Console.WriteLine("please enter a number between 1-8");
    }
}