using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;//使用线程

namespace RF400S_Dll_Demo
{
    public partial class Form1 : Form
    {
        #region "RF400S接口函数的声明"
       
        [DllImport("RF400S.dll")]
        public static extern int RF400S_CommOpen(string Port);

        [DllImport("RF400S.dll")]
        public static extern int RF400S_CommOpenWithBaud(string Port, uint BaudRate);

        [DllImport("RF400S.dll")]
        public static extern int RF400S_CommClose(int comhandle);

        [DllImport("RF400S.dll")]
        public static extern int RF400S_UsbOpen(int venderID, int productID);

        [DllImport("RF400S.dll")]
        public static extern int RF400S_UsbClose(int usbHandle);

        [DllImport("RF400S.dll")]
        public static extern int RF400S_Reset(int portType, int devHandle, byte[] strSend, byte[] strRecv, int nTimeOut);

        [DllImport("RF400S.dll")]
        public static extern int RF400S_SetBuzzer(int portType, int devHandle, byte numberOfTimes, byte[] strSend, byte[] strRecv, int nTimeOut);

        [DllImport("RF400S.dll")]
        public static extern int RF400S_LedControl(int portType, int devHandle, bool bLedOn, byte[] strSend, byte[] strRecv, int nTimeOut);

        [DllImport("RF400S.dll")]
        public static extern int RF400S_SetCommBaud(int portType, int devHandle, byte BaudPM, byte[] strSend, byte[] strRecv, int nTimeOut);

        [DllImport("RF400S.dll")]
        public static extern int RF400S_GetFwVerion(int portType, int devHandle, byte[] strVersion, byte[] strSend, byte[] strRecv, int nTimeOut);


        [DllImport("RF400S.dll")]
        public static extern int RF400S_S50DetectCard(int portType, int devHandle, byte[] strSend, byte[] strRecv, int nTimeOut);

        [DllImport("RF400S.dll")]
        public static extern int RF400S_S50GetCardID(int portType, int devHandel, byte[] cardId, byte[] strSend, byte[] strRecv, int nTimeout);

        [DllImport("RF400S.dll")]
        public static extern int RF400S_S50LoadSecKey(int portType, int devHandel, byte SectorNo, byte keyType, byte[] key, byte[] strSend, byte[] strRecv, int nTimeout);

        [DllImport("RF400S.dll")]   //注意S50地址的算法，同时块的数据为16个
        public static extern int RF400S_S50ReadBlock(int portType, int devHandle, byte Address, byte[] Blockdata, byte[] strSend, byte[] strRecv, int nTimeout);

        [DllImport("RF400S.dll")]   //注意S50地址的算法，同时块的数据为16个
        public static extern int RF400S_S50WriteBlock(int portType, int devHandle, byte Address, byte[] Blockdata, byte[] strSend, byte[] strRecv, int nTimeout);

        [DllImport("RF400S.dll")]   //注意S50地址的算法，初始化数据的长度为4个字节
        public static extern int RF400S_S50InitValue(int portType, int devHanel, byte Address, byte[] InitData, byte[] strSend, byte[] strRecv, int nTimeout);

        [DllImport("RF400S.dll")]   //注意S50地址的算法，增值数据的长度为4个字节
        public static extern int RF400S_S50Increment(int portType, int devHanel, byte Address, byte[] IncremData, byte[] strSend, byte[] strRecv, int nTimeout);

        [DllImport("RF400S.dll")]   //注意S50地址的算法，减值数据的长度为4个字节
        public static extern int RF400S_S50Decrement(int portType, int devHanel, byte Address, byte[] DecremData, byte[] strSend, byte[] strRecv, int nTimeout);

        [DllImport("RF400S.dll")]
        public static extern int RF400S_S50Halt(int portType, int devHanel,  byte[] strSend, byte[] strRecv, int nTimeout);

        [DllImport("RF400S.dll")]   //块数据的长度为64
        public static extern int RF400S_S50ReadSector(int portType, int devHandle, byte sectorNo, byte[] readData, byte[] strSend, byte[] strRecv, int nTimeout);

        [DllImport("RF400S.dll")]   //块数据的长度为64
        public static extern int RF400S_S50WrtieSector(int portType, int devHandle, byte sectorNo, byte[] writeData, byte[] strSend, byte[] strRecv, int nTimeout);

        //----------------------S70卡片操作 声明开始-----------------------------------------
        [DllImport("RF400S.dll")]   
        public static extern int RF400S_S70DetectCard(int portType, int devHandle, byte[] strSend, byte[] strRecv, int nTimeout);

