// This script wraps the Scriptmonkey notification API for web standards

window.Notification = function (title, options) {
    if (!options)
        window.Scriptmonkey.showNotification(title, "", document.URL);
    else
        window.Scriptmonkey.showNotification(title, options.body, document.URL);
}
window.Notification.permission = "granted";
window.Notification.checkPermission = function () { return "granted"; };
window.Notification.requestPermission = function(f) { f("granted"); };