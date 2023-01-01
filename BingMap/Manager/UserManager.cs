using BingMap.DB;
using BingMap.Model;
using System;

namespace BingMap.Manager
{
    public sealed class UserManager
    {
        private static readonly Lazy<UserManager> lazy = new Lazy<UserManager>(() => new UserManager());
        public static UserManager Instance { get { return lazy.Value; } }

        public string UserName { get { return userName; } set { userName = value; } }

        public bool IsGuest { get { return isGuest; } set { isGuest = value; } }

        public int Level { get { return m_Level; } set { m_Level = value; } }

        public int Type { get { return m_Type; } set { m_Type = value; } }

        public int PiexlX { get { return m_PiexlX; } set { m_PiexlX = value; } }

        public int PiexlY { get { return m_PiexlY; } set { m_PiexlY = value; } }

        public int WinWidth { get { return m_WinWidth; } set { m_WinWidth = value; } }

        public int WinHeight { get { return m_WinHeight; } set { m_WinHeight = value; } }

        public bool IsOpenTown { get { return isOpenTown; } set { isOpenTown = value; } }
        public bool IsOpenMark { get { return isOpenMark; } set { isOpenMark = value; } }
        public bool IsOpenMRTLine { get { return isOpenMRTLine; } set { isOpenMRTLine = value; } }


        private string userName;
        private bool isGuest;
        private int m_Level;
        private int m_Type;
        private int m_PiexlX;
        private int m_PiexlY;
        private int m_WinWidth;
        private int m_WinHeight;
        private bool isOpenTown;
        private bool isOpenMark;
        private bool isOpenMRTLine;


        private UserManager()
        {
            initUserInfo();
        }

        public void GuestLogin()
		{
			this.userName = "Guest";
			this.isGuest = true;
			this.m_Level = 10;
            this.m_Type = LayerType.A_LAYER_TYPE;
            this.m_PiexlX = 218469;
			this.m_PiexlY = 113688;
			this.m_WinWidth = 700;
			this.m_WinHeight = 700;
            this.isOpenTown = true;
            this.isOpenMark = true;
            this.isOpenMRTLine = true;
        }

        public void initUserInfo()
        {
            //this.isGuest = true;
            this.m_Level = 10;
            this.m_Type = LayerType.A_LAYER_TYPE;
            this.m_PiexlX = 218469;
            this.m_PiexlY = 113688;
            this.m_WinWidth = 700;
            this.m_WinHeight = 700;
            this.isOpenTown = true;
            this.isOpenMark = true;
            this.isOpenMRTLine = true;
        }

        public void UserLogin(string name)
        {
            this.userName = name;
            this.isGuest = false;
            string settingInfo = DbHelper.LoadUserSettingInfo(this.userName);
            string viewstatus = DbHelper.LoadUserViewStatusInfo(this.userName);
            if (settingInfo.Length != 0)
            {
                string[] subString = settingInfo.Split(new char[] { ',' });
                this.m_WinWidth = int.Parse(subString[0]);
                this.m_WinHeight = int.Parse(subString[1]);
                this.m_Type = int.Parse(subString[2]);
                if (subString.Length > 3) {
                    this.isOpenTown = Boolean.Parse(subString[3]);
                    this.isOpenMark = Boolean.Parse(subString[4]);
                    this.isOpenMRTLine = Boolean.Parse(subString[5]);
                }
                subString = viewstatus.Split(new char[] { ',' });
                this.m_PiexlX = int.Parse(subString[0]);
                this.m_PiexlY = int.Parse(subString[1]);
                this.m_Level = int.Parse(subString[2]);
            }
            else
            {
                this.m_Level = 10;
                this.m_Type = LayerType.A_LAYER_TYPE;
                this.m_PiexlX = 218469;
                this.m_PiexlY = 113688;
                this.m_WinWidth = 700;
                this.m_WinHeight = 700;
            }
        }
    }
}
