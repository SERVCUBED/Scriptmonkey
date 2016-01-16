using System.Runtime.InteropServices;

namespace BHOUserScript
{
    [ComVisible(true),
     Guid("FC4801A3-2BA9-11CF-A229-00AA003D7352"),
     InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IExtension
    {
        void setScriptValue(string name, string value, int scriptIndex);

        string getScriptValue(string name, string defaultValue, int scriptIndex);

        void deleteScriptValue(string name, int scriptIndex);

        string getScriptValuesList(int scriptIndex);

        void setClipboard(object data);

        string getScriptResourceText(string resourceName, int scriptIndex);

        string getScriptResourceUrl(string resourceName, int scriptIndex);

        string xmlHttpRequest(string details);

        string getVersion();

        int getScriptCount();

        void showOptions();
    }
}