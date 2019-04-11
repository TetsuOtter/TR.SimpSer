using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO.Ports;
using System.Runtime.InteropServices;

namespace TR.SimpSer
{
  /// <summary>メインの機能をここに実装する。</summary>
  static internal class Main
  {
    static private SerialPort SP = null;
    static private ushort DataNum = 0;
    static private int PortNum = 0;
    static private int BaudRateNum = 0;

    const int SetVehicleSpecMin = 1;
    const int SetVehicleSpecMax = 24;
    const int InitializeMin = 25;
    const int InitializeMax = 25;
    const int SetPowerMin = 51;
    const int SetPowerMax = 51;
    const int SetBrakeMin = 61;
    const int SetBrakeMax = 61;
    const int SetReverserMin = 71;
    const int SetReverserMax = 71;
    const int DoorOCMin = 81;
    const int DoorOCMax = 81;
    const int SetSignalMin = 91;
    const int SetSignalMax = 91;
    const int SetBeaconMin = 100;
    const int SetBeaconMax = 355;
    const int KeyDUMin = 500;
    const int KeyDUMax = 520;
    const int ElapseMin = 901;
    const int ElapseMax = 2155;


    static internal void Load()
    {
#if DEBUG
      MessageBox.Show("PITempCS Debug Build");//If you don't need, please remove it.
#endif
      //ファイル名読む
      //DLL情報Getして位置GetしてFolderごとSplitしてそのLast(DLL名)をGet
      //Refer : http://d.hatena.ne.jp/tekk/20110307/1299513821
      string FileNameStr = Assembly.GetExecutingAssembly().Location.Split('\\').Last();
      string[] FileNameStrArr = FileNameStr.Split('.');
      if (FileNameStrArr[0] != "TR" || FileNameStrArr[1] != "SimpSer")//書式エラー
      {
        MessageBox.Show("ERR01 : Configuration format is incorrect.\nThe file name is : " + FileNameStr, "SimpSer", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      try
      {
        PortNum= int.Parse(FileNameStrArr[3]);
        BaudRateNum = int.Parse(FileNameStrArr[4]);
      }catch(Exception e)//Int変換エラー
      {
        MessageBox.Show("ERR02 : Int.Parse Error\n\nError message\n" + e.Message, "SimpSer", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      if (PortNum < 0 || PortNum > 255)//ポート番号の範囲
      {
        MessageBox.Show("ERR03 : PortNum Range Error\nPlease set in 0 ~ 255", "SimpSer", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      if (BaudRateNum <= 0)//BaudRateの範囲
      {
        MessageBox.Show("ERR04 : BaudRate Range Error\nPlease set in Natural Number", "SimpSer", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      //DataName判別
      try
      {
        DataNum = ushort.Parse(FileNameStrArr[2]);
      }
      catch (FormatException)
      {
        DataNum = DataNumSearch(in FileNameStrArr[2]);
        if (DataNum == 0)
        {
          MessageBox.Show("ERR09 : This name is not defined.\nName : " + FileNameStrArr[2], "SimpSer", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      catch (Exception e)
      {
        MessageBox.Show("ERR08 : Int.Parse Error in DataName/Num\n\nError message\n" + e.Message, "SimpSer", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      if (DataNum == 0) return;
      //シリアル開く
      try
      {
        SP = new SerialPort()
        {
          BaudRate = BaudRateNum,
          PortName = "COM" + PortNum.ToString()
        };
      }catch(Exception e)//SerialPort開けなかった
      {
        MessageBox.Show("ERR05 : SerialPort Opening Error\n\nError message\n" + e.Message, "SimpSer", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      if (SP == null) { MessageBox.Show("ERR06 : SerialPort Opening Error\nValue is NULL.", "SimpSer", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
      if (!SP.IsOpen) { MessageBox.Show("ERR07 : SerialPort is Closed.", "SimpSer", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
    }

    static private ushort DataNumSearch(in string s)
    {
      switch (s)
      {
        case "STTa":return 901;
        case "SPDf":return 903;
        default:return 0;
      }
    }

    static internal void Dispose()
    {
      if (SP.IsOpen) SP.Close();
    }

    static internal void GetVehicleSpec(Spec s)
    {

    }

    static internal void Initialize(int s)
    {

    }
    //Attention! This Method uses "UNSAFE".
    static unsafe internal void Elapse(State st, int* Pa, int* Sa)
    {
      if (SP.IsOpen && DataNum >= ElapseMin && DataNum <= ElapseMax)
      {
        switch (DataNum)
        {
          case 901:
            //Refer : http://note.websmil.com/csharp/c-%E6%A7%8B%E9%80%A0%E4%BD%93%E3%81%A8%E3%83%90%E3%82%A4%E3%83%88%E9%85%8D%E5%88%97%EF%BC%88byte%EF%BC%89%E3%81%AE%E5%A4%89%E6%8F%9B
            int sz = Marshal.SizeOf(st);
            byte[] ba = new byte[sz];
            fixed(byte* pb = ba)
            {
              *(State*)pb = st;
            }
            SP?.Write(ba,0,sz);
            break;
          case 903:
            SP?.WriteLine(st.V.ToString());
            break;
        }
      }
    }

    static internal void SetPower(int p)
    {

    }

    static internal void SetBrake(int b)
    {

    }

    static internal void SetReverser(int r)
    {

    }
    static internal void KeyDown(int k)
    {

    }

    static internal void KeyUp(int k)
    {

    }

    static internal void DoorOpen()
    {

    }
    static internal void DoorClose()
    {

    }
    static internal void HornBlow(int h)
    {

    }
    static internal void SetSignal(int s)
    {

    }
    static internal void SetBeaconData(Beacon b)
    {

    }
  }
}
