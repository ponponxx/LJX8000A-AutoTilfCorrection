using System.Windows.Forms;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing.Text;
using System.Linq;
using System.Collections;

namespace Sample_AutoTiltCorrection
{
    public partial class Form1 : Form
    {
        private LJX8IF_ETHERNET_CONFIG _ethernetConfig;
        private LJX8IF_GET_PROFILE_REQUEST profileReq;
        private LJX8IF_GET_PROFILE_RESPONSE profileRsp;
        private LJX8IF_PROFILE_INFO profileInfo;
        public Form1()
        {
            InitializeComponent();
        }

        //加主機
        private void Add_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                // Add the text to the CheckedListBox
                controllerList.Items.Add(textBox1.Text + "." + textBox2.Text + "." + textBox3.Text + "." + textBox4.Text);

                // Optionally clear the TextBox after adding
                textBox4.Clear();
            }
            else
            {
                MessageBox.Show("Please enter some text.");
            }
        }


        //刪除主機
        private void Remove_Click(object sender, EventArgs e)
        {
            for (int i = controllerList.Items.Count - 1; i >= 0; i--)
            {
                // Check if the item is checked (or selected)
                if (controllerList.GetItemChecked(i))
                {
                    // Remove the item
                    controllerList.Items.RemoveAt(i);
                }
            }
        }

        //Open Connect
        private void Connect_Click(object sender, EventArgs e)
        {
            string port = textBox5.Text;
            int deviceId = 0;
            for (int i = 0; i <= controllerList.Items.Count - 1; i++)
            {

                // Check if the item is checked (or selected)
                if (controllerList.GetItemChecked(i))
                {
                    IPAddress ipAddress;
                    if (IPAddress.TryParse(controllerList.Items[i].ToString(), out ipAddress))
                    {
                        _ethernetConfig.abyIpAddress = ipAddress.GetAddressBytes();
                        _ethernetConfig.wPortNo = Convert.ToUInt16(port);
                        int rc = NativeMethods.LJX8IF_EthernetOpen(deviceId, ref _ethernetConfig);
                        if (rc == 0)
                        {
                            deviceId += 1;
                            listBox1.Items.Add("連線成功 rc = " + rc);
                        }
                        else
                        {
                            listBox1.Items.Add("連線失敗 rc = " + rc);
                        }

                    }
                    else
                    {
                        listBox1.Items.Add(controllerList.Items[i].ToString() + " : Not a IP address");
                    }

                }
            }
        }
        //Close Connect
        private void Disconnect_Click(object sender, EventArgs e)
        {
            int deviceID = 0;
            for (int i = 0; i <= controllerList.Items.Count - 1; i++)
            {

                if (controllerList.GetItemChecked(i))
                {
                    int rc = NativeMethods.LJX8IF_CommunicationClose(deviceID);
                    if (rc != 0)
                    {
                        listBox1.Items.Add("斷線失敗 rc = " + rc);
                    }
                    else
                    {
                        listBox1.Items.Add("斷線 device ID =  " + deviceID);
                    }
                    deviceID += 1;
                }
            }
        }
        private void Correction_Click(object sender, EventArgs e)
        {
            byte trigBK;
            byte batchBK;
            byte[] angleSettingBK = new byte[28];
            //取觸發模式 + Batch設定
            (trigBK,batchBK,angleSettingBK) = settingBackUp(0);
            listBox1.Items.Add("備份完成:" + trigBK + "/" + batchBK);
            //把Program改成 連續觸發 + 無Batch + 消除目前校正角度
            byte[] angle = new byte[2];
            angle[0] = 0;
            angle[1] = 0;
            settingChange(0,0,0, angleSettingBK,angle);
            //取輪廓
            int[] profileData = new int[3200];
            profileData = getProfile(0);
            //計算角度
            angle = angleCalculation(angleSettingBK, profileData);
            //進行校正與恢復Trigger/Batch設定
            settingChange(0, trigBK, batchBK, angleSettingBK, angle);

        }
        private (byte,byte,byte[]) settingBackUp(int deviceID)
        {
            //取觸發模式
            byte[] triggerSettingData = new byte[4];
            using (PinnedObject pinSettingData = new PinnedObject(triggerSettingData))
            {
                LJX8IF_TARGET_SETTING getSetting = new LJX8IF_TARGET_SETTING();
                getSetting.byType = 0x10;
                getSetting.byCategory = 0x00;
                getSetting.byItem = 0x01;
                getSetting.byTarget1 = 0x00;
                getSetting.byTarget2 = 0x00;
                getSetting.byTarget3 = 0x00;
                getSetting.byTarget4 = 0x00;
                int rc = NativeMethods.LJX8IF_GetSetting(deviceID, 0, getSetting, pinSettingData.Pointer, 4);
                listBox1.Items.Add("修改ID:" + deviceID + "目前觸發模式" + triggerSettingData[0]);                
            }

            //取Batch設定
            byte[] batchSettingData = new byte[4];
            using (PinnedObject pinSettingData = new PinnedObject(batchSettingData))
            {
                LJX8IF_TARGET_SETTING getSetting = new LJX8IF_TARGET_SETTING();
                getSetting.byType = 0x10;
                getSetting.byCategory = 0x00;
                getSetting.byItem = 0x03;
                getSetting.byTarget1 = 0x00;
                getSetting.byTarget2 = 0x00;
                getSetting.byTarget3 = 0x00;
                getSetting.byTarget4 = 0x00;
                int rc = NativeMethods.LJX8IF_GetSetting(deviceID, 1, getSetting, pinSettingData.Pointer, 4);
                listBox1.Items.Add("修改ID:" + deviceID + "目前Batch模式" + batchSettingData[0]);
            }
            byte[] tiltSettingData = new byte[28];
            using (PinnedObject pinSettingData = new PinnedObject(tiltSettingData))
            {
                LJX8IF_TARGET_SETTING getSetting = new LJX8IF_TARGET_SETTING();
                getSetting.byType = 0x10;
                getSetting.byCategory = 0x02;
                getSetting.byItem = 0x0F;
                getSetting.byTarget1 = 0x00;
                getSetting.byTarget2 = 0x00;
                getSetting.byTarget3 = 0x00;
                getSetting.byTarget4 = 0x00;
                int rc = NativeMethods.LJX8IF_GetSetting(deviceID, 1, getSetting, pinSettingData.Pointer, 28);
                listBox1.Items.Add("修改ID:" + deviceID + "Tilt Data取得完成,目前Tilt:" + 
                    tiltSettingData[0].ToString("X2") + " " +
                    tiltSettingData[26].ToString("X2") + " " +
                    tiltSettingData[27].ToString("X2"));
            }
            
            return (triggerSettingData[0], batchSettingData[0], tiltSettingData);
        }
        private void settingChange(int deviceID,byte dataTrig,byte dataBatch, byte[] angleSetting, byte[] angle)
        {
            uint errorSetting = 0;
            byte[] SettingData = new byte[4];
            byte[] SettingDataAngle = angleSetting;
            using (PinnedObject pinSettingData = new PinnedObject(SettingData))
            {
                SettingData[0] = dataTrig;
                LJX8IF_TARGET_SETTING setting = new LJX8IF_TARGET_SETTING();
                setting.byType = 0x10;
                setting.byCategory = 0x00;
                setting.byItem = 0x01;
                setting.byTarget1 = 0x00;
                setting.byTarget2 = 0x00;
                setting.byTarget3 = 0x00;
                setting.byTarget4 = 0x00;
                int rc = NativeMethods.LJX8IF_SetSetting(deviceID, 1, setting, pinSettingData.Pointer, 4,ref errorSetting);
                listBox1.Items.Add("更改觸發設定"+rc);
            }
            using (PinnedObject pinSettingData = new PinnedObject(SettingData))
            {
                SettingData[0] = dataBatch;
                LJX8IF_TARGET_SETTING setting = new LJX8IF_TARGET_SETTING();
                setting.byType = 0x10;
                setting.byCategory = 0x00;
                setting.byItem = 0x03;
                setting.byTarget1 = 0x00;
                setting.byTarget2 = 0x00;
                setting.byTarget3 = 0x00;
                setting.byTarget4 = 0x00;
                int rc = NativeMethods.LJX8IF_SetSetting(deviceID, 1, setting, pinSettingData.Pointer, 4, ref errorSetting);
                listBox1.Items.Add("更改Batch設定" + rc);
            }
            using (PinnedObject pinSettingData = new PinnedObject(SettingDataAngle))
            {
                SettingDataAngle[0] = 1;
                SettingDataAngle[26] = angle[0];
                SettingDataAngle[27] = angle[1];
                LJX8IF_TARGET_SETTING setting = new LJX8IF_TARGET_SETTING();
                setting.byType = 0x10;
                setting.byCategory = 0x02;
                setting.byItem = 0x0F;
                setting.byTarget1 = 0x00;
                setting.byTarget2 = 0x00;
                setting.byTarget3 = 0x00;
                setting.byTarget4 = 0x00;
                int rc = NativeMethods.LJX8IF_SetSetting(deviceID, 1, setting, pinSettingData.Pointer, (uint)SettingDataAngle.Length, ref errorSetting);
                listBox1.Items.Add("更改Angle設定" + rc);
            }


        }
        private int[] getProfile(int deviceID)
        {
            uint profileDataLength = (4 * 6) + (4 * 3200) + (4 * 3200) + 4;
            int[] profileData = new int[profileDataLength/4];
            int[] heightData = new int[3200];
            profileReq.byErase = 0x00;
            profileReq.byTargetBank = 0x00;
            profileReq.byPositionMode = 0x00;
            profileReq.dwGetProfileNo = 0x00;
            profileReq.byGetProfileCount = 0x01;
            using (PinnedObject pinProfileData = new PinnedObject(profileData))
            {
                int rc = NativeMethods.LJX8IF_GetProfile(0,ref profileReq,ref profileRsp,ref profileInfo,pinProfileData.Pointer, profileDataLength);
            }
            Array.Copy(profileData,6,heightData,0,3200);
            return heightData; 
        }
        private byte[] angleCalculation(byte[] setting,int[] height)
        {
            byte[] cal = new byte[16];
            Array.Copy(setting,8,cal,0,16);
            int[] area = new int[4];     // Array to hold the 7 integers

            for (int i = 0; i < 4; i++)
            {
                area[i] = BitConverter.ToInt32(cal, i * 4);
            }
            listBox1.Items.Add("校正範圍:" + area[0] + "/" + area[1] + "/" + area[2] + "/" + area[3]);
            
            int[] heightLeft = new int[400];
            int[] heightRight = new int[400];
            Array.Copy(height, 800, heightLeft, 0, 400);
            Array.Copy(height, 2000, heightRight, 0, 400);
            double heightLeftAverage = heightLeft.Average();
            double heightRightAverage = heightRight.Average();
            double Tan = (heightRightAverage- heightLeftAverage) / ((area[2]+area[3])/2 - (area[1] + area[0])/2) ;
            double deg = Math.Atan(Tan);
            double resultInDegrees = deg * (180 / Math.PI);
            listBox1.Items.Add("resultInDegrees: " + resultInDegrees);
            int bytCalTemp;
            int angleInt = (int)(resultInDegrees * 100);
            if (angleInt < 0)
            {
                bytCalTemp = 65535 + angleInt;
            }
            else
            {
                bytCalTemp = angleInt;
            }
            byte[] byteArray = BitConverter.GetBytes(bytCalTemp);
            byte[] returnByte = new byte[2];
            returnByte[0] = byteArray[0];
            returnByte[1] = byteArray[1];
            return returnByte;
        }

    }
    public class ComboBoxItem
    {
        public string Text { get; set; }
        public int Value { get; set; }

        public ComboBoxItem(string text, int value)
        {
            Text = text;
            Value = value;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}