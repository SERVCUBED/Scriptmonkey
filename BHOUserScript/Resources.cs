using System;

namespace BHOUserScript
{
    internal static class Resources
    {
        public const string FirstTimeSetup = "Scriptmonkey is being set up for first time use. Please stand by. Your browser may appear unresponsive.";

        public const string FirstTimeSetupDone = "Finished settings up Scriptmonkey. Enjoy! :)";

        public static readonly string AutomaticAddFailError = "Oops! Unable to automatically install the script. Please try again or install the script manually." + Environment.NewLine + Environment.NewLine + "Error:" + Environment.NewLine;

        public const string Title = "Scriptmonkey";

        // Contents of Wrapper.js, minified and split to allow scriptIndex to be set easily
        public const string WrapperJS_Before = "function GM_deleteValue(e){window.Scriptmonkey.deleteScriptValue(e,scriptIndex,apiKey)}function GM_getValue(e,t){return window.Scriptmonkey.getScriptValue(e,t,scriptIndex,apiKey)}function GM_listValues(){return window.Scriptmonkey.getScriptValuesList(scriptIndex,apiKey)}function GM_setValue(e,t){window.Scriptmonkey.setScriptValue(e,t,scriptIndex,apiKey)}function GM_addStyle(e){css=document.createElement('style'),css.type='text/css',css.innerHTML=e,document.body.appendChild(css)}function GM_openInTab(e){window.open(e)}function GM_log(e){console.log('Scriptmonkey: '+e)}function GM_setClipboard(e){window.Scriptmonkey.setClipboard(e)}function GM_getResourceText(e){window.Scriptmonkey.getScriptResourceText(e,scriptIndex,apiKey)}function GM_getResourceURL(e){window.Scriptmonkey.getScriptResourceUrl(e,scriptIndex,apiKey)}function GM_registerMenuCommand(e,t,n){window.Scriptmonkey.setMenuCommand(t, e, scriptIndex, apiKey);}function GM_xmlhttpRequest(e){var t=window.Scriptmonkey.xmlHttpRequest(JSON.stringify(e),scriptIndex,apiKey);e.onload&&e.onload(JSON.parse(t))}scriptIndex=";
        public const string WrapperJS_Mid = ",unsafeWindow=window,apiKey='";
        public const string WrapperJS_After = "';";
    }
}
