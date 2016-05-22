// Script to inject into every page. This contains wrappers for GM_ functions and Scriptmonkey's own library.
function Scriptmonkey_S0() { // Scriptmonkey_S[scriptIndex]
    scriptIndex = 0; // This will be set dynamically in code
    unsafeWindow = window;
    apiKey = 'public';

// Wrappers for the GM_ functions
    function GM_deleteValue(name) {
        window.Scriptmonkey.deleteScriptValue(name, scriptIndex, apiKey);
    }

    function GM_getValue(name, defaultVal) {
        return window.Scriptmonkey.getScriptValue(name, defaultVal, scriptIndex, apiKey);
    }

    function GM_listValues() {
        return window.Scriptmonkey.getScriptValuesList(scriptIndex, apiKey);
    }

    function GM_setValue(name, value) {
        window.Scriptmonkey.setScriptValue(name, value, scriptIndex, apiKey);
    }

    function GM_addStyle(cssText) {
        css = document.createElement('style');
        css.type = 'text/css';
        css.innerHTML = cssText;
        document.body.appendChild(css);
    }

    function GM_openInTab(url) {
        window.open(url);
    }

    function GM_log(message) {
        console.log('Scriptmonkey: ' + message);
    }

    function GM_setClipboard(data) {
        window.Scriptmonkey.setClipboard(data);
    }

    function GM_getResourceText(name) {
        window.Scriptmonkey.getScriptResourceText(name, scriptIndex, apiKey);
    }

    function GM_getResourceURL(name) {
        window.Scriptmonkey.getScriptResourceUrl(name, scriptIndex, apiKey);
    }

    function GM_registerMenuCommand(caption, func, key) {
        window.Scriptmonkey.setMenuCommand(/^function\s+([\w\$]+)\s*\(/.exec(func.toString())[1], caption, scriptIndex, apiKey);
    }

    function GM_xmlhttpRequest(details) {
        var response = window.Scriptmonkey.xmlHttpRequest(JSON.stringify(details), scriptIndex, apiKey);
        if (details.onload)
            details.onload(JSON.parse(response));
    }

    // Placeholder
    var GM_info = {};
}
