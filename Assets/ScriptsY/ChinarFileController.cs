using UnityEngine;
using System.Runtime.InteropServices;
using System;
using UnityEngine.UI;
using System.IO;

/// <summary>
/// 文件控制脚本
/// </summary>
public class ChinarFileController : MonoBehaviour
{
    public GameObject RawImage;

    public GameObject shujuku2;

    public object path { get; private set; }

    /// <summary>
    /// 打开项目
    /// </summary>
    public void OpenProject()
    {
        //RawImage.SetActive(false);
        //FileDisplay.GetComponent<Button>().interactable = false;
        OpenFileDlg pth = new OpenFileDlg();
        pth.structSize = Marshal.SizeOf(pth);
        pth.filter = "All files (*.*)|*.*";
        pth.file = new string(new char[256]);
        pth.maxFile = pth.file.Length;
        pth.fileTitle = new string(new char[64]);
        pth.maxFileTitle = pth.fileTitle.Length;
        pth.initialDir = "c:";
        //pth.initialDir = Application.dataPath.Replace("/", "\\") + "\\Resources"; //默认路径
        pth.title = "打开项目";
        pth.defExt = "dat";
        pth.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;
        if (OpenFileDialog.GetOpenFileName(pth))
        {
            //pth.fileTitle为选择文件的名字
            //string[] pthString = pth.fileTitle.Split('.');

            string filepath = pth.file; //选择的文件路径;  

            //clazz img为图片 video为视频 file为文件
            string clazz = "";

            string FileName = Path.GetFileName(filepath);
            string FileNameWithoutExtension = Path.GetFileNameWithoutExtension(filepath);
            string Extension = Path.GetExtension(filepath);

            //Debug.Log(Path.GetFileName(filepath));
            //Debug.Log(Path.GetFileNameWithoutExtension(filepath));
            //Debug.Log(Path.GetExtension(filepath));

            //pthString[1]为文件后缀名，根据后缀名判断文件类型
            if (Extension == ".bmp" || Extension == ".BMP" || Extension == ".jpg" || Extension == ".JPG"
                || Extension == ".jpeg" || Extension == ".JPEG" || Extension == ".png" || Extension == ".PNG"
                || Extension == ".gif" || Extension == ".GIF" || Extension == ".psd" || Extension == ".PSD"
                || Extension == ".tiff" || Extension == ".TIFF" || Extension == ".ai" || Extension == ".AI"
                || Extension == ".eps" || Extension == ".EPS" || Extension == ".svg" || Extension == ".SVG"
                || Extension == ".cr2" || Extension == ".CR2" || Extension == ".nef" || Extension == ".NEF"
                || Extension == ".dng" || Extension == ".DNG" || Extension == ".jiff" || Extension == ".JIFF")
            {
                clazz = "img";
            }
            else if (Extension == ".mp4" || Extension == ".m4v" || Extension == ".mov" || Extension == ".qt"
                || Extension == ".avi" || Extension == ".flv" || Extension == ".wmv" || Extension == ".asf"
                || Extension == ".mpeg" || Extension == ".mpg" || Extension == ".vob" || Extension == ".mkv"
                || Extension == ".asf" || Extension == ".rm" || Extension == ".rmvb" || Extension == ".vob"
                || Extension == ".ts" || Extension == ".dat")
            {
                clazz = "video";
            }
            else
            {
                clazz = "file";
            }

            //开始上传文件到项目里folder文件夹以及数据库 pthString[0]为文件名 pthString[1]文件后缀名 filepath为文件路径 clazz为文件类型
            new FileUpLoadController().Upload(FileNameWithoutExtension, Extension, filepath, clazz);

        }
    }

    public void OpenModelProject()
    {
        //关闭所有二级、三级菜单
        ButtonHoverControl.closeAllTwoMenu();
        ButtonTwoHoverControl.closeAllThreeMenu();
        OpenFileDlg pth = new OpenFileDlg();
        pth.structSize = Marshal.SizeOf(pth);
        pth.filter = "Model File (*.fbx;*.FBX;*.prefab;*.PREFAB)\0*.fbx;*.FBX;*.prefab;*.PREFAB";
        //pth.filter = "All files (*.*)|*.*";
        pth.file = new string(new char[256]);
        pth.maxFile = pth.file.Length;
        pth.fileTitle = new string(new char[64]);
        pth.maxFileTitle = pth.fileTitle.Length;
        pth.initialDir = Application.dataPath.Replace("/", "\\") + "\\Resources"; //默认路径
        pth.title = "打开项目";
        pth.defExt = "dat";
        pth.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;
        if (OpenFileDialog.GetOpenFileName(pth))
        {
            //pth.fileTitle为选择文件的名字
            //string[] pthString = pth.fileTitle.Split('.');

            string filepath = pth.file; //选择的文件路径;  

            string FileName = Path.GetFileName(filepath);
            string FileNameWithoutExtension = Path.GetFileNameWithoutExtension(filepath);
            string Extension = Path.GetExtension(filepath);

            //FileUpLoadController.DeviceId = FileNameWithoutExtension;

            FileUpLoadController.SourcePath = filepath;

            FileUpLoadController.modelType = Extension;

            shujuku2.transform.position = new Vector3(971, 586, 600);

            //new FileUpLoadController().TypeChoose();

            //开始上传文件到项目里folder文件夹以及数据库 pthString[0]为文件名 pthString[1]文件后缀名 filepath为文件路径 clazz为文件类型

        }
    }

    public void OpenTuPianProject()
    {
        OpenFileDlg pth = new OpenFileDlg();
        pth.structSize = Marshal.SizeOf(pth);
        pth.filter = "All files (*.*)|*.*";
        pth.file = new string(new char[256]);
        pth.maxFile = pth.file.Length;
        pth.fileTitle = new string(new char[64]);
        pth.maxFileTitle = pth.fileTitle.Length;
        pth.initialDir = Application.dataPath.Replace("/", "\\") + "\\Resources"; //默认路径
        pth.title = "打开项目";
        pth.defExt = "dat";
        pth.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;
        if (OpenFileDialog.GetOpenFileName(pth))
        {
            //pth.fileTitle为选择文件的名字
            //string[] pthString = pth.fileTitle.Split('.');

            string filepath = pth.file; //选择的文件路径;  

            string FileName = Path.GetFileName(filepath);
            string FileNameWithoutExtension = Path.GetFileNameWithoutExtension(filepath);
            string Extension = Path.GetExtension(filepath);

            FileUpLoadController.ImgType = Extension;

            FileUpLoadController.ImgSourcePath = filepath;

            //开始上传文件到项目里folder文件夹以及数据库 pthString[0]为文件名 pthString[1]文件后缀名 filepath为文件路径 clazz为文件类型

        }
    }
}