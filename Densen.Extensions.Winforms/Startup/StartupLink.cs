using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Forms;

namespace Extensions.Winforms;

[ComImport]
[Guid("00021401-0000-0000-C000-000000000046")]
class ShellLink
{
}

/// <summary>The IShellLink interface allows Shell links to be created, modified, and resolved</summary>
[ComImport()]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("000214F9-0000-0000-C000-000000000046")]
interface IShellLinkW
{
    /// <summary>Retrieves the path and file name of a Shell link object</summary>
    void GetPath([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, out IntPtr pfd, int fFlags);
    /// <summary>Retrieves the list of item identifiers for a Shell link object</summary>
    void GetIDList(out IntPtr ppidl);
    /// <summary>Sets the pointer to an item identifier list (PIDL) for a Shell link object.</summary>
    void SetIDList(IntPtr pidl);
    /// <summary>Retrieves the description string for a Shell link object</summary>
    void GetDescription([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);
    /// <summary>Sets the description for a Shell link object. The description can be any application-defined string</summary>
    void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
    /// <summary>Retrieves the name of the working directory for a Shell link object</summary>
    void GetWorkingDirectory([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);
    /// <summary>Sets the name of the working directory for a Shell link object</summary>
    void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
    /// <summary>Retrieves the command-line arguments associated with a Shell link object</summary>
    void GetArguments([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);
    /// <summary>Sets the command-line arguments for a Shell link object</summary>
    void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
    /// <summary>Retrieves the hot key for a Shell link object</summary>
    void GetHotkey(out short pwHotkey);
    /// <summary>Sets a hot key for a Shell link object</summary>
    void SetHotkey(short wHotkey);
    /// <summary>Retrieves the show command for a Shell link object</summary>
    void GetShowCmd(out int piShowCmd);
    /// <summary>Sets the show command for a Shell link object. The show command sets the initial show state of the window.</summary>
    void SetShowCmd(int iShowCmd);
    /// <summary>Retrieves the location (path and index) of the icon for a Shell link object</summary>
    void GetIconLocation([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath,
        int cchIconPath, out int piIcon);
    /// <summary>Sets the location (path and index) of the icon for a Shell link object</summary>
    void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
    /// <summary>Sets the relative path to the Shell link object</summary>
    void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);
    /// <summary>Attempts to find the target of a Shell link, even if it has been moved or renamed</summary>
    void Resolve(IntPtr hwnd, int fFlags);
    /// <summary>Sets the path and file name of a Shell link object</summary>
    void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
}

public class AppLink
{
    public static bool CreateAppLink(bool Startup = false, bool Delete = false, bool UseApplicationProductName = true,string? ProductName=null)
    {
        string AppPath = Application.ExecutablePath;
        string AppName = ProductName ?? 
            (UseApplicationProductName ? Application.ProductName: Path.GetFileNameWithoutExtension(AppPath));
        string AppLink = Environment.GetFolderPath(Startup ?  Environment.SpecialFolder.Startup: Environment.SpecialFolder.Desktop ) + "\\" + AppName + ".lnk";
        if (Delete)
        {
            if (File.Exists(AppLink))
            {
                File.Delete(AppLink);
                return true;
            }
        }
        else
        {
            if (!File.Exists(AppLink))
            {
                IShellLinkW link = (IShellLinkW)new ShellLink();
                link.SetPath(AppPath);
                link.SetDescription(AppName);
                link.SetWorkingDirectory(Path.GetDirectoryName(AppPath));
                link.SetIconLocation(AppPath, 0);
                IPersistFile file = (IPersistFile)link;
                file.Save(AppLink, false);
                return true;
            }
        }
        return false;
    }


    public static bool IsInStartup()
    {
        var startupPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
        var exeFilePath = Application.ExecutablePath;
        var appName = Path.GetFileNameWithoutExtension(exeFilePath);
        var lnkFilePath = Path.Combine(startupPath, $"{appName}.lnk");
        if (File.Exists(lnkFilePath))
            return true;
        return false;
    }
}