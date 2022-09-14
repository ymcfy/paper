using Net.Media;
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using DG.Tweening;
using Excel;
using System.Data;
public class jiankong : MonoBehaviour
{
    public bool isshow = false;
    //视频宽
    public int width;
    //视频高
    public int height;
    public Texture2D texture;
    public Material mat;
    IntPtr libvlc_instance_t;
    IntPtr libvlc_media_player_t;
    IntPtr handle;
    public string rtspurl;
    private MediaPlayer.VideoLockCB _videoLockCB;
    private MediaPlayer.VideoUnlockCB _videoUnlockCB;
    private MediaPlayer.VideoDisplayCB _videoDisplayCB;
    private const int _width = 1024;
    private const int _height = 576;
    private const int _pixelBytes = 4;
    private const int _pitch = 1024 * _pixelBytes;
    private IntPtr _buff = IntPtr.Zero;
    private float fireRate = 0.02F;
    private float nextFire = 0.0F;
    bool ready = false;
    //bool值判断当前是否是打开状态；
    public bool a;
    // Use this for initialization
    void Start()
    {
        if (_videoLockCB == null)
            _videoLockCB = new MediaPlayer.VideoLockCB(VideoLockCallBack);
        if (_videoUnlockCB == null)
            _videoUnlockCB = new MediaPlayer.VideoUnlockCB(VideoUnlockCallBack);
        if (_videoDisplayCB == null)
            _videoDisplayCB = new MediaPlayer.VideoDisplayCB(VideoDiplayCallBack);
        texture = new Texture2D(1024, 576, TextureFormat.RGBA32, false);
        mat.mainTexture = texture;
        _buff = Marshal.AllocHGlobal(_pitch * _height);
        handle = new IntPtr(1);
        libvlc_instance_t = MediaPlayer.Create_Media_Instance();
        libvlc_media_player_t = MediaPlayer.Create_MediaPlayer(libvlc_instance_t, handle);
        MediaPlayer.SetCallbacks(libvlc_media_player_t, _videoLockCB, _videoUnlockCB, _videoDisplayCB, IntPtr.Zero);
        GameObject.Find("Main Camera/Quad").transform.DOScale(new Vector3(0, 0, 0), 1f);
    }
    void creat_url(string jiankong_name)
    {
        FileStream stream = File.Open(Application.dataPath + "\\Data\\" + "/jiankong.xlsx", FileMode.Open, FileAccess.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        DataSet result = excelReader.AsDataSet();
        int rows = result.Tables[0].Rows.Count;
        for (int i = 0; i < rows; i++)
        {
            string shebei = result.Tables[0].Rows[i][7].ToString();
            if (shebei.Contains(jiankong_name))
            {
                string IP = result.Tables[0].Rows[i][2].ToString();
                string URL = "rtsp://admin:a1234567@" + IP + "/h264/ch1/main/av_stream";
                showScreen(URL);
            }
        }

    }
    public void showScreen(string URL)
    {
        ready = MediaPlayer.NetWork_Media_Play(libvlc_instance_t, libvlc_media_player_t, URL);
        width = MediaPlayer.GetMediaWidth(libvlc_media_player_t);
        height = MediaPlayer.GetMediaHeight(libvlc_media_player_t);
        MediaPlayer.SetFormart(libvlc_media_player_t, "ARGB", _width, _height, _width * 4);
        GameObject.Find("Main Camera/Quad").transform.DOScale(new Vector3(1f, 1F, 1F), 0f);
        GameObject.Find("Canvas_button/RawImage_jiankong").transform.DOScale(new Vector3(1, 1f, 1), 0f);
    }
    public void close()
    {
        GameObject.Find("Canvas_button/RawImage_jiankong").transform.DOScale(new Vector3(0, 0, 0), 0f);
        GameObject.Find("Main Camera/Quad").transform.DOScale(new Vector3(0, 0, 0), 0f);
    }
    void Update()
    {
        if (Islock())
        {
            texture.LoadRawTextureData(_buff, _buff.ToInt32());
            texture.Apply();
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "jiankongTag")
                {
                    string name = hit.transform.gameObject.name;
                    Debug.Log("名称" + name);
                    if (!isshow)
                    {
                        creat_url(name);
                    }
                    else if (isshow)
                    {
                        close();
                    }
                    isshow = !isshow;
                }
            }
        }
    }
    public void panduan(string name)
    {
        if (!isshow)
        {
            creat_url(name);
            GameObject jiankong = GameObject.Find("监控/" + name);
            Debug.Log(jiankong);
            Vector3 jiankong_position = jiankong.transform.localPosition;
            this.transform.position = new Vector3(jiankong_position.x, jiankong_position.y + 2, jiankong_position.z);
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (isshow)
        {
            close();
        }
        isshow = !isshow;
    }
    //private void OnGUI()
    //{
    //    if(GUI.Button(new Rect(0,0,100,100),"Take"))
    //    {
    //      Debug.Log (MediaPlayer.TakeSnapShot(libvlc_media_player_t, @Application.streamingAssetsPath, "testa.jpg"));
    //    }

    //}
    private IntPtr VideoLockCallBack(IntPtr opaque, IntPtr planes)
    {
        Lock();
        Marshal.WriteIntPtr(planes, _buff);//初始化 
        //Debug.Log("Lock");
        return IntPtr.Zero;
    }
    private void VideoUnlockCallBack(IntPtr opaque, IntPtr picture, IntPtr planes)
    {
        Unlock();
        //Debug.Log("Unlock");
    }
    private void VideoDiplayCallBack(IntPtr opaque, IntPtr picture)
    {
        if (Islock())
        {
            //Debug.Log("Islock");
            //texture.LoadRawTextureData(picture, picture.ToInt32());
            //texture.Apply();
            //fwrite(buffer, sizeof buffer, 1, fp);  
        }
    }
    bool obj = false;
    private void Lock()
    {
        obj = true;
    }
    private void Unlock()
    {
        obj = false;
    }
    private bool Islock()
    {
        return obj;
    }
    private void OnDestroy()
    {

    }
    [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory")]
    private static extern void CopyMemory(IntPtr Destination, IntPtr Source, uint Length);
    private void OnApplicationQuit()
    {
        try
        {
            Debug.Log(MediaPlayer.MediaPlayer_IsPlaying(libvlc_media_player_t));

            if (MediaPlayer.MediaPlayer_IsPlaying(libvlc_media_player_t))
            {
                MediaPlayer.MediaPlayer_Stop(libvlc_media_player_t);
            }

            MediaPlayer.Release_MediaPlayer(libvlc_media_player_t);

            MediaPlayer.Release_Media_Instance(libvlc_instance_t);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}
