using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

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
            using (TodoListContext context = new TodoListContext())
            {
                foreach (ToDoListItem item in context.ToDoListTable)
                {
                    Items.Add(item);
                }
            }

        }

        public ToDoListItem AddItem (string description)
        {
            ToDoListItem itemToAdd = new ToDoListItem();
                   
            itemToAdd.Description = description;
            Items.Add(itemToAdd);
            using (TodoListContext context=new TodoListContext())
            {
                context.ToDoListTable.Add(itemToAdd);
                context.SaveChanges();

            }
            return itemToAdd;

        }

    }
    
}