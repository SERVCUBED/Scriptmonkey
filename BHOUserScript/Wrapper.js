// Script to inject into every page. This contains wrappers for GM_ functions and Scriptmonkey's own library.
scriptIndex = 0; // This will be set dynamically in code
unsafeWindow = window;

// Wrappers for the GM_ functions
function GM_deleteValue(name)
{
    window.Scriptmonkey.deleteScriptValue(name, scriptIndex);
}

function GM_getValue(name, defaultVal)
{
    return window.Scriptmonkey.getScriptValue(name, defaultVal, scriptIndex);
}

function GM_listValues()
{
    return window.Scriptmonkey.getScriptValuesList(scriptIndex);
}

function GM_setValue(name, value)
{
    window.Scriptmonkey.setScriptValue(name, value, scriptIndex);
}

function GM_addStyle(cssText)
{
    css = document.createElement('style');
    css.type = 'text/css';
    css.innerHTML = cssText;
    document.body.appendChild(css);
}

function GM_openInTab(url)
{
    window.open(url);
}

function GM_log(message)
{
    console.log('Scriptmonkey: ' + message);
}

function GM_setClipboard(data)
{
    window.Scriptmonkey.setClipboard(data);
}

function GM_getResourceText(name)
{
    window.Scriptmonkey.getScriptResourceText(name, scriptIndex);
}

function GM_getResourceURL(name)
{
    window.Scriptmonkey.getScriptResourceUrl(name, scriptIndex);
}

function GM_registerMenuCommand(caption, func, key)
{
    //TODO: Implement this.
}

function GM_xmlhttpRequest(details)
{
    var response = window.Scriptmonkey.xmlHttpRequest(JSON.stringify(details));
    details.onload(JSON.parse(response));
}
