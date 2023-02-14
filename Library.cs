using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Globalization;

namespace targil_3
{
    class Library
    {
        public void DropDatabase()
        {
            SqlConnectionStringBuilder conn_str = new SqlConnectionStringBuilder();
            conn_str.ConnectionString = @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;";
            SqlConnection sqlconnection = new SqlConnection(conn_str.ConnectionString);
            sqlconnection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlconnection;
            cmd.CommandText = "DROP DATABASE IF EXISTS NatanielParievsky";
            cmd.ExecuteNonQuery();
            Console.WriteLine("database droped succefully");
        }
        public void CreateDatabaseAndTables()
        {
            SqlConnectionStringBuilder conn_str = new SqlConnectionStringBuilder();
            conn_str.ConnectionString = @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;";

            SqlConnection sqlconnection = new SqlConnection(conn_str.ConnectionString);

            //create database
            string str = @"CREATE DATABASE NatanielParievsky";
            SqlCommand myCommand = new SqlCommand(str, sqlconnection);
            try
            {
                sqlconnection.Open();
                myCommand.ExecuteNonQuery();
                Console.WriteLine("DataBase is Created successfully");
            }
            catch (System.Exception error)
            {
                Console.WriteLine(error.Message);
            }
            finally
            {
                if (sqlconnection.State == System.Data.ConnectionState.Open)
                {
                    sqlconnection.Close();
                }
            }
            //create subscriber table
            conn_str = new SqlConnectionStringBuilder();
            conn_str.ConnectionString = @"Server=localhost\SQLEXPRESS;Database=NatanielParievsky;Trusted_Connection=True;";

            sqlconnection = new SqlConnection(conn_str.ConnectionString);
            sqlconnection.Open();
            using SqlCommand cmd = new SqlCommand
            {
                Connection = sqlconnection,
                CommandType = System.Data.CommandType.Text,
            };
            cmd.CommandText = "DROP TABLE IF EXISTS Subscribers";
            cmd.ExecuteNonQuery();
            string query =
                @"CREATE TABLE Subscribers
        (
            id NVARCHAR(9) NOT NULL PRIMARY KEY,
            fname NVARCHAR(15) NOT NULL,
            lname NVARCHAR(15) NOT NULL,
            book1 NVARCHAR(5),
            book2 NVARCHAR(5),
            book3 NVARCHAR(5),
        )";
            cmd.CommandText = query;
            int results = cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Subscribers(id,fname,lname,book1,book2,book3) VALUES('211375167','nati','parievsky','','','')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Subscribers(id,fname,lname,book1,book2,book3) VALUES('316944765','yana','foigel','','','')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Subscribers(id,fname,lname,book1,book2,book3) VALUES('421376167','sasha','mann','','','')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Subscribers(id,fname,lname,book1,book2,book3) VALUES('123456789','miri','pesa','','','')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Subscribers(id,fname,lname,book1,book2,book3) VALUES('987654321','faina','zalman','','','')";
            cmd.ExecuteNonQuery();

            //create table books
            cmd.CommandText = "DROP TABLE IF EXISTS Books";
            cmd.ExecuteNonQuery();
            query =
                @"CREATE TABLE Books
      (
        id_book NVARCHAR(5) NOT NULL PRIMARY KEY,
        title NVARCHAR(30) NOT NULL,
        author NVARCHAR(30) NOT NULL,
        genre NVARCHAR(20) NOT NULL,
        type NVARCHAR(1) NOT NULL,
        copies INT NOT NULL,
        id NVARCHAR(9) NOT NULL FOREIGN KEY REFERENCES Subscribers(id),
      )";
            cmd.CommandText = query;
            results = cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Books(id_book,title,author,genre,type,copies,id) VALUES('10','harry potter','jk rowling','fantasy','1',1,'123456789')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Books(id_book,title,author,genre,type,copies,id) VALUES('20','percy jackson','riodan','fantasy','2',1,'123456789')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Books(id_book,title,author,genre,type,copies,id) VALUES('30','a song of ice and fire','riodan','fantasy','2',1,'123456789')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Books(id_book,title,author,genre,type,copies,id) VALUES('40','the call of cthulu','hp lovecraft','horror','1',1,'123456789')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Books(id_book,title,author,genre,type,copies,id) VALUES('50','foundation','azimov','science fiction','2',1,'123456789')";
            cmd.ExecuteNonQuery();
        }
        public void AddBook() 
        {
            SqlConnectionStringBuilder conn_str = new SqlConnectionStringBuilder();
            conn_str.ConnectionString = @"Server=localhost\SQLEXPRESS;Database=NatanielParievsky;Trusted_Connection=True;";

            SqlConnection sqlconnection = new SqlConnection(conn_str.ConnectionString);
            sqlconnection.Open();
            using SqlCommand cmd = new SqlCommand
            {
                Connection = sqlconnection,
                CommandType = System.Data.CommandType.Text,
                CommandText = "SELECT * FROM Books Where id_book=@key_value_of_book",
            };
            //print all books
            cmd.CommandText = "SELECT * FROM Books";
            SqlParameter sqlparam = new SqlParameter("", System.Data.SqlDbType.NChar);
            sqlparam.Value = "";
            cmd.Parameters.Add(sqlparam);
            using SqlDataReader results2 = cmd.ExecuteReader();
            while (results2.Read())
            {
                Console.WriteLine($"{results2[0]}- {results2[1]}");

            }
            results2.Close();

            Console.WriteLine("please enter the type of book you would like. physical:1, digital:2");
            char type;

            while (!char.TryParse(Console.ReadLine(), out type) || (type != '1' && type != '2'))
            {
                Console.WriteLine("please enter only a number: 1 or 2");
            }

            Console.WriteLine("please enter the title of the book");
            string title = Console.ReadLine();
            while (title == "" || title.Length >30)
            {
                Console.WriteLine("the title cant be empty space or bigger the 30 characters, please try again");
                title = Console.ReadLine();
            }

            Console.WriteLine("please enter the author's name of the book");
            string author = Console.ReadLine();
            while (author == "" || author.Length>30)
            {
                Console.WriteLine("the author cant be empty space  or bigger the 30 characters, please try again");
                author = Console.ReadLine();
            }

            Console.WriteLine("please enter the genre of the book");
            string genre = Console.ReadLine();
            while (genre == "" || genre.Length>20)
            {
                Console.WriteLine("the genre cant be empty space or bigger then 20 characters, please try again");
                genre = Console.ReadLine();
            }
            Console.WriteLine("please enter the id of the book(remember its up to five digits)");
            int key_value_of_book = 0;

            while (!int.TryParse(Console.ReadLine(), out key_value_of_book) || key_value_of_book > 9999 || key_value_of_book < 0)
            {
                Console.WriteLine("please enter only number(remember its up to five digits) " +
                    "and only between 0 and 9999");
            }

            cmd.CommandText = "SELECT * FROM Books Where id_book=@key_value_of_book";
            sqlparam = new SqlParameter("key_value_of_book",System.Data.SqlDbType.NChar);
            sqlparam.Value = key_value_of_book;
            cmd.Parameters.Add(sqlparam);
            using SqlDataReader results = cmd.ExecuteReader();
            while (results.HasRows)
            {
                results.Close();
                
                cmd.CommandText = "SELECT type FROM Books Where id_book=@key_value_of_book";
                object t_of_book = cmd.ExecuteScalar();
                char type_book = System.Convert.ToChar(t_of_book);//checking if the type entered equls the type that exists is the table

                if (type == type_book)
                {
                    if (type_book == '1')
                    {
                        cmd.CommandText = "UPDATE Books SET copies+=1 WHERE id_book=@key_value_of_book";
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("a copy of the paperbook has been added");
                        return;
                    }
                    if (type_book == '2')
                    {
                        Console.WriteLine("the digital book already exists");
                        return;
                    }
                }
                Console.WriteLine("the type of the book you have mentioned doesnt match the type that it is in the table, please try again");
                return;
            }
            results.Close();
            
            cmd.CommandText = "INSERT INTO Books(id_book,title,author,genre,type,copies,id) " +
                "VALUES('" + key_value_of_book + "','"+title+"','"+author+"','"+genre+"','"+type+ "',1,'123456789')";
            cmd.ExecuteNonQuery();
            Console.WriteLine("the book has been added succefully");
            sqlconnection.Close();
            return;
        }
        public void AddSubscriber()
        {
            SqlConnectionStringBuilder conn_str = new SqlConnectionStringBuilder();
            conn_str.ConnectionString = @"Server=localhost\SQLEXPRESS;Database=NatanielParievsky;Trusted_Connection=True;";

            SqlConnection sqlconnection = new SqlConnection(conn_str.ConnectionString);
            sqlconnection.Open();
            using SqlCommand cmd = new SqlCommand
            {
                Connection = sqlconnection,
                CommandType = System.Data.CommandType.Text,
                CommandText = "SELECT * FROM Subscribers Where id=@sub_id",
            };
            //print all subscribers
            cmd.CommandText = "SELECT * FROM Subscribers";
            SqlParameter sqlparam = new SqlParameter("", System.Data.SqlDbType.NChar);
            sqlparam.Value = "";
            cmd.Parameters.Add(sqlparam);
            using SqlDataReader results2 = cmd.ExecuteReader();
            while (results2.Read())
            {
                Console.WriteLine($"{results2[0]}- {results2[1]} {results2[2]}");

            }
            results2.Close();

            Console.WriteLine("please enter the id of the subscriber");
            string sub_id = Console.ReadLine();

            while (sub_id.Length > 9 || sub_id.Length < 8)
            {
                Console.WriteLine("please enter only number(remember its up to five digits) " +
                    "and only between 9999999 and 999999999");
            }

            Console.WriteLine("please enter the subscribers first name");
            string fname = Console.ReadLine();
            while (fname == "")
            {

                Console.WriteLine("the first name cant be empty, please enter again");
                fname = Console.ReadLine();
            }

            Console.WriteLine("please enter the subscribers last name");
            string lname = Console.ReadLine();
            while (lname == "")
            {

                Console.WriteLine("the last name cant be empty, please enter again");
                lname = Console.ReadLine();
            }
            cmd.CommandText = "SELECT * FROM Subscribers Where id=@sub_id";
            sqlparam = new SqlParameter("sub_id", System.Data.SqlDbType.NChar);
            sqlparam.Value = sub_id;
            cmd.Parameters.Add(sqlparam);
            using SqlDataReader results = cmd.ExecuteReader();

            while (results.HasRows)
            {
                results.Close();
                Console.WriteLine("the subscriber already exists please try again");
                return;

            }
            results.Close();
            cmd.CommandText = "INSERT INTO Subscribers(id,fname,lname) " +
                "VALUES('" + sub_id + "','" + fname + "','" + lname + "')";
            cmd.ExecuteNonQuery();
            Console.WriteLine("the new subscriber has been added succefully");
            sqlconnection.Close();
            return;
        }
        public void BorrowBook()
        {
            Console.WriteLine("please enter the id of the sub who would like to borrow the book");
            string sub_id=Console.ReadLine();
            while (sub_id.Length > 9 || sub_id.Length < 8)
            {
                Console.WriteLine("please enter only number(remember its up to five digits) " +
                    "and only between 8 and 9 digits");
                sub_id= Console.ReadLine();
            }
            Console.WriteLine("please enter the id of the book");
            string key_book=Console.ReadLine();
            while (key_book.Length >5)
            {
                Console.WriteLine("please enter only number(remember its up to five digits) ");
                key_book= Console.ReadLine();
            }
            SqlConnectionStringBuilder conn_str = new SqlConnectionStringBuilder();
            conn_str.ConnectionString = @"Server=localhost\SQLEXPRESS;Database=NatanielParievsky;Trusted_Connection=True;";

            SqlConnection sqlconnection = new SqlConnection(conn_str.ConnectionString);
            sqlconnection.Open();
            using SqlCommand cmd = new SqlCommand
            {
                Connection = sqlconnection,
                CommandType = System.Data.CommandType.Text,
                CommandText = "SELECT * FROM Subscribers Where id=@sub_id",
            };
            //checking if subscriber exists in table
            SqlParameter sqlparam = new SqlParameter("sub_id", System.Data.SqlDbType.NChar);
            sqlparam.Value = sub_id;
            cmd.Parameters.Add(sqlparam);
            using SqlDataReader results = cmd.ExecuteReader();

            if (!results.HasRows)
            {
                results.Close();
                Console.WriteLine("the subscriber doesnt exist, please try another");
                return;

            }
            results.Close();

            //checking if book exists in table
            cmd.CommandText = "SELECT * FROM Books WHERE id_book=@key_book";
            sqlparam = new SqlParameter("key_book", System.Data.SqlDbType.NChar);
            sqlparam.Value = key_book;
            cmd.Parameters.Add(sqlparam);
            using SqlDataReader results2 = cmd.ExecuteReader();

            if (!results2.HasRows)
            {
                results2.Close();
                Console.WriteLine("the book doesnt exist, please try another");
                return;
            }
            results2.Close();
            //get type of book and if it's physical, check if its above 0
            cmd.CommandText = "SELECT type FROM Books WHERE id_book=@key_book";
            object type = cmd.ExecuteScalar();
            char type_book = System.Convert.ToChar(type);
            if (type_book == '1')
            {
                cmd.CommandText = "SELECT copies FROM Books WHERE id_book=@key_book";
                object copies=cmd.ExecuteScalar();
                int num_copies=System.Convert.ToInt32(copies);
                if(num_copies == 0) 
                {
                    Console.WriteLine("there arent availble copies of this physical book, please try another book");
                    sqlconnection.Close();
                    return;
                }
            }

            //checks done now update book
            //book1 slot
            cmd.CommandText = "SELECT book1 FROM Subscribers WHERE id=@sub_id";
            object book1 = cmd.ExecuteScalar();
            string book_1 = System.Convert.ToString(book1);
            if (book_1 == "")
            {
                cmd.CommandText = "UPDATE Subscribers SET book1=@key_book WHERE id=@sub_id";
                cmd.ExecuteNonQuery();
                Console.WriteLine("book1 updated succefully");
                if (type_book == '1')
                {
                    cmd.CommandText = "UPDATE Books SET copies-=1 WHERE id_book=@key_book";
                    cmd.ExecuteNonQuery();
                }
                sqlconnection.Close();
                return;
            }
            //book2 slot
            cmd.CommandText = "SELECT book2 FROM Subscribers WHERE id=@sub_id";
            object book2 = cmd.ExecuteScalar();
            string book_2 = System.Convert.ToString(book2);
            if (book_2 == "")
            {
                cmd.CommandText = "UPDATE Subscribers SET book2=@key_book WHERE id=@sub_id";
                cmd.ExecuteNonQuery();
                Console.WriteLine("book2 updated succefully");
                if (type_book == '1')
                {
                    cmd.CommandText = "UPDATE Books SET copies-=1 WHERE id_book=@key_book";
                    cmd.ExecuteNonQuery();
                }
                sqlconnection.Close();
                return;
            }
            //book3 slot
            cmd.CommandText = "SELECT book3 FROM Subscribers WHERE id=@sub_id";
            object book3 = cmd.ExecuteScalar();
            string book_3 = System.Convert.ToString(book3);
            if (book_3 == "")
            {
                cmd.CommandText = "UPDATE Subscribers SET book3=@key_book WHERE id=@sub_id";
                cmd.ExecuteNonQuery();
                Console.WriteLine("book3 updated succefully");
                if (type_book == '1')
                {
                    cmd.CommandText = "UPDATE Books SET copies-=1 WHERE id_book=@key_book";
                    cmd.ExecuteNonQuery();
                }
                sqlconnection.Close();
                return;
            }
            Console.WriteLine("all book slots are taken by this subscriber, please return a book and try again");
            sqlconnection.Close();
            return;
        }
        public void ReturnBook()
        {
            Console.WriteLine("please enter the id of the sub who would like to return the book");
            string sub_id = Console.ReadLine();
            while (sub_id.Length > 9 || sub_id.Length < 8)
            {
                Console.WriteLine("please enter only number(remember its up to five digits) " +
                    "and only between 8 and 9 digits");
                sub_id = Console.ReadLine();
            }
            Console.WriteLine("please enter the id of the book");
            string key_book = Console.ReadLine();
            while (key_book.Length > 5)
            {
                Console.WriteLine("please enter only number(remember its up to five digits) ");
                key_book = Console.ReadLine();
            }
            SqlConnectionStringBuilder conn_str = new SqlConnectionStringBuilder();
            conn_str.ConnectionString = @"Server=localhost\SQLEXPRESS;Database=NatanielParievsky;Trusted_Connection=True;";

            SqlConnection sqlconnection = new SqlConnection(conn_str.ConnectionString);
            sqlconnection.Open();
            using SqlCommand cmd = new SqlCommand
            {
                Connection = sqlconnection,
                CommandType = System.Data.CommandType.Text,
                CommandText = "SELECT * FROM Subscribers Where id=@sub_id",
            };
            //checking if subscriber exists in table
            SqlParameter sqlparam = new SqlParameter("sub_id", System.Data.SqlDbType.NChar);
            sqlparam.Value = sub_id;
            cmd.Parameters.Add(sqlparam);
            using SqlDataReader results = cmd.ExecuteReader();

            if (!results.HasRows)
            {
                results.Close();
                Console.WriteLine("the subscriber doesnt exist, please try another");
                return;

            }
            results.Close();

            //checking if book exists in table
            cmd.CommandText = "SELECT * FROM Books WHERE id_book=@key_book";
            sqlparam = new SqlParameter("key_book", System.Data.SqlDbType.NChar);
            sqlparam.Value = key_book;
            cmd.Parameters.Add(sqlparam);
            using SqlDataReader results2 = cmd.ExecuteReader();

            if (!results2.HasRows)
            {
                results2.Close();
                Console.WriteLine("the book doesnt exist, please try another");
                return;
            }
            results2.Close();

            //get type of book and if it's physical
            cmd.CommandText = "SELECT type FROM Books WHERE id_book=@key_book";
            object type = cmd.ExecuteScalar();
            char type_book = System.Convert.ToChar(type);

            //now we check which slot has the id of the book and return it
            cmd.CommandText = "SELECT book1 FROM Subscribers WHERE id=@sub_id";
            object book1 = cmd.ExecuteScalar();
            string book_1 = System.Convert.ToString(book1);
            if (book_1 == key_book)
            {
                cmd.CommandText = "UPDATE Subscribers SET book1='' WHERE id=@sub_id";
                cmd.ExecuteNonQuery();
                Console.WriteLine("book1 returned succefully");
                if (type_book == '1')
                {
                    cmd.CommandText = "UPDATE Books SET copies+=1 WHERE id_book=@key_book";
                    cmd.ExecuteNonQuery();
                }
                sqlconnection.Close();
                return;
            }
            cmd.CommandText = "SELECT book2 FROM Subscribers WHERE id=@sub_id";
            object book2 = cmd.ExecuteScalar();
            string book_2 = System.Convert.ToString(book2);
            if (book_2 == key_book)
            {
                cmd.CommandText = "UPDATE Subscribers SET book2='' WHERE id=@sub_id";
                cmd.ExecuteNonQuery();
                Console.WriteLine("book2 returned succefully");
                if (type_book == '1')
                {
                    cmd.CommandText = "UPDATE Books SET copies+=1 WHERE id_book=@key_book";
                    cmd.ExecuteNonQuery();
                }
                sqlconnection.Close();
                return;
            }
            cmd.CommandText = "SELECT book3 FROM Subscribers WHERE id=@sub_id";
            object book3 = cmd.ExecuteScalar();
            string book_3 = System.Convert.ToString(book3);
            if (book_3 == key_book)
            {
                cmd.CommandText = "UPDATE Subscribers SET book3='' WHERE id=@sub_id";
                cmd.ExecuteNonQuery();
                Console.WriteLine("book3 returned succefully");
                if (type_book == '1')
                {
                    cmd.CommandText = "UPDATE Books SET copies+=1 WHERE id_book=@key_book";
                    cmd.ExecuteNonQuery();
                }
                sqlconnection.Close();
                return;
            }
            else
            {
                Console.WriteLine("the subscriber doesnt have the book, please try again");
                sqlconnection.Close();
                return;
            }
        }
        public void PrintBookDetails() 
        {
            Console.WriteLine("please enter the id of the book");
            string key_book = Console.ReadLine();
            while (key_book.Length > 5)
            {
                Console.WriteLine("please enter only number(remember its up to five digits) ");
                key_book = Console.ReadLine();
            }
            SqlConnectionStringBuilder conn_str = new SqlConnectionStringBuilder();
            conn_str.ConnectionString = @"Server=localhost\SQLEXPRESS;Database=NatanielParievsky;Trusted_Connection=True;";

            SqlConnection sqlconnection = new SqlConnection(conn_str.ConnectionString);
            sqlconnection.Open();
            using SqlCommand cmd = new SqlCommand
            {
                Connection = sqlconnection,
                CommandType = System.Data.CommandType.Text,
                CommandText = "SELECT * FROM Books Where id_book=@key_book",
            };
     
            SqlParameter sqlparam = new SqlParameter("key_book", System.Data.SqlDbType.NChar);
            sqlparam.Value = key_book;
            cmd.Parameters.Add(sqlparam);
            cmd.CommandText = "SELECT type FROM Books WHERE id_book=@key_book";
            object type = cmd.ExecuteScalar();
            char type_book = System.Convert.ToChar(type);
            cmd.CommandText = "SELECT * FROM Books Where id_book=@key_book";
            using SqlDataReader results = cmd.ExecuteReader();
            //checking if book exists
            if (!results.HasRows)
            {
                Console.WriteLine("book doesnt exist, please try again");
                return;
            }
            //if book exists, comes here
            while (results.Read())
            {
                if (type_book=='1')
                {
                    Console.WriteLine($"{results[0]}, {results[1]}, {results[2]}, {results[3]}, physical, number of copies- {results[5]}");
                }
                if (type_book == '2')
                {
                    Console.WriteLine($"{results[0]}, {results[1]}, {results[2]}, {results[3]}, digital, number of copies- {results[5]}");
                }

            }
            results.Close();
            sqlconnection.Close();
            return;
        }
        public void PrintBooksByGenre() 
        {
            Console.WriteLine("please enter the genre of the books");
            string genre = Console.ReadLine();
            while (genre=="")
            {
                Console.WriteLine("the genre cant be empty, please try again");
                genre = Console.ReadLine();
            }
            SqlConnectionStringBuilder conn_str = new SqlConnectionStringBuilder();
            conn_str.ConnectionString = @"Server=localhost\SQLEXPRESS;Database=NatanielParievsky;Trusted_Connection=True;";

            SqlConnection sqlconnection = new SqlConnection(conn_str.ConnectionString);
            sqlconnection.Open();
            using SqlCommand cmd = new SqlCommand
            {
                Connection = sqlconnection,
                CommandType = System.Data.CommandType.Text,
                CommandText = "SELECT * FROM Books Where genre=@genre",
            };

            SqlParameter sqlparam = new SqlParameter("genre", System.Data.SqlDbType.NChar);
            sqlparam.Value = genre;
            cmd.Parameters.Add(sqlparam);
            using SqlDataReader results = cmd.ExecuteReader();
            if(!results.HasRows) 
            {
                Console.WriteLine("there arent any books by that genre, please try again");
                return;
            }
            while (results.Read())
            {
                Console.WriteLine($"{results[0]}- {results[1]}");

            }
            results.Close();
            sqlconnection.Close();
            return;
        }
        public void PrintBooksBySub()
        {
            Console.WriteLine("please enter the id of the sub you would like to see the book of");
            string sub_id = Console.ReadLine();
            while (sub_id.Length > 9 || sub_id.Length < 8)
            {
                Console.WriteLine("please enter only number(remember its up to five digits) " +
                    "and only between 8 and 9 digits");
                sub_id = Console.ReadLine();
            }
            SqlConnectionStringBuilder conn_str = new SqlConnectionStringBuilder();
            conn_str.ConnectionString = @"Server=localhost\SQLEXPRESS;Database=NatanielParievsky;Trusted_Connection=True;";

            SqlConnection sqlconnection = new SqlConnection(conn_str.ConnectionString);
            sqlconnection.Open();
            using SqlCommand cmd = new SqlCommand
            {
                Connection = sqlconnection,
                CommandType = System.Data.CommandType.Text,
                CommandText = "SELECT * FROM Subscribers Where id=@sub_id",
            };

            SqlParameter sqlparam = new SqlParameter("sub_id", System.Data.SqlDbType.NChar);
            sqlparam.Value = sub_id;
            cmd.Parameters.Add(sqlparam);
            using SqlDataReader results = cmd.ExecuteReader();
            if (!results.HasRows)
            {
                results.Close();
                Console.WriteLine("subscribers doesnt exist, please try again");
                return;
            }
            while (results.Read())
            {
                Console.WriteLine($"{results[3]}, {results[4]}, {results[5]}");

            }
            results.Close();
            Console.WriteLine();
            //prints all books
            cmd.CommandText = "SELECT * FROM Books";
            sqlparam = new SqlParameter("", System.Data.SqlDbType.NChar);
            sqlparam.Value = "";
            cmd.Parameters.Add(sqlparam);
            using SqlDataReader results2 = cmd.ExecuteReader();
            while (results2.Read())
            {
                Console.WriteLine($"{results2[0]}, {results2[1]}");

            }
            results2.Close();
            sqlconnection.Close();
            return;
        }
    }
}
