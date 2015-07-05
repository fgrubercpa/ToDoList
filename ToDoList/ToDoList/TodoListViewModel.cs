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
    class TodoListViewModel
    {
        public ObservableCollection<ToDoListItem> Items { get; set; }
       
        
        public TodoListViewModel()
        {

            Items = new ObservableCollection<ToDoListItem>
            {
          
            };

    
     
 // Add Current Items in Database to 'To Do Items List' named 'ToDoListItem'. 
 
            
            
            using (SqlConnection connection = new SqlConnection("Server=MSI;Database=ToDoList;Trusted_Connection=True;"))
                

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
            
            





            //using (TodoListContext context = new TodoListContext())
            //{
            //    foreach (ToDoListItem item in context.ToDoListTable)
            //    {
            //        Items.Add(item);
            //    }
            //}

        }



// Add New 'To Do Item' to 'To Do Item List' named 'ToDoListItem'.     

        


        public ToDoListItem AddItem (string description)
        {
            ToDoListItem itemToAdd = new ToDoListItem();
                   
            itemToAdd.Description = description;
         
       
            Items.Add(itemToAdd);
            //using (TodoListContext context=new TodoListContext())
            //{
            //    context.ToDoListTable.Add(itemToAdd);
            //    context.SaveChanges();

            //}
          
            
            
            using (SqlConnection connection = new SqlConnection("Server=MSI;Database=ToDoList;Trusted_Connection=True;"))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    string insertStatement = "insert dbo.ToDoListItem(description) values(@desc2)";
                    //string insertStatement = "insert dbo.ToDoListItem(description) values('" + description + "')";
                    
                    command.CommandText = insertStatement;
                    command.Parameters.Add("@desc2", SqlDbType.NVarChar);
                    command.Parameters["@desc2"].Value = description;
                    command.ExecuteNonQuery();

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