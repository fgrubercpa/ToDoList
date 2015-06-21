using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    class TodoListContext : DbContext

    {
        public DbSet<ToDoListItem> ToDoListTable{get; set; }
        public TodoListContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TodoListContext>());
        }
    }
}
