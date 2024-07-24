using System.Collections.Generic;

public class RoomData
{
    public int roomId;
    public List<int> listUser = new List<int>();

    public RoomData(int roomId)
    {
        this.roomId = roomId;
    }
}