using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityExample1.Models
{
    public class UserTasks
    {
        private int taskId;
        private int ownerId;
        private string description;
        private DateTime dueDate;
        private int complete;

        public int TaskId { get => taskId; set => taskId = value; }
        public int OwnerId { get => ownerId; set => ownerId = value; }
        public string Description { get => description; set => description = value; }
        public DateTime DueDate { get => dueDate; set => dueDate = value; }
        public int Complete { get => complete; set => complete = value; }
    }
}
