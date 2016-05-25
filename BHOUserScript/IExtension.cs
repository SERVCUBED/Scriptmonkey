using System.Runtime.InteropServices;

namespace BHOUserScript
{
    [ComVisible(true),
     Guid("FC4801A3-2BA9-11CF-A229-00AA003D7352"),
     InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IExtension
    {
        void setScriptValue(string name, string value, int scriptIndex, string apiKey);

        string getScriptValue(string name, string defaultValue, int scriptIndex, string apiKey);

        void deleteScriptValue(string name, int scriptIndex, string apiKey);

        string getScriptValuesList(int scriptIndex, string apiKey);

        void setClipboard(object data);

        string getScriptResourceText(string resourceName, int scriptIndex, string apiKey);

        string getScriptResourceUrl(string resourceName, int scriptIndex, string apiKey);

        string xmlHttpRequest(string details, int scriptIndex, string apiKey);

        void setMenuCommand(string function, string caption, int scriptIndex, string apiKey);

        void deleteMenuCommand(string caption, int scriptIndex, string apiKey);

        string getVersion();

        int getScriptCount();

        void showOptions();

        void showNotification(string title, string text, string currentUrl);
    }
}