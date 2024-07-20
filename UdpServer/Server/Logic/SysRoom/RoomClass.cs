using Server.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Logic.SysRoom
{
    public class RoomClass
    {
        private int roomId;
        private List<int> listUsers;
        public RoomClass(int roomId)
        {
            this.roomId = roomId;
            listUsers = new List<int>();
        }
        public void AddUser(int userId)
        {
            listUsers.Add(userId);
        }
        public void RemoveUser(int userId)
        {
            bool result = listUsers.Remove(userId);
        }
    }
}
