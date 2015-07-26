using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace ToDoList
{
    using System.Configuration;

    class TodoListViewModel
    {
        public ObservableCollection<ToDoListItem> Items { get; set; }
       
        
        public TodoListViewModel()
        {

            Items = new ObservableCollection<ToDoListItem>
            {
          
            };
             // Add Current Items in Database to 'To Do Items List' named 'ToDoListItem'. 
            var connectionString = ConfigurationManager.ConnectionStrings["TodoList"];
            using (SqlConnection connection = new SqlConnection(connectionString.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    string query = "select * from dbo.ToDoListItem";
                    command.CommandText = query;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string description = reader.GetString(1);
                            ToDoListItem item = new ToDoListItem();
                            item.Description = description;
                            Items.Add(item);
                        }
                    }
                }
            }

        }



// Add New 'To Do Item' to 'To Do Item List' named 'ToDoListItem'.     

        


        public ToDoListItem AddItem (string description)
        {
            ToDoListItem itemToAdd = new ToDoListItem();
                   
            itemToAdd.Description = description;
         
       
            Items.Add(itemToAdd);

            var connectionString = ConfigurationManager.ConnectionStrings["TodoList"];
            using (SqlConnection connection = new SqlConnection(connectionString.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    string insertStatement = "insert dbo.ToDoListItem(description) values(@desc2); select scope_identity()";
                    
                    command.CommandText = insertStatement;
                    command.Parameters.Add("@desc2", SqlDbType.NVarChar);
                    command.Parameters["@desc2"].Value = description;
                    var result = command.ExecuteScalar();
                    itemToAdd.Id = Convert.ToInt32((decimal) result);
                }

            }
            
            
            return itemToAdd;

        }

        public void DeleteItem(ToDoListItem itemToDelete)
        {
            Items.Remove(itemToDelete);
        }


    }
    
}