        [DllImport("RF400S.dll")]   //cardID 定长4byte
        public static extern int RF400S_S70GetCardID(int portType, int devHandle, byte[] cardID, byte[] strSend, byte[] strRecv, int nTimeout);

        [DllImport("RF400S.dll")]   //Key 定长6byte
        public static extern int RF400S_S70LoadSecKey(int portType, int devHandle, byte SectorNo, byte KeyType, byte[] key, byte[] strSend, byte[] strRecv, int nTimeout);

        [DllImport("RF400S.dll")]   //注意地址计算方法，块的长度固定16
        public static extern int RF400S_S70ReadBlock(int portType, int devHandle, byte Address, byte[] BlockData, byte[] strSend, byte[] strRecv, int nTimeout);


        [DllImport("RF400S.dll")]   //注意地址计算方法，块的长度固定16
        public static extern int RF400S_S70WriteBlock(int portType, int devHandle, byte Address, byte[] BlockData, byte[] strSend, byte[] strRecv, int nTimeout);


        [DllImport("RF400S.dll")]   //注意地址计算方法，初始化值的长度4
        public static extern int RF400S_S70InitValue(int portType, int devHandle, byte Address, byte[] InitData, byte[] strSend, byte[] strRecv, int nTimeout);

        [DllImport("RF400S.dll")]   //注意地址计算方法，增值的长度4
        public static extern int RF400S_S70Increment(int portType, int devHandle, byte Address, byte[] IncresData, byte[] strSend, byte[] strRecv, int nTimeout);


        [DllImport("RF400S.dll")]   //注意地址计算方法，减值的长度4
        public static extern int RF400S_S70Decrement(int portType, int devHandle, byte Address, byte[] DecremData, byte[] strSend, byte[] strRecv, int nTimeout);

        [DllImport("RF400S.dll")]   
        public static extern int RF400S_S70Halt(int portType, int devHandle,byte[] strSend, byte[] strRecv, int nTimeout);

        [DllImport("RF400S.dll")]   //注意地址计算方法，扇区的长度64
        public static extern int RF400S_S70ReadSector(int portType, int devHandle, byte SectorNo, byte[] ReadData, byte[] strSend, byte[] strRecv, int nTimeout);

        [DllImport("RF400S.dll")]   //注意地址计算方法，扇区的长度64
        public static extern int RF400S_S70WriteSector(int portType, int devHandle, byte SectorNo, byte[] WriteData, byte[] strSend, byte[] strRecv, int nTimeout);

        //----------------------S70卡片操作 声明结束----------------------------------------

        //------------------------UL卡操作声明开始----------------------------------------
        [DllImport("RF400S.dll")]
        public static extern int RF400S_ULDetectCard(int portType, int devHandle, byte[] strSend, byte[] strRecv, int nTimeout);


        [DllImport("RF400S.dll")]   //cardID 定长7个字节
        public static extern int RF400S_ULGetCardID(int portType, int devHandle, byte cardID, byte[] strSend, byte[] strRecv, int nTimeout);

        
        [DllImport("RF400S.dll")]   //16字节长度的BlockData
        public static extern int RF400S_ULWriteSector(int portType, int devHandle, byte SectorNo, byte[] BlockData, byte[] strSend, byte[] strRecv, int nTimeout);

        [DllImport("RF400S.dll")]
        public static extern int RF400S_ULHalt(int portType, int devHandle, byte[] strSend, byte[] strRecv, int nTimeout);

        //-------------------------UL卡操作声明结束----------------------------------------

        //-------读卡模式操作------------------------------------------------
        [DllImport("RF400S.dll")]   //读取到达的卡号
        public static extern int RF400S_RecvCardSn(int portType, int devHandle, byte[] cardSn, ref uint snLen, byte[] strSend, byte[] strRecv, int nTimeout);

        [DllImport("RF400S.dll")]   //0x30,命令，0x31自动
        public static extern int RF400S_SetReadSnMode(int portType, int devHandle, byte mode, byte[] strSend, byte[] strRecv, int nTimeout);

        [DllImport("RF400S.dll")]   ////0x30,命令，0x31自动
        public static extern int RF400S_GetReadSnMode(int portType, int devHandle, ref byte mode, byte[] strSend, byte[] strRecv, int nTimeout);
        //---------------------------------------------------------------------

        #endregion

        int nDevHandle = -1;     //设备的句柄值
        int nPortType = 0; //0: USB, 1: comPort

