using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class SysRoom : Singleton<SysRoom>
{
    public RoomData roomData;
    public void CreateRoomData(int roomId)
    {
        roomData = new RoomData(roomId);
    }
    public void Clear()
    {
        roomData.listUser.Clear();
        roomData.roomId = 0;
    }
}