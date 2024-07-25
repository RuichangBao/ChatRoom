using UnityEngine;
using UnityEngine.UI;

namespace UILogic
{
    public class UIMain : MonoBehaviour
    {
        public Button btnCreateRoom;
        public Button btnJoinRoom;
        public InputField InputFieldRoomId;
        void Start()
        {
            btnCreateRoom.onClick.AddListener(BtnCreateRoom);
            btnJoinRoom.onClick.AddListener(BtnJoinRoom);
        }
        //创建房间
        public void BtnCreateRoom()
        {
            SysRoom.Instance.RequestCreateRoom();
        }


        //加入房间
        public void BtnJoinRoom()
        {
            if (string.IsNullOrEmpty(InputFieldRoomId.text))
                return;
            if (!int.TryParse(InputFieldRoomId.text, out int roomId))
            {
                return;
            }
            SysRoom.Instance.RequestJoinRoom(roomId);
        }
    }
}