        //-----Automatic card reading thread----------------------
        Thread AutoReadThread;
        bool m_bThreadStart;
        
        

    
        public Form1()
        {
            InitializeComponent();
            m_bThreadStart = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //打开USB接口的设备，这里写死PID 和VID
            nDevHandle = RF400S_UsbOpen(0x5131, 0x2007);
            if ( -1 != nDevHandle)
            {
                nPortType = 0;
                MessageBox.Show("Turn on the USB device OK ");
            }
            else
            {
                MessageBox.Show("Turn on the USB device Failed");

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int nRet = -1;
            nRet = RF400S_UsbClose(nDevHandle);
            if ( nRet == 0 )
            {
                nDevHandle = -1;
                MessageBox.Show("Turn off the USB device OK ");
            }
            else
            {
                MessageBox.Show("Turn off the USB device Failed");

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //默认的打开的是com1, 9600bsp
            nDevHandle = RF400S_CommOpenWithBaud("com1", 9600);
            if (-1 != nDevHandle)
            {
                nPortType = 1;
                MessageBox.Show("Open Com1 interface successfully ");
            }
            else
            {
                MessageBox.Show("Open Com1 interface Failed");

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            int nRet = -1;
            nRet = RF400S_CommClose(nDevHandle);
            if (nRet == 0)
            {
                nDevHandle = -1;
                MessageBox.Show("Close Com1 interface OK");
            }
            else
            {
                MessageBox.Show("Close Com1 interface Failed");

            }
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            int nRet = -1;
           
            byte[] strSend;
            byte[] strRecv;
            strSend = new byte[400];
            strRecv = new byte[400];

            nRet = RF400S_Reset(nPortType, nDevHandle, strSend, strRecv, 3000);
            if (0 == nRet)
            {
                MessageBox.Show("Reset command succeeded");
            }
            else
            {
                MessageBox.Show("Reset command failed");

            }
        }

        private void btn_SetBeepTime_Click(object sender, EventArgs e)
        {

            int nRet = -1;
            byte[] strSend;
            byte[] strRecv;
            strSend = new byte[400];
            strRecv = new byte[400];

            byte nTimes = System.Convert.ToByte(num_beeptime.Value);
            nRet = RF400S_SetBuzzer(nPortType, nDevHandle, nTimes, strSend, strRecv, 3000);
            if (0 == nRet)
            {
                MessageBox.Show("Set the number of beeps successfully");
            }
            else
            {
                MessageBox.Show("Failed to set the number of beeps");

            }

        }

        private void btn_S50ReadSn_Click(object sender, EventArgs e)
        {
            int nRet = -1;
            byte[] CardID;
            CardID = new byte[4];
            byte[] strSend;
            byte[] strRecv;
            strSend = new byte[400];
            strRecv = new byte[400];
            nRet = RF400S_S50GetCardID(nPortType, nDevHandle, CardID, strSend, strRecv, 3000);
            string strInfo = "";
            if ( 0 == nRet )
            {
                for (int i = 0; i < 4; i++)
                    strInfo += CardID[i].ToString("X2") + " ";

                MessageBox.Show(strInfo);
            }
            else 
            {
                MessageBox.Show("Command failed");
            }
        }

        private void btn_OpenLed_Click(object sender, EventArgs e)
        {
            int nRet = -1;
            byte[] strSend;
            byte[] strRecv;
            strSend = new byte[400];
            strRecv = new byte[400];
            nRet = RF400S_LedControl(nPortType, nDevHandle, true, strSend, strRecv, 3000);
          
            if (0 == nRet)
            {
                MessageBox.Show("LED ON");
            }
            else
            {
                MessageBox.Show("ERROR");
            }
        }

        private void btn_CloseLed_Click(object sender, EventArgs e)
        {
            int nRet = -1;
            byte[] strSend;
            byte[] strRecv;
            strSend = new byte[400];
            strRecv = new byte[400];
            nRet = RF400S_LedControl(nPortType, nDevHandle, false, strSend, strRecv, 3000);
            
            if (0 == nRet)
            {
                MessageBox.Show("LED OFF");
            }
            else
            {
                MessageBox.Show("ERROR");
            }
        }

        private void btn_ManualRead_Click(object sender, EventArgs e)
        {
            int nRet = -1;
            byte[] strSend;
            byte[] strRecv;
            strSend = new byte[400];
            strRecv = new byte[400];
            nRet = RF400S_SetReadSnMode(nPortType, nDevHandle, 0x30, strSend, strRecv, 3000);
           
            if (0 == nRet)
            {
                MessageBox.Show("SUCCESS");
            }
            else
            {
                MessageBox.Show("Command Failed");
            }

        }

        private void btn_AutoRead_Click(object sender, EventArgs e)
        {
            int nRet = -1;
            byte[] strSend;
            byte[] strRecv;
            strSend = new byte[400];
            strRecv = new byte[400];
            nRet = RF400S_SetReadSnMode(nPortType, nDevHandle, 0x31,  strSend, strRecv, 3000);
            
            if (0 == nRet)
            {
                MessageBox.Show("The command is successful, and the thread for automatic card reading");
                AutoReadThread = new Thread(new ThreadStart(AutoReadProc));
                m_bThreadStart = true;
                AutoReadThread.Start();//启动线程
                btn_AutoRead.Enabled = false;   //禁止再次按下
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void btn_QueryMode_Click(object sender, EventArgs e)
        {
            int nRet = -1;
            byte[] strSend;
            byte[] strRecv;
            strSend = new byte[400];
            strRecv = new byte[400];
            byte mode = 0;
            nRet = RF400S_GetReadSnMode(nPortType, nDevHandle, ref mode, strSend, strRecv, 3000);
           
            if (0 == nRet)
            {
                if ( 0x30 == mode)
                    MessageBox.Show("Manual mode");
                if (0x31 == mode)
                    MessageBox.Show("Automatic mode");

            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void btn_SetBaud_Click(object sender, EventArgs e)
        {
            byte baudPM = 0;
            if (combo_Setbaud.Text == "9600")
                baudPM = 0x33;
            if (combo_Setbaud.Text == "19200")
                baudPM = 0x34;
            if (combo_Setbaud.Text == "38400")
                baudPM = 0x35;
            if (combo_Setbaud.Text == "57600")
                baudPM = 0x36;

            int nRet = -1;
            byte[] strSend;
            byte[] strRecv;
            strSend = new byte[400];
            strRecv = new byte[400];
            
            
            nRet = RF400S_SetCommBaud(nPortType, nDevHandle, baudPM, strSend, strRecv, 3000);

            if (0 == nRet)
            {

                MessageBox.Show("Command succeeded");

            }
            else
            {
                MessageBox.Show("Failed");
            }
        }

        private void btn_GetVersion_Click(object sender, EventArgs e)
        {

            int nRet = -1;
            byte[] strSend;
            byte[] strRecv;
            strSend = new byte[400];
            strRecv = new byte[400];
            byte[] strVersion;
            strVersion = new byte[50];
            nRet = RF400S_GetFwVerion(nPortType, nDevHandle, strVersion, strSend, strRecv, 3000);

            if (0 == nRet)
            {
                //命令成功后，需将strVersion由Byte转化成String 
                string strInfo = "";
                for (int i = 0; i < 50; i++)
                    strInfo += strVersion[i].ToString();
                    MessageBox.Show(strInfo);

            }
            else
            {
                MessageBox.Show("ERROR");
            }

        }

        private void btn_S50DetectCard_Click(object sender, EventArgs e)
        {
            int nRet = -1;
            byte[] strSend;
            byte[] strRecv;
            strSend = new byte[400];
            strRecv = new byte[400];
            nRet = RF400S_S50DetectCard(nPortType, nDevHandle, strSend, strRecv, 3000);

            if (0 == nRet)
            {

                MessageBox.Show("Command succeeded");

            }
            else
            {
                MessageBox.Show("Failed");
            }

        }

        private void btn_S50Halt_Click(object sender, EventArgs e)
        {

            int nRet = -1;
            byte[] strSend;
            byte[] strRecv;
            strSend = new byte[400];
            strRecv = new byte[400];
            nRet = RF400S_S50Halt(nPortType, nDevHandle, strSend, strRecv, 3000);

            if (0 == nRet)
            {

                MessageBox.Show("Command succeeded");

            }
            else
            {
                MessageBox.Show("Failed");
            }

        }

        private void btn_S50ReadSector_Click(object sender, EventArgs e)
        {
            int nRet = -1;
            byte[] strSend;
            byte[] strRecv;
            strSend = new byte[1000];
            strRecv = new byte[1000];
            byte nSector = System.Convert.ToByte(num_S50Sector.Value);
            byte[] sectorData;
            sectorData = new byte[64];

            nRet = RF400S_S50ReadSector(nPortType, nDevHandle, nSector, sectorData, strSend, strRecv, 3000);
            string strInfo = "";
            if (0 == nRet)
            {
                for (int i = 0; i < 64; i++)
                    strInfo += sectorData[i].ToString("X2") + " ";

                MessageBox.Show(strInfo);
            }
            else
            {
                MessageBox.Show("Command failed");
            }


        }

        private void btn_S50WriteSector_Click(object sender, EventArgs e)
        {

            int nRet = -1;
            byte[] strSend;
            byte[] strRecv;
            strSend = new byte[1000];
            strRecv = new byte[1000];
            byte nSector = System.Convert.ToByte(num_S50Sector.Value);
            byte[] sectorData;
            sectorData = new byte[64];
            //初始化写入的数据，写入同样的值
            for (int i = 0; i < 64; i ++  )
                sectorData[i] = System.Convert.ToByte(num_S50Writedata.Value);
            nRet = RF400S_S50WrtieSector(nPortType, nDevHandle, nSector, sectorData, strSend, strRecv, 3000);

            if (0 == nRet)
            {

                MessageBox.Show("Command succeeded");
            }
            else
            {
                MessageBox.Show("Command failed");
            }

        }

        private void btn_S50ReadBlock_Click(object sender, EventArgs e)
        {
            int nRet = -1;
            byte[] strSend;
            byte[] strRecv;
            strSend = new byte[1000];
            strRecv = new byte[1000];
            byte nSector = System.Convert.ToByte(num_S50Sector.Value);
            byte nBlock = System.Convert.ToByte(num_S50Block.Value);
            int  Addres = 0;
            //Pay attention to how the address is calculated
            Addres = nSector * 4 + nBlock;

            byte[] BlockData;
            BlockData = new byte[16];
            

            nRet = RF400S_S50ReadBlock(nPortType, nDevHandle, System.Convert.ToByte(Addres), BlockData, strSend, strRecv, 3000);
            string strInfo = "";
            if (0 == nRet)
            {
                for (int i = 0; i < 16; i++)
                    strInfo += BlockData[i].ToString("X2") + " ";

                MessageBox.Show(strInfo);
            }
            else
            {
                MessageBox.Show("Command Failed");
            }
        }

        private void btn_S50WriteBlock_Click(object sender, EventArgs e)
        {
            int nRet = -1;
            byte[] strSend;
            byte[] strRecv;
            strSend = new byte[1000];
            strRecv = new byte[1000];
            byte nSector = System.Convert.ToByte(num_S50Sector.Value);
            byte nBlock = System.Convert.ToByte(num_S50Block.Value);
            int  Addres = 0;
            //注意地址的计算方法
            Addres = nSector * 4 + nBlock;

            byte[] BlockData;
            BlockData = new byte[16];
            //初始化写入的数据，写入同样的值
            for (int i = 0; i < 16; i++)
                BlockData[i] = System.Convert.ToByte(num_S50Writedata.Value);

            nRet = RF400S_S50WriteBlock(nPortType, nDevHandle, System.Convert.ToByte(Addres), BlockData, strSend, strRecv, 3000);
            string strInfo = "";
            if (0 == nRet)
            {

                MessageBox.Show("Command succeeded");
            }
            else
            {
                MessageBox.Show("Command failed");
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {

            int nRet = -1;
            byte[] strSend;
            byte[] strRecv;
            strSend = new byte[1000];
            strRecv = new byte[1000];
            byte nSector = System.Convert.ToByte(num_S50Sector.Value);
            byte keyType = 0x30;    //This is set as the default value and should be selectable in the interface


            byte[] Key;
            Key = new byte[6];
            //Initialize the written data and write the same value
            for (int i = 0; i < 6; i++)
                Key[i] = 0xFF;

            nRet = RF400S_S50LoadSecKey(nPortType, nDevHandle, nSector, keyType, Key, strSend, strRecv, 3000);

            if (0 == nRet)
            {

                MessageBox.Show("Command succeeded");
            }
            else
            {
                MessageBox.Show("Command failed");
            }


        }

        //自动的读卡的线程
        private void AutoReadProc()
        {
            int nRet = -1;
            byte[] strSend;
            byte[] strRecv;
            strSend = new byte[1000];
            strRecv = new byte[1000];

            uint nSnLen = 0;
            byte[] cardSn;
            cardSn = new byte[10];
            string strInfo = "";
            while (m_bThreadStart)
            {

                if (m_bThreadStart == false)
                    break;

                strInfo = "";
                nRet = RF400S_RecvCardSn(nPortType, nDevHandle, cardSn, ref nSnLen, strSend, strRecv, 1000);
                System.Diagnostics.Trace.TraceInformation("fefefe");
                if (nRet == 0)
                {
                    
                    for ( int i =0; i< nSnLen; i ++ )
                        strInfo += cardSn[i].ToString("X2") + " ";
                    MessageBox.Show(strInfo);
                }
                else
                {
                    LabSn.Text = "";
                }
                System.Threading.Thread.Sleep(10);
            }

            btn_AutoRead.Enabled = true;   //按钮使能



        }

        private void button18_Click(object sender, EventArgs e)
        {
           // AutoReadThread.ThreadState
            m_bThreadStart = false;

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }
    }
}
